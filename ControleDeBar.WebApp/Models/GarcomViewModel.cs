using ControleDeBar.Dominio.ModuloGarcom;

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

    }

    public class EditarGarcomViewModel
    {

    }

    public class ExcluirGarcomViewModel
    {

    }
}
