using ControleDeBar.Dominio.Compartilhado;

namespace ControleDeBar.Infraestrutura.Arquivos.Compartilhado
{
    public abstract class RepositorioBaseEmArquivo<T> where T : EntidadeBase<T>
    {
        public ContextoDados contextoDados;
        public List<T> listaRegistros = new List<T>();
        public int ContadorId { get; private set; } = 1;

        public RepositorioBaseEmArquivo(ContextoDados contextoDados)
        {
            this.contextoDados = contextoDados;

            listaRegistros = BuscarRegistros();
        }

        public abstract List<T> BuscarRegistros();

        public virtual void Cadastrar(T registro)
        {
            if (registro != null)
            {
                registro.Id = UltimoID() + 1;
                listaRegistros.Add(registro);
            }
            contextoDados.Salvar();
        }

        public abstract void Editar(T registroAtual, T registroAtualizado);

        public void Excluir(T registro)
        {
            if (registro != null)
            {
                listaRegistros.Remove(registro);
            }
            contextoDados.Salvar();
        }

        public bool IDExiste(int id)
        {
            foreach (T registro in listaRegistros)
            {
                if (registro.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public T BuscarRegistroPorID(int id)
        {
            foreach (T registro in listaRegistros)
            {
                if (registro.Id == id) 
                    return registro;
            }
            return null;
        }

        public abstract bool RegistroDuplicado(T registro);

        public abstract int UltimoID();
    }
}
