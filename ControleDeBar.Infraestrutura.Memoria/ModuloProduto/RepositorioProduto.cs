using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Memoria.ModuloConta;

namespace ControleDeBar.Infraestrutura.Memoria.ModuloProduto
{
    public class RepositorioProduto : RepositorioBase<Produto>
    {
        public RepositorioConta repositorioConta;

        public override bool RegistroDuplicado(Produto produto)
        {
            foreach (Produto p in listaRegistros)
            {
                if (produto.Nome == p.Nome && produto.Id != p.Id)
                    return true;
            }
            return false;
        }

        public override void Editar(Produto produtoAtual, Produto produtoAtualizado)
        {
            produtoAtual.Nome = produtoAtualizado.Nome;
            produtoAtual.Preco = produtoAtualizado.Preco;
        }

        public void MarcarPedido(Produto produto)
        {
            produto.TemPedido = true;
        }

        public void DesmarcarPedido(Produto produto)
        {
            foreach (Conta c in repositorioConta.listaRegistros)
            {
                if (repositorioConta.ProdutoTemPedido(produto))
                    return;

                produto.TemPedido = false;
            }
        }
    }
}
