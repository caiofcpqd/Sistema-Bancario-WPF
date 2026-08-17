using Sistema_Bancário.Clientes;

namespace Sistema_Bancário.Gerenciamento
{
    public class Usuario
    {
        public static List<Clientes.Pessoa> DadosPessoais = new List<Clientes.Pessoa>();
        public static Dictionary<string, Clientes.Pessoa> dictCPF = new Dictionary<string, Clientes.Pessoa>();
        public static HashSet<string> hashEmail = new HashSet<string>();
        public static string CPFLogado { get; set; }
        public static Pessoa pessoaLogada { get; set; }
    }
}