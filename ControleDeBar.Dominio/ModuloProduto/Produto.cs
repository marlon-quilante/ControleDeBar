using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloConta;

namespace ControleDeBar.Dominio.ModuloProduto
{
    public class Produto : EntidadeBase<Produto>
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public decimal QtdDoPedido { get; set; } = 0;
        public decimal ValorDoPedido { get; set; } = 0;
        public bool TemPedido { get; set; }

        public Produto(string nome, decimal preco)
        {
            Nome = nome;
            Preco = preco;
        }

        public void MarcarPedido()
        {
            TemPedido = true;
        }

        public void DesmarcarPedido()
        {
            TemPedido = false;
        }

        public override bool TemRestricaoDeExclusao()
        {
            if (TemPedido)
                return true;
            return false;
        }

        public override string ValidacaoDeDados()
        {
            string erros = string.Empty;

            if (Nome.Length < 2 || Nome.Length > 100)
                erros += "O nome do produto precisa conter entre 2 e 100 caracteres!\n";
            if (!decimal.IsPositive(Preco))
                erros += "O preço não é válido!";

            return erros;
        }
    }
}
