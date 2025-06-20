namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public interface ITela
    {
        public string EscolherOpcaoMenu();
        public void Cadastrar();
        public void Visualizar();
        public void Editar();
        public void Excluir();
    }
}
