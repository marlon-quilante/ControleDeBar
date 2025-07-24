using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloProduto;
using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Memoria.ModuloConta;
using ControleDeBar.Infraestrutura.Memoria.ModuloGarcom;
using ControleDeBar.Infraestrutura.Memoria.ModuloMesa;
using ControleDeBar.Infraestrutura.Memoria.ModuloProduto;

namespace ControleDeBar.ConsoleApp.ModuloConta
{
    public class TelaConta : TelaBase<Conta>, ITela
    {
        public RepositorioConta repositorioConta;
        public RepositorioMesa repositorioMesa;
        public RepositorioGarcom repositorioGarcom;
        public RepositorioProduto repositorioProduto;
        public TelaProduto telaProduto;

        public TelaConta(RepositorioConta repositorioConta) : base("Conta", repositorioConta)
        {
            this.repositorioConta = repositorioConta;
        }

        public string EscolherOpcaoMenuConta()
        {
            Console.Clear();
            Console.WriteLine("---------------------------");
            Console.WriteLine($"Controle de Conta");
            Console.WriteLine("---------------------------");

            Console.WriteLine();
            Console.WriteLine("1- Abrir Conta");
            Console.WriteLine("2- Visualizar Contas");
            Console.WriteLine("3- Visualizar Fechamento Diário");
            Console.WriteLine("4- Visualizar Pedido");
            Console.WriteLine("5- Fechar Conta");
            Console.WriteLine("6- Adicionar Item");
            Console.WriteLine("7- Remover Item");
            Console.WriteLine("8- Voltar");
            Console.WriteLine();

            Console.Write("Selecione a opção: ");
            return Console.ReadLine();
        }

        public void FecharConta()
        {
            Console.Clear();
            Console.WriteLine("---------------------");
            Console.WriteLine("Conta - Fechamento");
            Console.WriteLine("---------------------\n");

            Console.WriteLine("Digite o ID da conta que deseja fechar...");
            Console.WriteLine();
            int idConta = ObterID();
            Conta conta = repositorioConta.BuscarRegistroPorID(idConta);

            if (conta.Status != "Aberta")
            {
                Console.WriteLine();
                ApresentarMensagem("Essa conta já está fechada! Pressione ENTER para voltar...", ConsoleColor.Red);
                return;
            }
            conta.FecharConta();
            Console.WriteLine();
            ApresentarMensagem("Conta fechada com sucesso!", ConsoleColor.Green);
        }

        public override void Visualizar()
        {
            base.Visualizar();

            Console.WriteLine("{0,-5} | {1,-15} | {2,-7} | {3, -15} | {4, -15}",
                "ID", "Cliente", "Mesa", "Garçom", "Status");
            foreach (Conta conta in repositorioConta.listaRegistros)
            {
                Console.WriteLine("{0,-5} | {1,-15} | {2,-7} | {3, -15} | {4, -15}",
                conta.Id, conta.NomeCliente, conta._mesa.Numero, conta._garcom.Nome, conta.Status);
            }
            Console.WriteLine();
            ApresentarMensagem("Pressione ENTER para continuar...", ConsoleColor.Yellow);
        }

        public void VisualizarPedido()
        {
            Console.Clear();
            Console.WriteLine("-------------------------");
            Console.WriteLine("Conta - Visualizar Pedido");
            Console.WriteLine("-------------------------\n");

            Console.WriteLine("Digite o ID da conta para visualizar...");
            Console.WriteLine();
            int idConta = ObterID();
            Conta conta = repositorioConta.BuscarRegistroPorID(idConta);
            Console.WriteLine();

            Console.WriteLine("{0,-5} | {1,-12} | {2,-10} | {3, -15} | {4, -15}",
                "ID", "Produto", "Preço", "Quantidade", "Valor Total");
            foreach (Produto produto in conta.Pedido)
            {
                Console.WriteLine("{0,-5} | {1,-12} | {2,-10} | {3, -15} | {4, -15}",
                conta.Id, produto.Nome, produto.Preco.ToString("C2"), produto.QtdDoPedido, produto.ValorDoPedido.ToString("C2"));
            }
            Console.WriteLine();
            ApresentarMensagem("Pressione ENTER para continuar...", ConsoleColor.Yellow);
        }

        public void VisualizarFechamentoDiario()
        {
            Console.Clear();
            Console.WriteLine("-------------------------");
            Console.WriteLine("Conta - Fechamento Diário");
            Console.WriteLine("-------------------------\n");

            decimal valorFechamento = repositorioConta.FechamentoDiario();

            Console.WriteLine("{0,-15} | {1,-10}",
                "Data", "Valor Total");

            Console.WriteLine("{0,-15} | {1,-10}",
            DateTime.Now.ToShortDateString(), valorFechamento.ToString("C2"));

            Console.WriteLine();
            ApresentarMensagem("Pressione ENTER para continuar...", ConsoleColor.Yellow);
        }

        public override Conta ObterDados()
        {
            List<Produto> pedido = new List<Produto>();

            Console.Write("Digite o nome do cliente: ");
            string nomeCliente = Console.ReadLine();
            Console.WriteLine();
            Mesa mesa = ObterDadosMesa();
            Console.WriteLine();
            Garcom garcom = ObterDadosGarcom();
            Console.WriteLine();

            pedido = ObterDadosPedido();

            return new Conta(nomeCliente, mesa, garcom, pedido);
        }

        private Garcom ObterDadosGarcom()
        {
            Garcom garcom = null;

            while (garcom == null)
            {
                Console.Write("Digite o nome do garçom: ");
                string nomeGarcom = Console.ReadLine();

                garcom = repositorioGarcom.BuscarGarcomPorNome(nomeGarcom);

                if (garcom == null)
                {
                    Console.WriteLine();
                    ApresentarMensagem("Não existe garçom cadastrado com esse nome! Pressione ENTER para tentar novamente...", ConsoleColor.Red);
                    continue;
                }
            }
            return garcom;
        }

        private Mesa ObterDadosMesa()
        {
            Mesa mesa = null;
            int numeroMesa = 0;
            bool numeroMesaValido = false;

            while (!numeroMesaValido || mesa == null || mesa.Status != "Livre")
            {
                Console.Write("Digite o número da mesa: ");
                numeroMesaValido = int.TryParse(Console.ReadLine(), out numeroMesa);

                if (!numeroMesaValido)
                {
                    Console.WriteLine();
                    ApresentarMensagem("Número da mesa inválido! Pressione ENTER para tentar novamente...", ConsoleColor.Red);
                    continue;
                }
                else
                    mesa = repositorioMesa.BuscarMesaPorNumero(numeroMesa);

                if (mesa == null)
                {
                    Console.WriteLine();
                    ApresentarMensagem("Não existe mesa cadastrada com esse número! Pressione ENTER para tentar novamente...", ConsoleColor.Red);
                    continue;
                }

                if (mesa.Status != "Livre")
                {
                    Console.WriteLine();
                    ApresentarMensagem("Essa mesa não está livre no momento! Pressione ENTER para tentar novamente...", ConsoleColor.Red);
                    continue;
                }
            }
            return mesa;
        }

        private List<Produto> ObterDadosPedido()
        {
            List<Produto> ListaProdutos = new List<Produto>();
            decimal quantidade = 0;
            bool quantidadeValida = false;

            Console.WriteLine("Digite as informações do produto para incluir ao pedido... ");
            Console.WriteLine();
            int idProduto = telaProduto.ObterID();

            Console.Write("Quantidade: ");
            while (!quantidadeValida)
            {
                quantidadeValida = decimal.TryParse(Console.ReadLine(), out quantidade);

                if (!quantidadeValida)
                {
                    Console.WriteLine();
                    ApresentarMensagem("Quantidade inválida! Pressione ENTER para tentar novamente...", ConsoleColor.Red);
                    Console.WriteLine();
                    continue;
                }
            }

            Produto produto = repositorioProduto.BuscarRegistroPorID(idProduto);
            produto.QtdDoPedido = quantidade;

            produto.MarcarPedido();
            ListaProdutos.Add(produto);

            return ListaProdutos;
        }

        public void RemoverProdutoDoPedido()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Conta - Remover Produto do Pedido");
            Console.WriteLine("---------------------------------\n");

            Console.WriteLine("Digite o ID da conta para remover um produto...");
            Console.WriteLine();
            int idConta = ObterID();
            Conta conta = repositorioConta.BuscarRegistroPorID(idConta);
            Console.WriteLine();

            Console.WriteLine("Digite o ID do produto que deseja remover...");
            Console.WriteLine();
            int idProduto = telaProduto.ObterID();
            Produto produto = repositorioProduto.BuscarRegistroPorID(idProduto);

            if (repositorioConta.ProdutoExisteNoPedido(produto, conta))
            {
                conta.Pedido.Remove(produto);
                ApresentarMensagem("Produto removido com sucesso!", ConsoleColor.Green);
                produto.DesmarcarPedido();
            }
            else
            {
                Console.WriteLine();
                ApresentarMensagem("Este produto não está no pedido informado! Pressione ENTER para tentar novamente...", ConsoleColor.Red);
                RemoverProdutoDoPedido();
                return;
            }
        }

        public void AdicionarProdutoNoPedido()
        {
            decimal quantidade = 0;
            bool quantidadeValida = false;

            Console.Clear();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Conta - Adicionar Produto no Pedido");
            Console.WriteLine("-----------------------------------\n");

            Console.WriteLine("Digite o ID da conta para adicionar um produto...");
            Console.WriteLine();
            int idConta = ObterID();
            Conta conta = repositorioConta.BuscarRegistroPorID(idConta);

            Console.WriteLine();
            Console.WriteLine("Digite o ID do produto que deseja adicionar...");
            Console.WriteLine();
            int idProduto = telaProduto.ObterID();
            Produto produto = repositorioProduto.BuscarRegistroPorID(idProduto);

            if (repositorioConta.ProdutoExisteNoPedido(produto, conta))
            {
                Console.WriteLine();
                ApresentarMensagem("Este produto já está no pedido informado! Pressione ENTER para tentar novamente...", ConsoleColor.Red);
                AdicionarProdutoNoPedido();
                return;
            }
            else
            {
                Console.Write("Quantidade: ");
                while (!quantidadeValida)
                {
                    quantidadeValida = decimal.TryParse(Console.ReadLine(), out quantidade);

                    if (!quantidadeValida)
                    {
                        Console.WriteLine();
                        ApresentarMensagem("Quantidade inválida! Pressione ENTER para tentar novamente...", ConsoleColor.Red);
                        Console.WriteLine();
                        continue;
                    }
                }

                produto.QtdDoPedido = quantidade;
                conta.Pedido.Add(produto);
                ApresentarMensagem("Produto adicionado com sucesso!", ConsoleColor.Green);
                repositorioConta.CalcularValor(conta);
                produto.MarcarPedido();
            }
        }
    }
}
