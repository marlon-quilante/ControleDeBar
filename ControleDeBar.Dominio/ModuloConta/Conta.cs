using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;

namespace ControleDeBar.Dominio.ModuloConta
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

        public void FecharConta()
        {
            Status = "Fechada";
            _mesa.Desocupar();
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
