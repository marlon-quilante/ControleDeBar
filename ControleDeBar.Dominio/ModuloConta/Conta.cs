using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;

namespace ControleDeBar.Dominio.ModuloConta
{
    public class Conta : EntidadeBase<Conta>
    {
        public List<Pedido> Pedidos { get; set; }
        public decimal ValorTotalConta { get; set; }
        public string NomeCliente { get; set; }
        public Mesa Mesa { get; set; }
        public Garcom Garcom { get; set; }
        public string Status { get; set; } = "Aberta";
        public DateTime DataAbertura { get; set; } = DateTime.UtcNow;

        public Conta() { }

        public Conta(string nomeCliente, Mesa mesa, Garcom garcom)
        {
            NomeCliente = nomeCliente;
            Mesa = mesa;
            Garcom = garcom;
            Pedidos = new List<Pedido>();
        }

        public override bool TemRestricaoDeExclusao()
        {
            throw new NotImplementedException();
        }

        public override string ValidacaoDeDados()
        {
            string erros = string.Empty;

            if (NomeCliente.Length < 3 && NomeCliente.Length > 100)
                erros += "O nome do cliente precisa conter entre 3 e 100 caracteres!\n";

            return erros;
        }
    }
}
