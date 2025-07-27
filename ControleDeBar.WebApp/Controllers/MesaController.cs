using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloMesa;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Controllers
{
    public class MesaController : Controller
    {
        private RepositorioMesaEmArquivo repositorioMesa { get; set; }

        public MesaController()
        {
            ContextoDados contextoDados = new ContextoDados(carregarDados: true);
            repositorioMesa = new RepositorioMesaEmArquivo(contextoDados);
        }


        public IActionResult Index()
        {
            List<Mesa> mesas = repositorioMesa.BuscarRegistros();

            VisualizarMesasViewModel visualizarVM = new VisualizarMesasViewModel(mesas);

            return View(visualizarVM);
        }

        public IActionResult Cadastrar()
        {
            CadastrarMesaViewModel cadastrarVM = new CadastrarMesaViewModel();

            return View(cadastrarVM);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarMesaViewModel cadastrarVM)
        {
            Mesa novaMesa = new Mesa(cadastrarVM.Numero, cadastrarVM.QtdLugares);

            repositorioMesa.Cadastrar(novaMesa);

            return RedirectToAction(nameof(Index));
        }
    }
}
