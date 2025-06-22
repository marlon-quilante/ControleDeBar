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

        public override void Editar(Mesa mesaAtual, Mesa mesaAtualizada)
        {
            if (mesaAtual != null && mesaAtualizada != null)
            {
                mesaAtual.Numero = mesaAtualizada.Numero;
                mesaAtual.QtdLugares = mesaAtualizada.QtdLugares;
            }
        }
    }
}
