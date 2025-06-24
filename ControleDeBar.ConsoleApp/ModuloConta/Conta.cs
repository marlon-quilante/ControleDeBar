using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloGarcom;
using ControleDeBar.ConsoleApp.ModuloMesa;
using ControleDeBar.ConsoleApp.ModuloProduto;

namespace ControleDeBar.ConsoleApp.ModuloConta
{
    public class Conta : EntidadeBase<Conta>
    {
        public List<Produto> Pedido { get; set; }
        public string NomeCliente { get; set; }
        public Mesa _mesa { get; set; }
        public Garcom _garcom { get; set; }
        public string Status { get; set; } = "Aberta";
        public DateTime DataAbertura { get; set; } = DateTime.Now;

        public Conta(string nomeCliente, Mesa mesa, Garcom garcom, List<Produto> pedido)
        {
            NomeCliente = nomeCliente;
            _mesa = mesa;
            _garcom = garcom;
            Pedido = pedido;
        }

        public override bool TemRestricaoDeExclusao()
        {
            throw new NotImplementedException();
        }

        public override string ValidacaoDeDados()
        {
            string erros = string.Empty;

            return erros;
        }
    }
}
