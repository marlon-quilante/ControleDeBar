using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Infraestrutura.Memoria.ModuloGarcom;

namespace ControleDeBar.ConsoleApp.ModuloGarcom
{
    public class TelaGarcom : TelaBase<Garcom>, ITela
    {
        public RepositorioGarcom repositorioGarcom;

        public TelaGarcom(RepositorioGarcom repositorioGarcom) : base ("Garçom", repositorioGarcom)
        {
            this.repositorioGarcom = repositorioGarcom;
        }

        public override Garcom ObterDados()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();
            Console.Write("Digite o CPF: ");
            string cpf = Console.ReadLine();

            Garcom garcom = new Garcom(nome, cpf);

            return garcom;
        }

        public override void Visualizar()
        {
            base.Visualizar();

            Console.WriteLine("{0,-5} | {1,-20} | {2,-15}",
                "ID", "Nome", "CPF");
            foreach (Garcom garcom in repositorioGarcom.listaRegistros)
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-15}",
                garcom.Id, garcom.Nome, garcom.CPF);
            }
            Console.WriteLine();
            ApresentarMensagem("Pressione ENTER para continuar...", ConsoleColor.Yellow);
        }
    }
}
