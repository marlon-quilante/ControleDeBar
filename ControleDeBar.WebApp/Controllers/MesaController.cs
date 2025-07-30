using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloMesa;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Controllers
{
    public class MesaController : Controller
    {
        private readonly RepositorioMesaEmArquivo repositorioMesa;

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
            if (!ModelState.IsValid)
                return View(cadastrarVM);

            Mesa novaMesa = new Mesa(cadastrarVM.Numero, cadastrarVM.QtdLugares);

            repositorioMesa.Cadastrar(novaMesa);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(int id)
        {
            Mesa mesa = repositorioMesa.BuscarRegistroPorID(id);

            EditarMesaViewModel editarVM = new EditarMesaViewModel(mesa.Id, mesa.Numero, mesa.QtdLugares);

            return View(editarVM);
        }

        [HttpPost]
        public IActionResult Editar(int id, EditarMesaViewModel editarVM)
        {
            id = editarVM.Id;
            Mesa mesaAtual = repositorioMesa.BuscarRegistroPorID(id);
            Mesa mesaAtualizada = new Mesa(editarVM.Numero, editarVM.QtdLugares);

            repositorioMesa.Editar(mesaAtual, mesaAtualizada);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(int id)
        {
            Mesa mesa = repositorioMesa.BuscarRegistroPorID(id);

            ExcluirMesaViewModel excluirVM = new ExcluirMesaViewModel(mesa.Id, mesa.Numero);

            return View(excluirVM);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int id)
        {
            Mesa mesa = repositorioMesa.BuscarRegistroPorID(id);

            repositorioMesa.Excluir(mesa);

            return RedirectToAction(nameof(Index));
        }
    }
}
