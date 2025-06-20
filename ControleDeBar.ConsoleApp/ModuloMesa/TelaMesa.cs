using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    public class TelaMesa : TelaBase<Mesa>, ITela
    {
        RepositorioMesa<Mesa> repositorioMesa;
        private int numeroMesa = 0;
        private bool numeroMesaValido = false;
        private int qtdLugares = 0;
        private bool qtdLugaresValido = false;

        public TelaMesa(RepositorioMesa<Mesa> repositorioMesa) : base("Mesa", repositorioMesa)
        {
            this.repositorioMesa = repositorioMesa;
        }

        public override void Visualizar()
        {
            base.Visualizar();

            Console.WriteLine("{0,-5} | {1,-7} | {2,-10} | {3, -10}", 
                "ID", "Número", "Capacidade", "Status");
            foreach (Mesa mesa in repositorioMesa.listaRegistros)
            {
                Console.WriteLine("{0,-5} | {1,-7} | {2,-10} | {3, -10}",
                mesa.Id, mesa.Numero, mesa.QtdLugares, mesa.Status);
            }
            Console.Write("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }

        public override Mesa ObterDados()
        {
            while (!numeroMesaValido || !qtdLugaresValido)
            {
                Console.Write("Digite o número da mesa: ");
                numeroMesaValido = int.TryParse(Console.ReadLine(), out numeroMesa);
                Console.Write("Digite a quantidade de lugares da mesa: ");
                qtdLugaresValido = int.TryParse(Console.ReadLine(), out qtdLugares);

                if (!numeroMesaValido || !qtdLugaresValido)
                {
                    Console.WriteLine("\nDados inválidos! Pressione ENTER para tentar novamente...");
                    Console.ReadLine();
                }
            }
            numeroMesaValido = false;
            qtdLugaresValido = false;
            return new Mesa(numeroMesa, qtdLugares);
        }
    }
}
