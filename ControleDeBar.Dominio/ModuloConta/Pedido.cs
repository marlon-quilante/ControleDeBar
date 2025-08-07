namespace ControleDeBar.Dominio.ModuloConta
{
    public class Pedido
    {
        public List<ProdutoPedido> Produtos { get; set; }
        public decimal ValorTotal { get; set; }

        public Pedido() { }

        public Pedido(List<ProdutoPedido> produtos)
        {
            Produtos = produtos;
        }
    }
}
