using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloMesa;

namespace ControleDeBar.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelaPrincipal telaPrincipal = new TelaPrincipal();

            while (true)
            {
                Console.Clear();
                telaPrincipal.EscolherOpcaoMenuPrincipal();
                ITela telaEscolhida = telaPrincipal.TelaEscolhida();

                if (telaPrincipal.fecharSistema) break;
                if (telaEscolhida == null) continue;

                string opcaoEscolhida = telaEscolhida.EscolherOpcaoMenu();

                switch (int.Parse(opcaoEscolhida))
                {
                    case 1: telaEscolhida.Cadastrar(); break;
                    case 2: telaEscolhida.Visualizar(); break;
                    case 3: telaEscolhida.Editar(); break;
                    case 4: telaEscolhida.Excluir(); break;
                    default: break;
                }
            }

        }
    }
}