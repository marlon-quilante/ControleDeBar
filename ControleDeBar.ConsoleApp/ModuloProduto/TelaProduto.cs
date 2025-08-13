using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Memoria.ModuloProduto;

namespace ControleDeBar.ConsoleApp.ModuloProduto
{
    public class TelaProduto : TelaBase<Produto>, ITela
    {
        public RepositorioProduto repositorioProduto;

        public TelaProduto(RepositorioProduto repositorioProduto) : base("Produto", repositorioProduto)
        {
            this.repositorioProduto = repositorioProduto;
        }

        public override Produto ObterDados()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();
            Console.Write("Digite o preço: ");
            decimal preco = decimal.Parse(Console.ReadLine());

            return new Produto(nome, preco);
        }

        public override void Visualizar()
        {
            base.Visualizar();

            Console.WriteLine("{0,-5} | {1,-20} | {2,-10}",
                "ID", "Nome", "Preço");
            foreach (Produto produto in repositorioProduto.listaRegistros)
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-10}",
                produto.Id, produto.Nome, produto.Preco.ToString("C2"));
            }
            Console.WriteLine();
            ApresentarMensagem("Pressione ENTER para continuar...", ConsoleColor.Yellow);
        }
    }
}
