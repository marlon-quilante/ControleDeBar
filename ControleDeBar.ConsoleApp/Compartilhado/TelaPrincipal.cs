using ControleDeBar.ConsoleApp.ModuloConta;
using ControleDeBar.ConsoleApp.ModuloGarcom;
using ControleDeBar.ConsoleApp.ModuloMesa;
using ControleDeBar.ConsoleApp.ModuloProduto;
using ControleDeBar.Infraestrutura.Memoria.ModuloConta;
using ControleDeBar.Infraestrutura.Memoria.ModuloGarcom;
using ControleDeBar.Infraestrutura.Memoria.ModuloMesa;
using ControleDeBar.Infraestrutura.Memoria.ModuloProduto;

namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public class TelaPrincipal
    {
        private string opcaoEscolhida;
        public bool fecharSistema = false;

        public RepositorioMesa repositorioMesa;
        public TelaMesa telaMesa;

        public RepositorioGarcom repositorioGarcom;
        public TelaGarcom telaGarcom;

        public RepositorioProduto repositorioProduto;
        public TelaProduto telaProduto;

        public RepositorioConta repositorioConta;
        public TelaConta telaConta;

        public TelaPrincipal()
        {
            repositorioMesa = new RepositorioMesa();
            telaMesa = new TelaMesa(repositorioMesa);

            repositorioGarcom = new RepositorioGarcom();
            telaGarcom = new TelaGarcom(repositorioGarcom);

            repositorioProduto = new RepositorioProduto();
            repositorioProduto.repositorioConta = repositorioConta;
            telaProduto = new TelaProduto(repositorioProduto);

            repositorioConta = new RepositorioConta();
            repositorioConta.repositorioProduto = repositorioProduto;
            telaConta = new TelaConta(repositorioConta);
            telaConta.repositorioMesa = repositorioMesa;
            telaConta.repositorioGarcom = repositorioGarcom;
            telaConta.repositorioProduto = repositorioProduto;
            telaConta.telaProduto = telaProduto;
        }

        public void EscolherOpcaoMenuPrincipal()
        {
            Console.WriteLine("---------------");
            Console.WriteLine("Controle de Bar");
            Console.WriteLine("---------------");

            Console.WriteLine();
            Console.WriteLine("1- Controle de Mesas");
            Console.WriteLine("2- Controle de Garçons");
            Console.WriteLine("3- Controle de Produtos");
            Console.WriteLine("4- Controle de Contas");
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
            if (opcaoEscolhida == "3")
                return telaProduto;
            if (opcaoEscolhida == "4")
                return telaConta;
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
