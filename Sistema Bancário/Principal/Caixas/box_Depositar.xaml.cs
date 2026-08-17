using Sistema_Bancário.Gerenciamento;
using System.Windows;

namespace Sistema_Bancário.Principal
{
    public partial class box_Depositar : Window
    {
        public box_Depositar()
        {
            InitializeComponent();
        }

        void depDinheiro()
        {
            var contaCorrente = false;
            var valor = txtValorDeposito.Text;
            var cliente = Usuario.dictCPF[Usuario.CPFLogado];

            if (rbContaCorrente.IsChecked == true)
                contaCorrente = true;
            else
                contaCorrente = false;

            if (string.IsNullOrEmpty(valor))
            {
                MessageBox.Show(
                    "Insira um valor de deposito",
                    "Erro"); 

                return;
            }
            try
            {
                var try_valor = Convert.ToDecimal(valor);

                if (Banco.onDeposito(cliente, contaCorrente, try_valor))
                {
                    MessageBox.Show(
                        "Valor inserido depositado com sucesso",
                        "Sucesso!");
                }
            }
            catch
            {
                if (!decimal.TryParse(valor, out _))
                {
                    MessageBox.Show(
                        "Insira um valor de deposito válido",
                        "Erro");
                }
            }
        }

        private void DepositarButton_Click(object sender, RoutedEventArgs e)
        {
            depDinheiro();
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}