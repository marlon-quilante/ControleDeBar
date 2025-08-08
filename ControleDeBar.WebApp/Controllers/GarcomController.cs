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

        public IActionResult Cadastrar()
        {
            CadastrarGarcomViewModel cadastrarVM = new CadastrarGarcomViewModel();

            return View(cadastrarVM);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarGarcomViewModel cadastrarVM)
        {
            if (!ModelState.IsValid)
                return View(cadastrarVM);

            Garcom novoGarcom = new Garcom(cadastrarVM.Nome, cadastrarVM.CPF);

            repositorioGarcom.Cadastrar(novoGarcom);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(int id)
        {
            Garcom garcom = repositorioGarcom.BuscarRegistroPorID(id);

            EditarGarcomViewModel editarVM = new EditarGarcomViewModel(garcom.Id, garcom.Nome, garcom.CPF);

            return View(editarVM);
        }

        [HttpPost]
        public IActionResult Editar(int id, EditarGarcomViewModel editarVM)
        {
            if (!ModelState.IsValid)
                return View(editarVM);

            Garcom garcomAtual = repositorioGarcom.BuscarRegistroPorID(id);
            Garcom garcomAtualizado = new Garcom(editarVM.Nome, editarVM.CPF);

            repositorioGarcom.Editar(garcomAtual, garcomAtualizado);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(int id)
        {
            Garcom garcom = repositorioGarcom.BuscarRegistroPorID(id);

            ExcluirGarcomViewModel excluirVM = new ExcluirGarcomViewModel(garcom.Id, garcom.Nome);

            return View(excluirVM);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int id)
        {
            Garcom garcom = repositorioGarcom.BuscarRegistroPorID(id);

            repositorioGarcom.Excluir(garcom);

            return RedirectToAction(nameof(Index));
        }
    }
}
