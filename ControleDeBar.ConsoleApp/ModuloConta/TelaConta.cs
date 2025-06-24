using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloGarcom;
using ControleDeBar.ConsoleApp.ModuloMesa;
using ControleDeBar.ConsoleApp.ModuloProduto;

namespace ControleDeBar.ConsoleApp.ModuloConta
{
    public class TelaConta : TelaBase<Conta>, ITela
    {
        public RepositorioConta<Conta> repositorioConta;
        public RepositorioMesa<Mesa> repositorioMesa;
        public RepositorioGarcom<Garcom> repositorioGarcom;
        public RepositorioProduto<Produto> repositorioProduto;
        public TelaProduto telaProduto;

        public TelaConta(RepositorioConta<Conta> repositorioConta) : base("Conta", repositorioConta)
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
            repositorioConta.FecharConta(conta);
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
            Console.WriteLine("---------------------");
            Console.WriteLine("Conta - Visualizar Pedido");
            Console.WriteLine("---------------------\n");

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
            int numeroMesa = 0;
            bool numeroMesaValido = false;
            Mesa mesa = null;
            Garcom garcom = null;
            List<Produto> pedido = new List<Produto>();

            Console.Write("Digite o nome do cliente: ");
            string nomeCliente = Console.ReadLine();

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

            Console.WriteLine();
            pedido = ObterDadosPedido();

            return new Conta(nomeCliente, mesa, garcom, pedido);
        }

        public List<Produto> ObterDadosPedido()
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
                conta.Pedido.Remove(produto);
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

            if (!repositorioConta.ProdutoExisteNoPedido(produto, conta))
            {
                conta.Pedido.Add(produto);
                repositorioConta.CalcularValor(conta);
                repositorioConta.MarcarProdutosComPedido(conta);
            }
            else
            {
                Console.WriteLine();
                ApresentarMensagem("Este produto já está no pedido informado! Pressione ENTER para tentar novamente...", ConsoleColor.Red);
                AdicionarProdutoNoPedido();
                return;
            }
        }
    }
}
