namespace Sistema_Bancário.Clientes
{
    public class Pessoa
    {
		private string nome;
		private string telefone;
		private string email;
		private DateTime nascimento;
		private string senha;
        private Conta.Corrente corrente;
        private Conta.Poupanca poupanca;
		private List<Conta.Extrato> extrato;

		public List<Conta.Extrato> publicExtrato
		{
			get { return extrato; }
			set { extrato = value; }
		}

		public Conta.Poupanca publicPoupanca
        {
            get { return poupanca; }
            set { poupanca = value; }
        }

        public Conta.Corrente publicCorrente
        {
            get { return corrente; }
            set { corrente = value; }
        }

		public string publicSenha
		{
			get { return senha; }
			set { senha = value; }
		}

		public DateTime publicNascimento
		{
			get { return nascimento; }
			set { nascimento = value; }
		}

		public string publicEmail
		{
			get { return email; }
			set { email = value; }
		}

		public string publicTelefone
		{
			get { return telefone; }
			set { telefone = value; }
		}

		public string publicNome
		{
			get { return nome; }
			set { nome = value; }
		}

        public Pessoa()
        {
        }

        public Pessoa(string _nome, string _telefone, string _email, DateTime _nascimento, string _senha)
        {
            publicNome = _nome;
            publicTelefone = _telefone;
            publicEmail = _email;
            publicNascimento = _nascimento;
            publicSenha = _senha;
			publicCorrente = new Conta.Corrente();
            publicPoupanca = new Conta.Poupanca();
            publicExtrato = new List<Conta.Extrato>();
        }
    }
}