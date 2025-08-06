using ControleDeBar.Dominio.ModuloGarcom;
using System.ComponentModel.DataAnnotations;

namespace ControleDeBar.WebApp.Models
{
    public class VisualizarGarconsViewModel
    {
        public List<DetalhesGarcomViewModel> Registros {  get; set; }

        public VisualizarGarconsViewModel()
        {
        }

        public VisualizarGarconsViewModel(List<Garcom> garcons)
        {
            Registros = new List<DetalhesGarcomViewModel>();

            foreach (Garcom g in garcons)
            {
                DetalhesGarcomViewModel detalhesVM = new DetalhesGarcomViewModel(g.Id, g.Nome, g.CPF);
                Registros.Add(detalhesVM);
            }
        }
    }

    public class DetalhesGarcomViewModel 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }

        public DetalhesGarcomViewModel() { }

        public DetalhesGarcomViewModel(int id, string nome, string cpf)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
        }
    }

    public class CadastrarGarcomViewModel 
    {
        [MinLength(2, ErrorMessage = "O nome do garçom deve possuir no mínimo 2 caracteres")]
        public string Nome { get; set; }

        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato xxx.xxx.xxx-xx")]
        public string CPF { get; set; }

        public CadastrarGarcomViewModel() { }

        public CadastrarGarcomViewModel(string nome, string cpf)
        {
            Nome = nome;
            CPF = cpf;
        }
    }

    public class EditarGarcomViewModel
    {
        public int Id { get; set; }

        [MinLength(2, ErrorMessage = "O nome do garçom deve possuir no mínimo 2 caracteres")]
        public string Nome { get; set; }

        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato xxx.xxx.xxx-xx")]
        public string CPF { get; set; }

        public EditarGarcomViewModel() { }

        public EditarGarcomViewModel(int id, string nome, string cpf)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
        }
    }

    public class ExcluirGarcomViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ExcluirGarcomViewModel() { }

        public ExcluirGarcomViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
