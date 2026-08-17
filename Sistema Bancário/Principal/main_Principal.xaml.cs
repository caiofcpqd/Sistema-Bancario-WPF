using Sistema_Bancário.Gerenciamento;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sistema_Bancário.Principal
{
    public partial class main_Principal : Window
    {
        public main_Principal()
        {
            InitializeComponent();
            TodasAsTransacao();

            rbContaCorrente.IsChecked = false;
            rbPoupanca.IsChecked = true;
        }

        public void TodasAsTransacao()
        {
            spMovimentacoes.Children.Clear();
            var Extrato = Usuario.dictCPF[Usuario.CPFLogado].publicExtrato;

            for (int i = Extrato.Count - 1; i >= 0; i--)
            {
                AddTransacaoVisual(
                    Extrato[i].publicDescricao, 
                    Convert.ToString(Extrato[i].publicData),
                    Extrato[i].publicValor,
                    Extrato[i].publicTipo);
            }
        }

        public void AtualizarInfo(bool corrente)
        {
            var cliente = Usuario.dictCPF[Usuario.CPFLogado];

            var nomePessoa = cliente.publicNome;
            var saldoCorrente = cliente.publicCorrente.publicSaldo;
            var saldoPoupanca = cliente.publicPoupanca.publicSaldo;
            var limiteCorrente = cliente.publicCorrente.publicLimite;
            var limitePoupanca = cliente.publicPoupanca.publicRendimento;

            lbNomeDoCliente.Text = $"Olá, {nomePessoa}!";

            lbSaldoConta.Text = corrente ? saldoCorrente.ToString("C2") : saldoPoupanca.ToString("C2");
            lbValorSecundario.Text = corrente ? limiteCorrente.ToString("C2") : limitePoupanca.ToString("C2");
            lbTipoSecundario.Text = corrente ? "LIMITE" : "RENDIMENTO";

            TodasAsTransacao();
        }

        public void AddTransacaoVisual(string descricaoTexto, string dataTexto, decimal valorNumerico, string tipoTexto)
        {
            bool saida = descricaoTexto.Equals("Pagamento", StringComparison.OrdinalIgnoreCase) || descricaoTexto.Equals("Transferência", StringComparison.OrdinalIgnoreCase) || descricaoTexto.Equals("Devolução", StringComparison.OrdinalIgnoreCase);
            bool entrada = descricaoTexto.Equals("Depósito", StringComparison.OrdinalIgnoreCase) || descricaoTexto.Equals("Recebimento", StringComparison.OrdinalIgnoreCase);

            string valorTexto;
            Color corValor;

            if (saida)
            {
                valorTexto = $"- R$ {Math.Abs(valorNumerico):N2}";
                corValor = Color.FromRgb(211, 47, 47);
            }
            else if (entrada)
            {
                valorTexto = $"+ R$ {Math.Abs(valorNumerico):N2}";
                corValor = Color.FromRgb(35, 134, 54);
            }
            else
            {
                bool valorPositivo = valorNumerico >= 0;

                valorTexto = valorPositivo
                    ? $"+ R$ {Math.Abs(valorNumerico):N2}"
                    : $"- R$ {Math.Abs(valorNumerico):N2}";

                corValor = valorPositivo
                    ? Color.FromRgb(35, 134, 54)
                    : Color.FromRgb(211, 47, 47);
            }

            var movimentacao = new Grid
            {
                Height = 72
            };

            movimentacao.ColumnDefinitions.Add(
                new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                }
            );

            movimentacao.ColumnDefinitions.Add(
                new ColumnDefinition
                {
                    Width = new GridLength(170)
                }
            );

            var informacoes = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20, 0, 0, 0)
            };

            var descricao = new TextBlock
            {
                Text = descricaoTexto,
                FontSize = 14,
                FontWeight = FontWeights.SemiBold,
                Foreground = new SolidColorBrush(Color.FromRgb(51, 51, 51))
            };

            var data = new TextBlock
            {
                Text = dataTexto,
                FontSize = 12,
                Foreground = new SolidColorBrush(
                    Color.FromRgb(153, 153, 153)
                ),
                Margin = new Thickness(0, 5, 0, 0)
            };

            informacoes.Children.Add(descricao);
            informacoes.Children.Add(data);

            var valores = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 0, 20, 0)
            }; 
            
            Grid.SetColumn(valores, 1);

            var valor = new TextBlock
            {
                Text = valorTexto,
                FontSize = 14,
                FontWeight = FontWeights.SemiBold,
                Foreground = new SolidColorBrush(corValor),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            var tipo = new TextBlock
            {
                Text = tipoTexto,
                FontSize = 11,
                Foreground = new SolidColorBrush(Color.FromRgb(153, 153, 153)),
                Margin = new Thickness(0, 3, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            valores.Children.Add(valor);
            valores.Children.Add(tipo);
            movimentacao.Children.Add(informacoes);
            movimentacao.Children.Add(valores);

            var separador = new Border
            {
                Height = 1,
                Background = new SolidColorBrush(Color.FromRgb(238, 238, 238))
            };

            spMovimentacoes.Children.Add(movimentacao);
            spMovimentacoes.Children.Add(separador);
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            var login = new main_Win();
            login.Show();

            this.Close();
        }

        private void DepositarButton_Click(object sender, RoutedEventArgs e)
        {
            var box = new box_Depositar();
            box.Show();
        }

        private void TransferirButton_Click(object sender, RoutedEventArgs e)
        {
            var box = new Caixas.box_Transferir();
            box.Show();
        }

        private void PagarButton_Click(object sender, RoutedEventArgs e)
        {
            var box = new box_Pagar();
            box.Show();
        }

        private void ExtratoButton_Click(object sender, RoutedEventArgs e)
        {
            var box = new Caixas.box_Extrato();
            box.Show();
        }

        private void ContaCorrente_Checked(object sender, RoutedEventArgs e)
        {
            AtualizarInfo(true);
        }

        private void Poupanca_Checked(object sender, RoutedEventArgs e)
        {
            AtualizarInfo(false);
        }

        private void ScrollViewer_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (rbContaCorrente.IsChecked == true)
            {
                lbTipoConta.Text = "Conta Corrente";
                AtualizarInfo(true);
            }

            if (rbPoupanca.IsChecked == true)
            {
                lbTipoConta.Text = "Conta Poupança";
                AtualizarInfo(false);
            }
        }
    }
}
