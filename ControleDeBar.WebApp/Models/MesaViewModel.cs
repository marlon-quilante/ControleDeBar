using ControleDeBar.Dominio.ModuloMesa;

namespace ControleDeBar.WebApp.Models
{
    public class VisualizarMesasViewModel
    {
        public List<DetalhesMesaViewModel> Registros { get; set; }

        public VisualizarMesasViewModel(List<Mesa> mesas)
        {
            Registros = new List<DetalhesMesaViewModel>();

            foreach(Mesa m in mesas)
            {
                DetalhesMesaViewModel detalhesVM = new DetalhesMesaViewModel(m.Id, m.Numero, m.QtdLugares, m.Status);

                Registros.Add(detalhesVM);
            }
        }
    }

    public class DetalhesMesaViewModel
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int QtdLugares { get; set; }
        public string Status { get; set; }

        public DetalhesMesaViewModel(int id, int numero, int qtdLugares, string status)
        {
            Id = id;
            Numero = numero;
            QtdLugares = qtdLugares;
            Status = status;
        }
    }

    public class CadastrarMesaViewModel 
    {
        public int Numero { get; set; }
        public int QtdLugares { get; set; }

        public CadastrarMesaViewModel()
        {
        }
    }

    public class EditarMesaViewModel
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int QtdLugares { get; set; }

        public EditarMesaViewModel() { }

        public EditarMesaViewModel(int id, int numero, int qtdLugares)
        {
            Id = id;
            Numero = numero;
            QtdLugares = qtdLugares;
        }
    }

    public class ExcluirMesaViewModel 
    {
        public int Id { get; set; }
        public int Numero { get; set; }

        public ExcluirMesaViewModel() { }

        public ExcluirMesaViewModel(int id, int numero)
        {
            Id = id;
            Numero = numero;
        }
    }
}
