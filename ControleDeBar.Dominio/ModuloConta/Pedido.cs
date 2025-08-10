using ControleDeBar.Dominio.ModuloProduto;

namespace ControleDeBar.Dominio.ModuloConta
{
    public class Pedido
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorPedido { get; set; }

        public Pedido() { }

        public Pedido(Produto produto, decimal quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public Pedido(int id, Produto produto, decimal quantidade)
        {
            Id = id;
            Produto = produto;
            Quantidade = quantidade;
            ValorPedido = produto.Preco * quantidade;
        }
    }
}
