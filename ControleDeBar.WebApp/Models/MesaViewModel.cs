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
                DetalhesMesaViewModel detalhesVM = new DetalhesMesaViewModel(m.Numero, m.QtdLugares, m.Status);

                Registros.Add(detalhesVM);
            }
        }
    }

    public class DetalhesMesaViewModel
    {
        public int Numero { get; set; }
        public int QtdLugares { get; set; }
        public string Status { get; set; }

        public DetalhesMesaViewModel(int numero, int qtdLugares, string status)
        {
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

}
