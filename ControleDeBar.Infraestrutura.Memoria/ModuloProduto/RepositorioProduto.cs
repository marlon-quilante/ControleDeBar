using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloProduto;

namespace ControleDeBar.Infraestrutura.Memoria.ModuloProduto
{
    public class RepositorioProduto : RepositorioBase<Produto>
    {
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
    }
}
