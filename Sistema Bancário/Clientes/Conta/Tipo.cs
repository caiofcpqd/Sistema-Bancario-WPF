namespace Sistema_Bancário.Clientes.Conta
{
    public class Corrente : Tipo
    {
        private decimal Limite;

        public decimal publicLimite
        {
            get { return Limite; }
            set { Limite = value; }
        }
    }

    public class Poupanca : Tipo
    {
        private decimal Rendimento;

        public decimal publicRendimento
        {
            get { return Rendimento; }
            set { Rendimento = value; }
        }
    }

    public class Tipo
    {
        private decimal Saldo;

        public decimal publicSaldo
        {
            get { return Saldo; }
            set { Saldo = value; }
        }
    }
}
