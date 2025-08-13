using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloGarcom;

namespace ControleDeBar.Infraestrutura.Memoria.ModuloGarcom
{
    public class RepositorioGarcom : RepositorioBase<Garcom>
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

        public override void Editar(Garcom garcomAtual, Garcom garcomAtualizado)
        {
            if (garcomAtual != null && garcomAtualizado != null)
            {
                garcomAtual.Nome = garcomAtualizado.Nome;
                garcomAtual.CPF = garcomAtualizado.CPF;
            }
        }
        
        public Garcom BuscarGarcomPorNome(string nomeGarcom)
        {
            foreach (Garcom garcom in listaRegistros)
            {
                if (garcom.Nome == nomeGarcom)
                    return garcom;
            }
            return null;
        }
    }
}
