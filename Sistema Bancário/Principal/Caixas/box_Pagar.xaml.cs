using Sistema_Bancário.Clientes;
using Sistema_Bancário.Clientes.Conta;
using Sistema_Bancário.Gerenciamento;
using System.Windows;

namespace Sistema_Bancário.Principal
{
    public partial class box_Pagar : Window
    {
        public box_Pagar()
        {
            InitializeComponent();
        }

        public void ckCPFDestino(bool writelabel)
        {
            var cpf_destino = txtDestinatario.Text;

            if (string.IsNullOrEmpty(cpf_destino))
            {
                MessageBox.Show(
                    "Preencha o CPF",
                    "Erro"); 

                return;
            }

            if (!Usuario.dictCPF.ContainsKey(cpf_destino))
            {
                MessageBox.Show(
                    "CPF do destinatário não encontrado",
                    "CPF Inválido"); 

                return;
            }

            if (writelabel)
            {
                lbNomeDestinatario.Text = Usuario.dictCPF[cpf_destino].publicNome;
            }
        }

        public void efePagamento()
        {
            var contaCorrente = false;
            var valor = txtValorPagamento.Text;
            var cpf_destino = txtDestinatario.Text;
            var cliente = Usuario.dictCPF[Usuario.CPFLogado];

            if (rbContaCorrente.IsChecked == true)
                contaCorrente = true;
            else
                contaCorrente = false;

            ckCPFDestino(true);

            if (string.IsNullOrEmpty(valor))
            {
                MessageBox.Show(
                    "Preencha todos os campos",
                    "Erro"); 

                return;
            }
            try
            {
                var try_valor = Convert.ToDecimal(valor);
                var destinatario = Usuario.dictCPF[cpf_destino];
                var meuSaldo = rbContaCorrente.IsChecked == true ? cliente.publicCorrente.publicSaldo : cliente.publicPoupanca.publicSaldo;

                if (meuSaldo < try_valor)
                {
                    MessageBox.Show(
                        "Saldo insuficiente",
                        "Faça um novo depósito"); 

                    return;
                }
                else
                {
                    if (Banco.onTransferencia(cliente, destinatario, contaCorrente, try_valor, null, "Pagamento"))
                    {
                        MessageBox.Show(
                            "Pagamento efetuado com sucesso!",
                            "Sucesso");
                    }
                    else
                    {
                        MessageBox.Show(
                            "Ouve um problema com o pagamento, entre em contato com o seu banco",
                            "Sucesso");
                    }
                }
            }
            catch
            {
                if (!decimal.TryParse(valor, out _))
                {
                    MessageBox.Show(
                        "Insira um valor de pagamento válido.",
                        "Erro");
                }
            }
        }

        private void PagarButton_Click(object sender, RoutedEventArgs e)
        {
            efePagamento();
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void VerificarCpfButton_Click(object sender, RoutedEventArgs e)
        {
            ckCPFDestino(true);
        }
    }
}
