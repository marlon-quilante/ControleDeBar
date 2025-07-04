﻿using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloConta;

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

                if (telaEscolhida is TelaConta)
                {
                    string opcaoEscolhidaConta = telaPrincipal.telaConta.EscolherOpcaoMenuConta();

                    switch (int.Parse(opcaoEscolhidaConta))
                    {
                        case 1: telaEscolhida.Cadastrar(); break;
                        case 2: telaEscolhida.Visualizar(); break;
                        case 3: telaPrincipal.telaConta.VisualizarFechamentoDiario(); break;
                        case 4: telaPrincipal.telaConta.VisualizarPedido(); break;
                        case 5: telaPrincipal.telaConta.FecharConta(); break;
                        case 6: telaPrincipal.telaConta.AdicionarProdutoNoPedido(); break;
                        case 7: telaPrincipal.telaConta.RemoverProdutoDoPedido(); break;
                        default: break;
                    }
                }
                else
                {
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
}