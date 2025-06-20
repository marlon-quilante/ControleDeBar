using ControleDeBar.ConsoleApp.ModuloMesa;

namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public class TelaPrincipal
    {
        private string opcaoEscolhida;
        public bool fecharSistema = false;

        public RepositorioMesa<Mesa> repositorioMesa;
        public TelaMesa telaMesa;

        public TelaPrincipal()
        {
            repositorioMesa = new RepositorioMesa<Mesa>();
            telaMesa = new TelaMesa(repositorioMesa);
        }

        public void EscolherOpcaoMenuPrincipal()
        {
            Console.WriteLine("---------------");
            Console.WriteLine("Controle de Bar");
            Console.WriteLine("---------------");

            Console.WriteLine();
            Console.WriteLine("1- Controle de Mesas");
            Console.WriteLine("S- Sair");
            Console.WriteLine();

            Console.Write("Selecione a opção: ");
            opcaoEscolhida = Console.ReadLine();
        }

        public ITela TelaEscolhida()
        {
            if (opcaoEscolhida == "1") 
                return telaMesa;
            if (opcaoEscolhida == "S".ToLower())
            {
                fecharSistema = true;
                return null;
            }
            else
                return null;
        }
    }
}
