using ControleDeBar.Dominio.ModuloProduto;

namespace ControleDeBar.Dominio.ModuloConta
{
    public class ProdutoPedido : Produto
    {
        public decimal Quantidade { get; set; }

        public ProdutoPedido(string nome, decimal preco, decimal quantidade) : base(nome, preco)
        {
            Quantidade = quantidade;
        }
    }
}
