namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public int Id { get; set; }

        public abstract string ValidacaoDeDados(T entidadeBase);
    }
}
