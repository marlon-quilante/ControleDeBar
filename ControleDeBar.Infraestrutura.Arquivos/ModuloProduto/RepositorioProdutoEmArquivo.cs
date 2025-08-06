using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloConta;

namespace ControleDeBar.Infraestrutura.Arquivos.ModuloProduto
{
    public class RepositorioProdutoEmArquivo : RepositorioBaseEmArquivo<Produto>
    {
        private RepositorioContaEmArquivo repositorioConta;

        public RepositorioProdutoEmArquivo(ContextoDados contextoDados) :base(contextoDados)
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
            contextoDados.Salvar();
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

        public void MarcarPedido(Produto produto)
        {
            produto.TemPedido = true;
        }

        public void DesmarcarPedido(Produto produto)
        {
            ContextoDados contextoDados = new ContextoDados(carregarDados: true);
            repositorioConta = new RepositorioContaEmArquivo(contextoDados);

            List<Conta> contas = repositorioConta.BuscarRegistros(); 

            foreach (Conta c in contas)
            {
                if (repositorioConta.ProdutoExisteNoPedido(produto, c))
                    return;

                produto.TemPedido = false;
            }
        }

        public override bool RegistroDuplicado(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}
