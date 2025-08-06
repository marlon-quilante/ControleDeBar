using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeBar.Infraestrutura.Arquivos.ModuloGarcom
{
    public class RepositorioGarcomEmArquivo : RepositorioBaseEmArquivo<Garcom>
    {
        public RepositorioGarcomEmArquivo(ContextoDados contextoDados) : base(contextoDados)
        {
        }

        public override List<Garcom> BuscarRegistros()
        {
            return contextoDados.Garcons;
        }

        public override int UltimoID()
        {
            int ultimoID = 0;
            List<Garcom> garcons = BuscarRegistros();

            foreach (Garcom g in garcons)
                ultimoID = g.Id;

            return ultimoID;
        }

        public override void Cadastrar(Garcom garcom)
        {
            base.Cadastrar(garcom);
            contextoDados.Salvar();
        }

        public override void Editar(Garcom garcomAtual, Garcom garcomAtualizado)
        {
            if (garcomAtual != null && garcomAtualizado != null)
            {
                garcomAtual.Nome = garcomAtualizado.Nome;
                garcomAtual.CPF = garcomAtualizado.CPF;
            }
            contextoDados.Salvar();
        }

        public override bool RegistroDuplicado(Garcom garcom)
        {
            throw new NotImplementedException();
        }
    }
}
