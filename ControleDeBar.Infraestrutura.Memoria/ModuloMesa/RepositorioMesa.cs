using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloMesa;

namespace ControleDeBar.Infraestrutura.Memoria.ModuloMesa
{
    public class RepositorioMesa : RepositorioBase<Mesa>
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

        public Mesa BuscarMesaPorNumero(int numeroMesa)
        {
            foreach (Mesa mesa in listaRegistros)
            {
                if (mesa.Numero == numeroMesa)
                    return mesa;
            }
            return null;
        }
    }
}
