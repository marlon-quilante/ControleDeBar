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
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.ReadLine();
        }

        public void Cadastrar()
        {
            Console.Clear();
            Console.WriteLine("----------------------");
            Console.WriteLine($"{entidade} - Cadastro");
            Console.WriteLine("----------------------\n");
            T registro = ObterDados();

            string erros = registro.ValidacaoDeDados();

            if (erros != string.Empty)
            {
                Console.WriteLine();
                ApresentarMensagem(erros, ConsoleColor.Red);
                Console.Clear();
                Cadastrar();
                return;
            }

            if (repositorioBase.RegistroDuplicado(registro))
            {
                Console.WriteLine();
                ApresentarMensagem("Já existe um cadastro com esses dados!", ConsoleColor.Red);
                Cadastrar();
                return;
            }

            repositorioBase.Cadastrar(registro);
            Console.WriteLine();
            ApresentarMensagem("Cadastro realizado com sucesso!", ConsoleColor.Green);
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
            Console.WriteLine("--------------------");
            Console.WriteLine($"{entidade} - Edição");
            Console.WriteLine("--------------------\n");

            Console.WriteLine("Digite o ID do registro que deseja editar...\n");
            Console.WriteLine();
            int idRegistro = ObterID();
            T registroAtual = repositorioBase.BuscarRegistroPorID(idRegistro);

            Console.WriteLine();
            T registroAtualizado = ObterDados();
            registroAtualizado.Id = registroAtual.Id;

            if (repositorioBase.RegistroDuplicado(registroAtualizado))
            {
                Console.WriteLine();
                ApresentarMensagem("Já existe um cadastro com esses dados!", ConsoleColor.Red);
                Editar();
                return;
            }
            else
            {
                repositorioBase.Editar(registroAtual, registroAtualizado);
                Console.WriteLine();
                ApresentarMensagem("Edição realizada com sucesso!", ConsoleColor.Green);
            }
        }

        public void Excluir()
        {
            Console.Clear();
            Console.WriteLine("----------------------");
            Console.WriteLine($"{entidade} - Exclusão");
            Console.WriteLine("----------------------\n");

            Console.WriteLine("Digite o ID do registro que deseja excluir...\n");
            int idRegistro = ObterID();
            T entidadeBase = repositorioBase.BuscarRegistroPorID(idRegistro);

            if (entidadeBase.TemRestricaoDeExclusao())
            {
                Console.WriteLine();
                ApresentarMensagem("Não é possível excluir pois esse registro possui alguma restrição! Pressione ENTER para voltar...", ConsoleColor.Red);
                return;
            }

            repositorioBase.Excluir(entidadeBase);
            Console.WriteLine();
            ApresentarMensagem("Exclusão realizada com sucesso!", ConsoleColor.Green);
        }

        public abstract T ObterDados();

        public int ObterID()
        {
            int id = 0;
            bool idValido = false;
            bool idExiste = false;

            while (!idValido || !idExiste)
            {
                Console.Write("ID: ");
                idValido = int.TryParse(Console.ReadLine(), out id);

                if (!idValido)
                {
                    Console.WriteLine();
                    ApresentarMensagem("ID inválido! Pressione ENTER para tentar novamente...", ConsoleColor.Yellow);
                    continue;
                }

                idExiste = repositorioBase.IDExiste(id);

                if (!idExiste)
                {
                    Console.WriteLine();
                    ApresentarMensagem("Este ID não está cadastrado! Pressione ENTER para tentar novamente...", ConsoleColor.Yellow);
                    continue;
                }
            }
            return id;
        }
    }
}
