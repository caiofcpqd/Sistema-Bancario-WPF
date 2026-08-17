using Sistema_Bancário.Clientes.Conta;
using Sistema_Bancário.Gerenciamento;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Sistema_Bancário.Principal.Caixas
{
    public partial class box_Extrato : Window
    {
        public class ExtratoVisual
        {
            public string cpf_origem { get; set; }
            public string descricao { get; set; }
            public DateTime data { get; set; }
            public decimal valor { get; set; }
            public string tipo { get; set; }
            public string origem { get; set; }
            public string descricao2 { get; set; }
        }

        public box_Extrato()
        {
            InitializeComponent();
            TodasAsTransacao();
        }

        public void TodasAsTransacao()
        {
            spExtratos.Children.Clear();
            var Extrato = Usuario.dictCPF[Usuario.CPFLogado].publicExtrato;

            for (int i = Extrato.Count - 1; i >= 0; i--)
            {
                AdicionarExtratoVisual(
                    Extrato[i].publicCPFOrigem,
                    Extrato[i].publicDescricao,
                    Extrato[i].publicData,
                    Extrato[i].publicValor,
                    Extrato[i].publicTipo,
                    Extrato[i].publicOrigem,
                    Extrato[i].publicDescricao2);
            }
        }

        public void AdicionarExtratoVisual(string cpf_origem, string descricaoTexto, DateTime dataValor, decimal valorNumerico, string tipoTexto, string origemTexto, string descricao2Texto)
        {
            bool saida = descricaoTexto.Equals("Pagamento", StringComparison.OrdinalIgnoreCase) || descricaoTexto.Equals("Transferência", StringComparison.OrdinalIgnoreCase);
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

            var extrato = new Grid
            {
                MinHeight = 95,
                Margin = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            extrato.ColumnDefinitions.Add(
                new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                }
            );

            extrato.ColumnDefinitions.Add(
                new ColumnDefinition
                {
                    Width = GridLength.Auto
                }
            );

            extrato.ColumnDefinitions.Add(
                new ColumnDefinition
                {
                    Width = GridLength.Auto
                }
            );

            var informacoes = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(20, 10, 10, 10)
            };

            Grid.SetColumn(informacoes, 0);

            var descricao = new TextBlock
            {
                Text = descricaoTexto,
                FontSize = 14,
                FontWeight = FontWeights.SemiBold,
                Foreground = new SolidColorBrush(Color.FromRgb(51, 51, 51))
            };

            var descricao2 = new TextBlock
            {
                Text = descricao2Texto,
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.FromRgb(119, 119, 119)),
                Margin = new Thickness(0, 4, 0, 0)
            };

            var data = new TextBlock
            {
                Text = dataValor.ToString("dd/MM/yyyy HH:mm"),
                FontSize = 11,
                Foreground = new SolidColorBrush(Color.FromRgb(153, 153, 153)),
                Margin = new Thickness(0, 5, 0, 0)
            };

            informacoes.Children.Add(descricao);

            if (!string.IsNullOrWhiteSpace(descricao2Texto))
            {
                informacoes.Children.Add(descricao2);
            }

            informacoes.Children.Add(data);

            var painelBotao = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(10, 0, 15, 0)
            };

            Grid.SetColumn(painelBotao, 1);

            if (descricaoTexto.Equals("Recebimento", StringComparison.OrdinalIgnoreCase))
            {
                var btnDevolver = new Button
                {
                    Content = "Devolver",
                    Width = 85,
                    Height = 36,
                    Background = Brushes.White,
                    Foreground = new SolidColorBrush(Color.FromRgb(75, 22, 140)),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(75, 22, 140)),
                    BorderThickness = new Thickness(1),
                    FontSize = 12,
                    FontWeight = FontWeights.SemiBold,
                    Cursor = System.Windows.Input.Cursors.Hand,
                    Tag = new ExtratoVisual
                    {
                        cpf_origem = cpf_origem,
                        descricao = descricaoTexto,
                        data = dataValor,
                        valor = valorNumerico,
                        tipo = tipoTexto,
                        origem = origemTexto,
                        descricao2 = descricao2Texto
                    }
                };

                btnDevolver.Click += DevolverButton_Click;
                painelBotao.Children.Add(btnDevolver);
            }

            var valores = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 10, 15, 10)
            };

            Grid.SetColumn(valores, 2);

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
                Foreground = new SolidColorBrush(Color.FromRgb(119, 119, 119)),
                Margin = new Thickness(0, 4, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            var origem = new TextBlock
            {
                Text = string.IsNullOrWhiteSpace(origemTexto) ? "" : $"Origem: {origemTexto}",
                FontSize = 11,
                Foreground = new SolidColorBrush(Color.FromRgb(153, 153, 153)),
                Margin = new Thickness(0, 3, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            valores.Children.Add(valor);
            valores.Children.Add(tipo);

            if (!string.IsNullOrWhiteSpace(origemTexto))
            {
                valores.Children.Add(origem);
            }

            extrato.Children.Add(informacoes);
            extrato.Children.Add(painelBotao);
            extrato.Children.Add(valores);

            var separador = new Border
            {
                Height = 1,
                Background = new SolidColorBrush(Color.FromRgb(238, 238, 238))
            };

            spExtratos.Children.Add(extrato);
            spExtratos.Children.Add(separador);
        }

        private void DevolverButton_Click(object sender, RoutedEventArgs e)
        {
            var contaCorrente = false;

            var btn = (Button)sender;
            var extrato = (ExtratoVisual)btn.Tag;

            var cpf_destino = extrato.cpf_origem;
            var nome = extrato.origem;
            var valor = extrato.valor;
            var tipo = extrato.tipo;

            var cliente = Usuario.dictCPF[Usuario.CPFLogado];
            var destinatario = Usuario.dictCPF[cpf_destino];
            var meuSaldo = cliente.publicCorrente.publicSaldo;

            if (tipo == "Corrente")
                contaCorrente = true;
            else
                contaCorrente = false;

            if (meuSaldo < valor)
            {
                MessageBox.Show(
                    "Saldo na conta corrente insuficiente",
                    "Faça um novo depósito");

                return;
            }
            else
            {
                if (Banco.onTransferencia(cliente, destinatario, contaCorrente, valor, null, "Devolução"))
                {
                    MessageBox.Show(
                        "Devolução efetuada com sucesso!",
                        "Sucesso");
                }
                else
                {
                    MessageBox.Show(
                        "Ouve um problema com o pagamento, entre em contato com o seu banco",
                        "Sucesso");
                }

                TodasAsTransacao();
            }
        }

        private void FecharButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}
