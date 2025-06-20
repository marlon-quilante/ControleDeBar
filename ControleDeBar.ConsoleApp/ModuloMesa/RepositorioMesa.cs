using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    public class RepositorioMesa<T> : RepositorioBase<Mesa>
    {
        public override bool RegistroDuplicado(Mesa mesa)
        {
            if (mesa != null)
            {
                foreach (Mesa m in listaRegistros)
                {
                    if (m.Numero == mesa.Numero && m.Id != mesa.Id)
                        return true;
                }
            }
            return false;
        }

        public override void Editar(Mesa registroAtual, Mesa registroAtualizado)
        {
            if (registroAtual != null && registroAtualizado != null)
            {
                registroAtual.Numero = registroAtualizado.Numero;
                registroAtual.QtdLugares = registroAtualizado.QtdLugares;
            }
        }
    }
}
