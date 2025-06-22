namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public abstract class RepositorioBase<T> where T : EntidadeBase<T>
    {
        public List<T> listaRegistros = new List<T>();
        public int ContadorId { get; private set; } = 1;

        public void Cadastrar(T registro)
        {
            if (registro != null)
            {
                registro.Id = ContadorId;
                listaRegistros.Add(registro);
                ContadorId++;
            }
        }

        public abstract void Editar(T registroAtual, T registroAtualizado);

        public void Excluir(T registro)
        {
            if (registro != null)
            {
                listaRegistros.Remove(registro);
            }
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
    }
}
