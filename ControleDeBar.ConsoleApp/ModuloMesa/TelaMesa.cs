﻿using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    public class TelaMesa : TelaBase<Mesa>, ITela
    {
        RepositorioMesa repositorioMesa;

        public TelaMesa(RepositorioMesa repositorioMesa) : base("Mesa", repositorioMesa)
        {
            this.repositorioMesa = repositorioMesa;
        }

        public override Mesa ObterDados()
        {
            int numeroMesa = 0;
            bool numeroMesaValido = false;
            int qtdLugares = 0;
            bool qtdLugaresValido = false;

            while (!numeroMesaValido || !qtdLugaresValido)
            {
                Console.Write("Digite o número da mesa: ");
                numeroMesaValido = int.TryParse(Console.ReadLine(), out numeroMesa);
                Console.Write("Digite a quantidade de lugares da mesa: ");
                qtdLugaresValido = int.TryParse(Console.ReadLine(), out qtdLugares);

                if (!numeroMesaValido || !qtdLugaresValido)
                {
                    Console.WriteLine();
                    ApresentarMensagem("Dados inválidos! Pressione ENTER para tentar novamente...", ConsoleColor.Red);
                }
            }
            numeroMesaValido = false;
            qtdLugaresValido = false;
            return new Mesa(numeroMesa, qtdLugares);
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
            Console.WriteLine();
            ApresentarMensagem("Pressione ENTER para continuar...", ConsoleColor.Yellow);
        }
    }
}
