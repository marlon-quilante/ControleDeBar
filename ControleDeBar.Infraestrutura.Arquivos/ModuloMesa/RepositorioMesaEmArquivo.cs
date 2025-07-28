using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeBar.Infraestrutura.Arquivos.ModuloMesa
{
    public class RepositorioMesaEmArquivo : RepositorioBaseEmArquivo<Mesa>
    {
        public RepositorioMesaEmArquivo(ContextoDados contextoDados) : base(contextoDados)
        {
        }

        public override List<Mesa> BuscarRegistros()
        {
            return contextoDados.Mesas;
        }

        public override int UltimoID()
        {
            int ultimoID = 0;
            List<Mesa> mesas = BuscarRegistros();

            foreach (Mesa m in mesas)
                ultimoID = m.Id;

            return ultimoID;
        }

        public override void Cadastrar(Mesa mesa)
        {
            base.Cadastrar(mesa);
        }

        public override void Editar(Mesa mesaAtual, Mesa mesaAtualizada)
        {
            if (mesaAtual != null && mesaAtualizada != null)
            {
                mesaAtual.Numero = mesaAtualizada.Numero;
                mesaAtual.QtdLugares = mesaAtualizada.QtdLugares;
            }
            contextoDados.Salvar();
        }

        public override bool RegistroDuplicado(Mesa registro)
        {
            throw new NotImplementedException();
        }
    }
}
