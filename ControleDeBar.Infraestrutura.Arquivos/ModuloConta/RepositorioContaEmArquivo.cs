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

        public void AdicionarPedido(Produto produto, Conta conta, decimal quantidade)
        {
            int idPedido = UltimoIDPedido() + 1;
            Pedido pedido = new Pedido(idPedido, produto, quantidade);
            conta.Pedidos.Add(pedido);
            contextoDados.Salvar();
        }

        public void RemoverPedido(Pedido pedido, Conta conta)
        {
            conta.Pedidos.Remove(pedido);
            contextoDados.Salvar();
        }

        public void CalcularValorTotalConta(Conta conta)
        {
            foreach (Pedido p in conta.Pedidos)
                conta.ValorTotalConta += p.ValorPedido;
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
                    valorFechamento += conta.ValorTotalConta;
                }
            }
            return valorFechamento;
        }

        public bool ProdutoTemPedido(Produto produto)
        {
            foreach (Conta c in BuscarRegistros())
            {
                foreach (Pedido p in c.Pedidos)
                {
                    if (p.Produto.Id == produto.Id)
                        return true;
                }
            }
            return false;
        }

        public override int UltimoID()
        {
            int ultimoID = 0;
            List<Conta> contas = BuscarRegistros();

            foreach (Conta c in contas)
                ultimoID = c.Id;

            return ultimoID;
        }

        public int UltimoIDPedido()
        {
            int ultimoID = 0;
            List<Conta> contas = BuscarRegistros();

            foreach (Conta c in contas)
            {
                foreach (Pedido p in c.Pedidos)
                    ultimoID = p.Id;
            }
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
