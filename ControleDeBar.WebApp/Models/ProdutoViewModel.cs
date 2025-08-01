using ControleDeBar.Dominio.ModuloProduto;
using System.ComponentModel.DataAnnotations;

namespace ControleDeBar.WebApp.Models
{
    public class VisualizarProdutosViewModel
    {
        public List<DetalhesProdutoViewModel> Registros { get; set; }

        public VisualizarProdutosViewModel()
        {
        }

        public VisualizarProdutosViewModel(List<Produto> produtos)
        {
            Registros = new List<DetalhesProdutoViewModel>();

            foreach (Produto p in produtos)
            {
                DetalhesProdutoViewModel detalhesVM = new DetalhesProdutoViewModel(p.Id, p.Nome, p.Preco);
                Registros.Add(detalhesVM);
            }
        }
    }

    public class DetalhesProdutoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public DetalhesProdutoViewModel() { }

        public DetalhesProdutoViewModel(int id, string nome, decimal preco)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
        }
    }

    public class CadastrarProdutoViewModel
    {
        [MinLength(2, ErrorMessage = "O nome do produto deve haver no mínimo 2 caracteres")]
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public CadastrarProdutoViewModel() { }

        public CadastrarProdutoViewModel(string nome, decimal preco)
        {
            Nome = nome;
            Preco = preco;
        }
    }

    public class EditarProdutoViewModel
    {
        public int Id { get; set; }

        [MinLength(2, ErrorMessage = "O nome do produto deve haver no mínimo 2 caracteres")]
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public EditarProdutoViewModel() { }

        public EditarProdutoViewModel(int id, string nome, decimal preco)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
        }
    }

    public class ExcluirProdutoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ExcluirProdutoViewModel() { }

        public ExcluirProdutoViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
