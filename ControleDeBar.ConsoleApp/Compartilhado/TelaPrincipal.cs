using ControleDeBar.ConsoleApp.ModuloGarcom;
using ControleDeBar.ConsoleApp.ModuloMesa;

namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public class TelaPrincipal
    {
        private string opcaoEscolhida;
        public bool fecharSistema = false;

        public RepositorioMesa<Mesa> repositorioMesa;
        public TelaMesa telaMesa;

        public RepositorioGarcom<Garcom> repositorioGarcom;
        public TelaGarcom telaGarcom;

        public TelaPrincipal()
        {
            repositorioMesa = new RepositorioMesa<Mesa>();
            telaMesa = new TelaMesa(repositorioMesa);

            repositorioGarcom = new RepositorioGarcom<Garcom>();
            telaGarcom = new TelaGarcom(repositorioGarcom);
        }

        public void EscolherOpcaoMenuPrincipal()
        {
            Console.WriteLine("---------------");
            Console.WriteLine("Controle de Bar");
            Console.WriteLine("---------------");

            Console.WriteLine();
            Console.WriteLine("1- Controle de Mesas");
            Console.WriteLine("2- Controle de Garçons");
            Console.WriteLine("S- Sair");
            Console.WriteLine();

            Console.Write("Selecione a opção: ");
            opcaoEscolhida = Console.ReadLine();
        }

        public ITela TelaEscolhida()
        {
            if (opcaoEscolhida == "1") 
                return telaMesa;
            if (opcaoEscolhida == "2")
                return telaGarcom;
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
