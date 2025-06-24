using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloGarcom
{
    public class Garcom : EntidadeBase<Garcom>
    {
        public string Nome { get; set; }
        public string CPF { get;  set; }
        public bool TemPedido { get; set; }

        public Garcom(string nome, string cpf)
        {
            Nome = nome;
            CPF = cpf;
        }

        public override bool TemRestricaoDeExclusao()
        {
            if (TemPedido)
                return true;
            return false;
        }

        public override string ValidacaoDeDados()
        {
            string erros = string.Empty;

            if (Nome.Length < 3 || Nome.Length > 100)
                erros += "O nome precisa conter entre 3 e 100 caracteres!\n";
            if (!CPFValido(CPF))
                erros += "CPF inválido!";

            return erros;
        }

        private static bool CPFValido(string cpf)
        {

            //087.231.139-28
            if (cpf.Length == 14)
            {
                if (cpf[3] == '.' && cpf[7] == '.' && cpf[11] == '-')
                {
                    string cpfConvertido = cpf.Replace(".", "").Replace("-", "");
                    bool cpfValido = long.TryParse(cpfConvertido, out long cpfNumero);

                    if (cpfValido)
                        return true;
                }
            }           
            return false;
        }
    }
}
