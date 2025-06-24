using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    public class Mesa : EntidadeBase<Mesa>
    {
        public int Numero { get; set; }
        public int QtdLugares { get; set; }
        public string Status { get; set; } = "Livre";
        public bool TemPedido { get; set; } = false;

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

        public override bool TemRestricaoDeExclusao()
        {
            if (TemPedido)
                return true;
            return false;
        }

        public override string ValidacaoDeDados()
        {
            string erros = string.Empty;

            if (!int.IsPositive(Numero))
                erros += "O número da mesa precisa ser um número positivo!\n";
            if (!int.IsPositive(QtdLugares))
                erros += "A quantidade de lugares precisa ser um número positivo!";

            return erros;
        }
    }
}
