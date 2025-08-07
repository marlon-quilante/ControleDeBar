using ControleDeBar.Dominio.ModuloConta;
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

            conta.Mesa.Ocupar();
            conta.Mesa.TemPedido = true;
            conta.Garcom.TemPedido = true;
            contextoDados.Salvar();
        }

        public void CalcularValorPedido(Conta conta)
        {
            foreach (ProdutoPedido produtoPedido in conta.Pedido.Produtos)
            {
                conta.Pedido.ValorTotal += produtoPedido.Preco * produtoPedido.Quantidade;
            }
        }

        public bool ProdutoExisteNoPedido(Produto produto, Conta conta)
        {
            if (conta.Pedido.Produtos.Contains(produto))
                return true;
            return false;
        }

        public void AdicionarProdutoNoPedido(Produto produto, Conta conta)
        {
            if (!ProdutoExisteNoPedido(produto, conta))
            {
                conta.Pedido.Produtos.Add((ProdutoPedido)produto);
                contextoDados.Salvar();
            }
        }

        public void RemoverProdutoDoPedido(Produto produto, Conta conta)
        {
            if (ProdutoExisteNoPedido(produto, conta))
            {
                conta.Pedido.Produtos.Remove((ProdutoPedido)produto);
                contextoDados.Salvar();
            }
        }

        public void FecharConta(Conta conta)
        {
            conta.Status = "Fechada";
            conta.Mesa.Desocupar();
            contextoDados.Salvar();
        }

        public decimal ValorFechamentoDiario()
        {
            DateTime dataAtual = DateTime.Now;
            decimal valorFechamento = 0;

            foreach (Conta conta in listaRegistros)
            {
                if (dataAtual.Day == conta.DataAbertura.Day)
                {
                    valorFechamento += conta.Pedido.ValorTotal;
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
