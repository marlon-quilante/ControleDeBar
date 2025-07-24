using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloProduto;

namespace ControleDeBar.Infraestrutura.Memoria.ModuloConta
{
    public class RepositorioConta : RepositorioBase<Conta>
    {
        public override void Cadastrar(Conta conta)
        {
            base.Cadastrar(conta);

            CalcularValor(conta);
            conta._mesa.Ocupar();
            conta._mesa.TemPedido = true;
            conta._garcom.TemPedido = true;
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
