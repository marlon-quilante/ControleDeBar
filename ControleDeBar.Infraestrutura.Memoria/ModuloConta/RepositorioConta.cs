using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Memoria.ModuloProduto;

namespace ControleDeBar.Infraestrutura.Memoria.ModuloConta
{
    public class RepositorioConta : RepositorioBase<Conta>
    {
        public RepositorioProduto repositorioProduto;

        public override void Cadastrar(Conta conta)
        {
            base.Cadastrar(conta);

            CalcularValorTotalConta(conta);
            conta.Mesa.Ocupar();
            conta.Mesa.TemPedido = true;
            conta.Garcom.TemPedido = true;
        }

        public void AdicionarPedido(Produto produto, Conta conta, decimal quantidade)
        {
            int idPedido = UltimoIDPedido() + 1;
            Pedido pedido = new Pedido(idPedido, produto, quantidade);
            conta.Pedidos.Add(pedido);
        }

        public void RemoverPedido(Produto produto, Conta conta)
        {
            foreach (Pedido p in conta.Pedidos)
            {
                if (p.Produto.Id == produto.Id)
                    conta.Pedidos.Remove(p);
            }
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

        public int UltimoIDPedido()
        {
            int ultimoID = 0;

            foreach (Conta c in listaRegistros)
            {
                foreach (Pedido p in c.Pedidos)
                    ultimoID = p.Id;
            }
            return ultimoID;
        }

        public bool ProdutoTemPedido(Produto produto)
        {
            foreach (Conta c in listaRegistros)
            {
                foreach (Pedido p in c.Pedidos)
                {
                    if (p.Produto.Id == produto.Id)
                        return true;
                }
            }
            return false;
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
