using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloProduto;

namespace ControleDeBar.Infraestrutura.Arquivos.ModuloConta
{
    public class RepositorioContaEmArquivo : RepositorioBaseEmArquivo<Conta>
    {
        private RepositorioProdutoEmArquivo repositorioProduto;

        public RepositorioContaEmArquivo(ContextoDados contextoDados) : base(contextoDados)
        {
        }

        public override List<Conta> BuscarRegistros()
        {
            return contextoDados.Contas;
        }

        public override void Cadastrar(Conta conta)
        {
            base.Cadastrar(conta);

            CalcularValor(conta);
            conta.Mesa.Ocupar();
            conta.Mesa.TemPedido = true;
            conta.Garcom.TemPedido = true;
            contextoDados.Salvar();
        }

        public void CalcularValor(Conta conta)
        {
            foreach (Produto produto in conta.Pedido)
            {
                if (produto.ValorDoPedido == 0)
                {
                    produto.ValorDoPedido = produto.Preco * produto.QtdDoPedido;
                }
            }
        }

        public bool ProdutoExisteNoPedido(Produto produto, Conta conta)
        {
            if (conta.Pedido.Contains(produto))
                return true;
            return false;
        }

        public void AdicionarProdutoNoPedido(Produto produto, Conta conta, decimal quantidade)
        {
            ContextoDados contextoDados = new ContextoDados(carregarDados: true);
            repositorioProduto = new RepositorioProdutoEmArquivo(contextoDados);

            if (!ProdutoExisteNoPedido(produto, conta))
            {
                produto.QtdDoPedido = quantidade;
                conta.Pedido.Add(produto);
                CalcularValor(conta);
                repositorioProduto.MarcarPedido(produto);
                contextoDados.Salvar();
            }
        }

        public void RemoverProdutoDoPedido(Produto produto, Conta conta)
        {
            conta.Pedido.Remove(produto);
            repositorioProduto.DesmarcarPedido(produto);
            contextoDados.Salvar();
        }

        public void FecharConta(Conta conta)
        {
            conta.Status = "Fechada";
            conta.Mesa.Desocupar();
            contextoDados.Salvar();
        }

        public decimal FechamentoDiario()
        {
            DateTime dataAtual = DateTime.Now;
            decimal valorFechamento = 0;

            foreach (Conta conta in listaRegistros)
            {
                if (dataAtual.Day == conta.DataAbertura.Day)
                {
                    foreach (Produto produto in conta.Pedido)
                    {
                        valorFechamento += produto.ValorDoPedido;
                    }
                }
            }
            return valorFechamento;
        }

        public override int UltimoID()
        {
            int ultimoID = 0;
            List<Conta> contas = BuscarRegistros();

            foreach (Conta c in contas)
                ultimoID = c.Id;

            return ultimoID;
        }

        public override bool RegistroDuplicado(Conta registro)
        {
            return false;
        }

        public override void Editar(Conta registroAtual, Conta registroAtualizado)
        {
            throw new NotImplementedException();
        }
    }
}
