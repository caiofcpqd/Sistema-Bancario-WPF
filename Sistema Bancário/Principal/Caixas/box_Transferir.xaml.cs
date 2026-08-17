using Sistema_Bancário.Gerenciamento;
using System.Windows;

namespace Sistema_Bancário.Principal.Caixas
{
    public partial class box_Transferir : Window
    {
        public box_Transferir()
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
        }

        public void efeTransferencia()
        {
            var valor = txtValor.Text;
            var cpf_destino = txtDestinatario.Text;
            var cliente = Usuario.dictCPF[Usuario.CPFLogado];

            ckCPFDestino(false);

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
                var meuSaldo = cliente.publicCorrente.publicSaldo;

                if (meuSaldo < try_valor)
                {
                    MessageBox.Show(
                        "Saldo na conta corrente insuficiente",
                        "Faça um novo depósito");

                    return;
                }
                else
                {
                    if (Banco.onTransferencia(cliente, destinatario, true, try_valor, null, "Transferência"))
                    {
                        MessageBox.Show(
                            "Transferência efetuada com sucesso!",
                            "Sucesso");
                    }
                    else
                    {
                        MessageBox.Show(
                            "Ouve um problema na transfêrencia, entre em contato com o seu banco",
                            "Sucesso");
                    }
                }
            }
            catch
            {
                if (!decimal.TryParse(valor, out _))
                {
                    MessageBox.Show(
                        "Insira um valor de transferência válido.",
                        "Erro");
                }
            }
        }

        private void TransferirButton_Click(object sender, RoutedEventArgs e)
        {
            efeTransferencia();
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FecharButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
