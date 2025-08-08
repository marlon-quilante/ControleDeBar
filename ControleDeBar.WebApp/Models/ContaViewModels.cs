using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                DetalhesContaViewModel detalhesVM = new DetalhesContaViewModel(c.Id, c.NomeCliente, c.Mesa, c.Garcom, c.Pedidos, c.DataAbertura, c.Status, c.ValorTotalConta);
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
        public List<Pedido> Pedidos { get; set; }
        public string Status { get; set; }
        public DateTime DataAbertura { get; set; }
        public decimal ValorTotalConta { get; set; }

        public DetalhesContaViewModel()
        {
            Pedidos = new List<Pedido>();
        }

        public DetalhesContaViewModel(int id, string nomeCliente, Mesa mesa, Garcom garcom, List<Pedido> pedidos, DateTime dataAbertura, string status, decimal valorTotalConta)
        {
            Id = id;
            NomeCliente = nomeCliente;
            Mesa = mesa;
            Garcom = garcom;
            Pedidos = pedidos;
            DataAbertura = dataAbertura;
            Status = status;
            ValorTotalConta = valorTotalConta;
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
        public List<SelectListItem> MesasDisponiveis { get; set; }
        public List<SelectListItem> GarconsDisponiveis { get; set; }

        public CadastrarContaViewModel()
        {
            GarconsDisponiveis = new List<SelectListItem>();
            MesasDisponiveis = new List<SelectListItem>();
        }

        public CadastrarContaViewModel(List<Mesa> mesas, List<Garcom> garcons) : this()
        {
            foreach (Garcom g in garcons)
            {
                SelectListItem garcomDisponivel = new SelectListItem(g.Nome.ToString(), g.Id.ToString());

                GarconsDisponiveis.Add(garcomDisponivel);
            }

            foreach (Mesa m in mesas)
            {
                if (m.Status == "Livre")
                {
                    SelectListItem mesaDisponivel = new SelectListItem(m.Numero.ToString(), m.Id.ToString());
                    MesasDisponiveis.Add(mesaDisponivel);
                }
            }
        }
    }

    public class FecharContaViewModel 
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public Mesa Mesa { get; set; }
        public Garcom Garcom { get; set; }
        public List<Pedido> Pedidos { get; set; }
        public string Status { get; set; }
        public DateTime DataAbertura { get; set; }
        public decimal ValorTotalConta { get; set; }

        public FecharContaViewModel(int id, string nomeCliente, Mesa mesa, Garcom garcom, List<Pedido> pedidos, DateTime dataAbertura, decimal valorTotalConta)
        {
            Id = id;
            NomeCliente = nomeCliente;
            Mesa = mesa;
            Garcom = garcom;
            Pedidos = pedidos;
            DataAbertura = dataAbertura;
            ValorTotalConta = valorTotalConta;
        }
    }
}
