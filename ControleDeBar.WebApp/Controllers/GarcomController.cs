using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloGarcom;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Controllers
{
    public class GarcomController : Controller
    {
        private readonly RepositorioGarcomEmArquivo repositorioGarcom;

        public GarcomController()
        {
            ContextoDados contextoDados = new ContextoDados(carregarDados: true);
            repositorioGarcom = new RepositorioGarcomEmArquivo(contextoDados);
        }

        public IActionResult Index()
        {
            List<Garcom> garcons = repositorioGarcom.BuscarRegistros();

            VisualizarGarconsViewModel visualizarVM = new VisualizarGarconsViewModel(garcons);

            return View(visualizarVM);
        }
    }
}
