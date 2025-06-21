using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloGarcom
{
    public class RepositorioGarcom<T> : RepositorioBase<Garcom>
    {
        public override bool RegistroDuplicado(Garcom garcom)
        {
            if (garcom != null)
            {
                foreach (Garcom g in listaRegistros)
                {
                    if ((g.Nome == garcom.Nome || g.CPF == garcom.CPF) && g.Id != garcom.Id)
                        return true;
                }
            }
            return false;
        }

        public override void Editar(Garcom registroAtual, Garcom registroAtualizado)
        {
            if (registroAtual != null && registroAtualizado != null)
            {
                registroAtual.Nome = registroAtualizado.Nome;
                registroAtual.CPF = registroAtualizado.CPF;
            }
        }
    }
}
