using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloConta;
using ControleDeBar.Infraestrutura.Arquivos.ModuloGarcom;
using ControleDeBar.Infraestrutura.Arquivos.ModuloMesa;
using ControleDeBar.Infraestrutura.Arquivos.ModuloProduto;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Controllers
{
    public class ContaController : Controller
    {
        private readonly RepositorioContaEmArquivo repositorioConta;
        private readonly RepositorioMesaEmArquivo repositorioMesa;
        private readonly RepositorioGarcomEmArquivo repositorioGarcom;
        private readonly RepositorioProdutoEmArquivo repositorioProduto;

        public ContaController()
        {
            ContextoDados contextoDados = new ContextoDados(carregarDados: true);
            repositorioConta = new RepositorioContaEmArquivo(contextoDados);
            repositorioMesa = new RepositorioMesaEmArquivo(contextoDados);
            repositorioGarcom = new RepositorioGarcomEmArquivo(contextoDados);
            repositorioProduto = new RepositorioProdutoEmArquivo(contextoDados);
        }

        public IActionResult Index()
        {
            List<Conta> contas = repositorioConta.BuscarRegistros();

            VisualizarContasViewModel visualizarVM = new VisualizarContasViewModel(contas);

            return View(visualizarVM);
        }

        public IActionResult Detalhes(int id)
        {
            Conta contaSelecionada = repositorioConta.BuscarRegistroPorID(id);

            DetalhesContaViewModel detalhesVM = new DetalhesContaViewModel(
                contaSelecionada.Id,
                contaSelecionada.NomeCliente, 
                contaSelecionada.Mesa, 
                contaSelecionada.Garcom, 
                contaSelecionada.Pedidos, 
                contaSelecionada.DataAbertura, 
                contaSelecionada.Status,
                contaSelecionada.ValorTotalConta);

            if (detalhesVM.Pedidos == null)
                detalhesVM.Pedidos = new List<Pedido>();

            return View(detalhesVM);
        }

        public IActionResult Cadastrar()
        {
            List<Mesa> mesas = repositorioMesa.BuscarRegistros();
            List<Garcom> garcons = repositorioGarcom.BuscarRegistros();

            CadastrarContaViewModel cadastrarVM = new CadastrarContaViewModel(mesas, garcons);

            return View(cadastrarVM);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarContaViewModel cadastrarVM)
        {
            if (!ModelState.IsValid)
            {
                List<Mesa> mesas = repositorioMesa.BuscarRegistros();
                List<Garcom> garcons = repositorioGarcom.BuscarRegistros();
                List<Produto> produtos = repositorioProduto.BuscarRegistros();
                    
                cadastrarVM = new CadastrarContaViewModel(mesas, garcons);
                
                return View(cadastrarVM);
            }

            Mesa mesaSelecionada = repositorioMesa.BuscarRegistroPorID(cadastrarVM.MesaID);
            Garcom garcomSelecionado = repositorioGarcom.BuscarRegistroPorID(cadastrarVM.GarcomID);

            Conta conta = new Conta(cadastrarVM.NomeCliente, mesaSelecionada, garcomSelecionado);
            conta.DataAbertura = DateTime.Now;

            repositorioConta.Cadastrar(conta);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Fechar(int id)
        {
            Conta contaSelecionada = repositorioConta.BuscarRegistroPorID(id);

            if (contaSelecionada.Status == "Fechada")
                return RedirectToAction(nameof(Index));

            FecharContaViewModel fecharVM = new FecharContaViewModel(
                contaSelecionada.Id,
                contaSelecionada.NomeCliente,
                contaSelecionada.Mesa,
                contaSelecionada.Garcom,
                contaSelecionada.Pedidos,
                contaSelecionada.DataAbertura,
                contaSelecionada.ValorTotalConta);

            if (fecharVM.Pedidos == null)
                fecharVM.Pedidos = new List<Pedido>();

            return View(fecharVM);
        }

        [HttpPost]
        public IActionResult FecharConfirmado(int id)
        {
            Conta contaSelecionada = repositorioConta.BuscarRegistroPorID(id);

            repositorioConta.FecharConta(contaSelecionada);

            return RedirectToAction(nameof(Index));
        }
    }
}
