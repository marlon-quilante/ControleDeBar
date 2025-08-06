using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;
using System.ComponentModel.DataAnnotations;

namespace ControleDeBar.WebApp.Models
{
    public class VisualizarContasViewModel
    {
        public List<DetalhesContaViewModel> Registros { get; set; }

        public VisualizarContasViewModel() { }

        public VisualizarContasViewModel(List<Conta> contas)
        {
            Registros = new List<DetalhesContaViewModel>();

            foreach (Conta c in contas)
            {
                DetalhesContaViewModel detalhesVM = new DetalhesContaViewModel(c.Id, c.NomeCliente, c.Mesa, c.Garcom, c.Pedido, c.DataAbertura, c.Status);
                Registros.Add(detalhesVM);
            }
        }
    }

    public class DetalhesContaViewModel
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public Mesa Mesa { get; set; }
        public Garcom Garcom { get; set; }
        public List<Produto> Pedido { get; set; }
        public string Status { get; set; }
        public DateTime DataAbertura { get; set; }

        public DetalhesContaViewModel(int id, string nomeCliente, Mesa mesa, Garcom garcom, List<Produto> pedido, DateTime dataAbertura, string status)
        {
            Id = id;
            NomeCliente = nomeCliente;
            Mesa = mesa;
            Garcom = garcom;
            Pedido = pedido;
            DataAbertura = dataAbertura;
            Status = status;
        }
    }

    public class SelecionarMesaViewModel
    {
        public int Id { get; set; }
        public int Numero { get; set; }

        public SelecionarMesaViewModel(int id, int numero)
        {
            Id = id;
            Numero = numero;
        }
    }

    public class SelecionarGarcomViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public SelecionarGarcomViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class SelecionarProdutoViewModel 
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public SelecionarProdutoViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class CadastrarContaViewModel
    {
        [MinLength(2, ErrorMessage = "O nome do cliente possuir no mínimo 2 caracteres")]
        public string NomeCliente { get; set; }

        public DateTime DataAbertura { get; set; }
        public int GarcomID { get; set; }
        public int MesaID { get; set; }
        public int ProdutoID { get; set; }
        public List<SelecionarMesaViewModel> Mesas { get; set; }
        public List<SelecionarGarcomViewModel> Garcons { get; set; }
        public List<SelecionarProdutoViewModel> Produtos { get; set; }

        public CadastrarContaViewModel()
        {
            Garcons = new List<SelecionarGarcomViewModel>();
            Mesas = new List<SelecionarMesaViewModel>();
            Produtos = new List<SelecionarProdutoViewModel>();
        }

        public CadastrarContaViewModel(List<Mesa> mesas, List<Garcom> garcons, List<Produto> produtos) : this()
        {
            foreach (Garcom g in garcons)
            {
                SelecionarGarcomViewModel selecionarEquipamentoVM = new SelecionarGarcomViewModel(g.Id, g.Nome);

                Garcons.Add(selecionarEquipamentoVM);
            }

            foreach (Mesa m in mesas)
            {
                SelecionarMesaViewModel selecionarMesaVM = new SelecionarMesaViewModel(m.Id, m.Numero);

                Mesas.Add(selecionarMesaVM);
            }

            foreach (Produto p in produtos)
            {
                SelecionarProdutoViewModel selecionarProdutoVM = new SelecionarProdutoViewModel(p.Id, p.Nome);

                Produtos.Add(selecionarProdutoVM);
            }
        }
    }

    public class FecharContaViewModel 
    {
        public int Id {  get; set; }
        public string NomeCliente { get; set; }

        public FecharContaViewModel(int id, string nomeCliente)
        {
            Id = id;
            NomeCliente = nomeCliente;
        }
    }
}
