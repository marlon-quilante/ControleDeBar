namespace ControleDeBar.Dominio.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public int Id { get; set; }

        public abstract bool TemRestricaoDeExclusao();

        public abstract string ValidacaoDeDados();
    }
}
