namespace Sistema_Bancário.Clientes.Conta
{
    public class Extrato
    {
		private string descricao;
		private DateTime data;
		private decimal valor;
		private string tipo;
		private string origem;
		private string descricao2;
		private string cpf_origem;

		public string publicCPFOrigem
		{
			get { return cpf_origem; }
			set { cpf_origem = value; }
		}

		public string publicDescricao2
		{
			get { return descricao2; }
			set { descricao2 = value; }
		}

		public string publicOrigem
		{
			get { return origem; }
			set { origem = value; }
		}

		public string publicTipo
		{
			get { return tipo; }
			set { tipo = value; }
		}

		public decimal publicValor
		{
			get { return valor; }
			set { valor = value; }
		}

		public DateTime publicData
		{
			get { return data; }
			set { data = value; }
		}

		public string publicDescricao
		{
			get { return descricao; }
			set { descricao = value; }
		}
	}
}
