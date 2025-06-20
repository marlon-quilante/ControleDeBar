namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public abstract class TelaBase<T> where T : EntidadeBase<T>
    {
        public RepositorioBase<T> repositorioBase;
        private string entidade;

        public TelaBase(string entidade, RepositorioBase<T> repositorioBase)
        {
            this.entidade = entidade;
            this.repositorioBase = repositorioBase;
        }

        public string EscolherOpcaoMenu()
        {
            Console.Clear();
            Console.WriteLine("---------------------------");
            Console.WriteLine($"Controle de {entidade}");
            Console.WriteLine("---------------------------");

            Console.WriteLine();
            Console.WriteLine("1- Cadastrar");
            Console.WriteLine("2- Visualizar");
            Console.WriteLine("3- Editar");
            Console.WriteLine("4- Excluir");
            Console.WriteLine("5- Voltar");
            Console.WriteLine();

            Console.Write("Selecione a opção: ");
            return Console.ReadLine();
        }

        public void ApresentarMensagem(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine();
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("Pressione ENTER para continuar...");
            Console.ReadLine();
        }

        public virtual void Cadastrar()
        {
            Console.Clear();
            Console.WriteLine("---------------------");
            Console.WriteLine($"Cadastrar {entidade}");
            Console.WriteLine("---------------------\n");

            T registro = ObterDados();

            if (repositorioBase.RegistroDuplicado(registro))
            {
                ApresentarMensagem("Já existe um cadastro com esses dados!", ConsoleColor.Red);
                Console.Clear();
                Cadastrar();
                return;
            }
            else
            {
                repositorioBase.Cadastrar(registro);
                ApresentarMensagem("Cadastro realizado com sucesso!", ConsoleColor.Green);
            }
        }

        public virtual void Visualizar()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"{entidade} - Lista de Cadastros");
            Console.WriteLine("-------------------------------\n");
        }

        public void Editar()
        {
            Console.Clear();
            Console.WriteLine("---------------------");
            Console.WriteLine($"Editar {entidade}");
            Console.WriteLine("---------------------\n");

            int idRegistro = BuscarID();
            T registroAtual = repositorioBase.BuscarRegistroPorID(idRegistro);

            Console.WriteLine();
            T registroAtualizado = ObterDados();

            if (repositorioBase.RegistroDuplicado(registroAtualizado))
            {
                Console.WriteLine();
                ApresentarMensagem("Este registro já existe!", ConsoleColor.Red);
                Console.Clear();
                Editar();
                return;
            }
            else
            {
                repositorioBase.Editar(registroAtual, registroAtualizado);
                ApresentarMensagem("Edição realizada com sucesso!", ConsoleColor.Green);
            }
        }

        public void Excluir()
        {
            Console.Clear();
            Console.WriteLine("---------------------");
            Console.WriteLine($"Excluir {entidade}");
            Console.WriteLine("---------------------\n");

            Console.WriteLine("Digite o ID do registro que deseja excluir...\n");
            int idRegistro = BuscarID();
            T entidadeBase = repositorioBase.BuscarRegistroPorID(idRegistro);
            repositorioBase.Excluir(entidadeBase);
            ApresentarMensagem("Exclusão realizada com sucesso!", ConsoleColor.Green);
        }

        public abstract T ObterDados();

        public int BuscarID()
        {
            int id = 0;
            bool idValido = false;
            bool idExiste = false;

            while (!idValido || !idExiste)
            {
                Console.Write("Digite o ID: ");
                idValido = int.TryParse(Console.ReadLine(), out id);

                if (!idValido)
                {
                    Console.WriteLine("\nID inválido! Pressione ENTER para tentar novamente...");
                    Console.ReadLine();
                    continue;
                }

                idExiste = repositorioBase.IDExiste(id);

                if (!idExiste)
                {
                    Console.WriteLine("\nEste ID não está cadastrado! Pressione ENTER para tentar novamente...");
                    Console.ReadLine();
                    continue;
                }
            }
            return id;
        }
    }
}
