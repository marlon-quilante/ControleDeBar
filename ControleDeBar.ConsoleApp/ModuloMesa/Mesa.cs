using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    public class Mesa : EntidadeBase<Mesa>
    {
        public int Numero { get; set; }
        public int QtdLugares { get; set; }
        public string Status { get; set; } = "Livre";

        public Mesa(int numero, int qtdLugares)
        {
            Numero = numero;
            QtdLugares = qtdLugares;
        }

        public void Ocupar()
        {
            Status = "Ocupada";
        }

        public void Desocupar()
        {
            Status = "Livre";
        }

        public override string ValidacaoDeDados(Mesa mesa)
        {
            string erros = string.Empty;
            return erros;
        }
    }
}
