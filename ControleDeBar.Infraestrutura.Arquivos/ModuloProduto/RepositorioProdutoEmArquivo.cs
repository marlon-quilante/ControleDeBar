using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeBar.Infraestrutura.Arquivos.ModuloProduto
{
    public class RepositorioProdutoEmArquivo : RepositorioBaseEmArquivo<Produto>
    {
        public RepositorioProdutoEmArquivo(ContextoDados contextoDados) : base(contextoDados)
        {
        }

        public override List<Produto> BuscarRegistros()
        {
            return contextoDados.Produtos;
        }

        public override int UltimoID()
        {
            int ultimoID = 0;
            List<Produto> produtos = BuscarRegistros();

            foreach (Produto p in produtos)
                ultimoID = p.Id;

            return ultimoID;
        }

        public override void Cadastrar(Produto produto)
        {
            base.Cadastrar(produto);
        }

        public override void Editar(Produto produtoAtual, Produto produtoAtualizado)
        {
            if (produtoAtual != null && produtoAtualizado != null)
            {
                produtoAtual.Nome = produtoAtualizado.Nome;
                produtoAtual.Preco = produtoAtualizado.Preco;
            }
            contextoDados.Salvar();
        }

        public override bool RegistroDuplicado(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}
