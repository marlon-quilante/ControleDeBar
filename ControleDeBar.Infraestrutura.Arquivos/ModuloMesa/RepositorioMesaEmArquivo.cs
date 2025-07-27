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

        public override void Cadastrar(Mesa registro)
        {
            base.Cadastrar(registro);
        }

        public override void Editar(Mesa registroAtual, Mesa registroAtualizado)
        {
            throw new NotImplementedException();
        }

        public override bool RegistroDuplicado(Mesa registro)
        {
            throw new NotImplementedException();
        }
    }
}
