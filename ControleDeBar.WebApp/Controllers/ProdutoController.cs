using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloProduto;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly RepositorioProdutoEmArquivo repositorioProduto;

        public ProdutoController()
        {
            ContextoDados contextoDados = new ContextoDados(carregarDados: true);
            repositorioProduto = new RepositorioProdutoEmArquivo(contextoDados);
        }

        public IActionResult Index()
        {
            List<Produto> produtos = repositorioProduto.BuscarRegistros();

            VisualizarProdutosViewModel visualizarVM = new VisualizarProdutosViewModel(produtos);

            return View(visualizarVM);
        }

        public IActionResult Cadastrar()
        {
            CadastrarProdutoViewModel cadastrarVM = new CadastrarProdutoViewModel();

            return View(cadastrarVM);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarProdutoViewModel cadastrarVM)
        {
            if (!ModelState.IsValid)
                return View(cadastrarVM);

            Produto novoProduto = new Produto(cadastrarVM.Nome, cadastrarVM.Preco);

            repositorioProduto.Cadastrar(novoProduto);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(int id)
        {
            Produto produto = repositorioProduto.BuscarRegistroPorID(id);

            EditarProdutoViewModel editarVM = new EditarProdutoViewModel(produto.Id, produto.Nome, produto.Preco);

            return View(editarVM);
        }

        [HttpPost]
        public IActionResult Editar(int id, EditarProdutoViewModel editarVM)
        {
            if (!ModelState.IsValid)
                return View(editarVM);

            Produto produtoAtual = repositorioProduto.BuscarRegistroPorID(id);
            Produto produtoAtualizado = new Produto(editarVM.Nome, editarVM.Preco);

            repositorioProduto.Editar(produtoAtual, produtoAtualizado);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(int id)
        {
            Produto produto = repositorioProduto.BuscarRegistroPorID(id);

            ExcluirProdutoViewModel excluirVM = new ExcluirProdutoViewModel(produto.Id, produto.Nome);

            return View(excluirVM);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int id)
        {
            Produto produto = repositorioProduto.BuscarRegistroPorID(id);

            repositorioProduto.Excluir(produto);

            return RedirectToAction(nameof(Index));
        }
    }
}
