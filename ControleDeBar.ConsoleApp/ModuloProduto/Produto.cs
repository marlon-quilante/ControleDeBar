using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloProduto
{
    public class Produto : EntidadeBase<Produto>
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public Produto(string nome, decimal preco)
        {
            Nome = nome;
            Preco = preco;
        }

        public override string ValidacaoDeDados()
        {
            string erros = string.Empty;

            if (Nome.Length < 2 || Nome.Length > 100)
                erros += "O nome do produto precisa conter entre 2 e 100 caracteres!\n";
            if (!decimal.IsPositive(Preco) || !decimal.IsCanonical(Preco))
                erros += "O preço não é válido!";

            return erros;
        }
    }
}
