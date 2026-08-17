using Sistema_Bancário.Clientes;
using Sistema_Bancário.Gerenciamento;
using Sistema_Bancário.Principal;
using System.Windows;
using System.Windows.Media;

namespace Sistema_Bancário.Cadastro
{
    public partial class main_Reg : Window
    {
        public main_Reg()
        {
            InitializeComponent();
        }

        void intoLogin(string cpf)
        {
            Usuario.CPFLogado = cpf;

            main_Principal main = new main_Principal();
            main.Show();

            this.Close();
        }

        bool ckCampos(string senha, string nome, string nascimento, string cpf, string telefone, string email)
        {
            if (string.IsNullOrEmpty(nome) ||
                string.IsNullOrEmpty(nascimento) ||
                string.IsNullOrEmpty(cpf) ||
                string.IsNullOrEmpty(telefone) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(senha))
            {
                MessageBox.Show(
                    "Preencha todos os campos.",
                    "Erro"); 

                return false;
            }

            if (senha.Length < 5)
            {
                lbSenha.Foreground = Brushes.Red;

                MessageBox.Show(
                    "Insira uma senha mais forte.",
                    "Erro na senha"); 
                
                return false;
            }

            if (!email.Contains("@") || !email.Contains(".com", StringComparison.OrdinalIgnoreCase))
            {
                lbEmail.Foreground = Brushes.Red;

                MessageBox.Show(
                    "Insira um email válido.",
                    "Erro no email"); 
                
                return false;
            }

            if (cpf.Length != 11)
            {
                lbCPF.Foreground = Brushes.Red;

                MessageBox.Show(
                    "Insira um CPF válido.",
                    "Erro no número de CPF"); 
                
                return false;
            }

            if (telefone.Length != 13)
            {
                lbTelefone.Foreground = Brushes.Red;

                MessageBox.Show(
                    "Insira um número de telefone válido. (55XXXXXXXXXXX)",
                    "Erro no número de telefone"); 
                
                return false;
            }

            return true;
        }

        void cadCliente()
        {
            var nome = NomeTextBox.Text.ToUpper();
            var cpf = CpfTextBox.Text;
            var telefone = TelefoneTextBox.Text;
            var email = EmailTextBox.Text;
            var nascimento = DataNascimentoTextBox.Text;
            var senha = SenhaPasswordBox.Password;

            var ckCPF = Usuario.dictCPF.ContainsKey(cpf);
            var ckEmail = Usuario.hashEmail.Contains(email);

            lbEmail.Foreground = SystemColors.ControlTextBrush;
            lbCPF.Foreground = SystemColors.ControlTextBrush;
            lbTelefone.Foreground = SystemColors.ControlTextBrush;

            if (!ckCampos(senha, nome, nascimento, cpf, telefone, email))
                return;

            if (ckEmail)
            {
                MessageBox.Show(
                    "Email já cadastrado, insira um novo.",
                    "Erro"); 
                
                return;
            }

            if (ckCPF)
            {
                MessageBox.Show(
                    "CPF já cadastrado, insira um novo.",
                    "Erro"); 
                
                return;
            }
            try
            {
                var try_date = Convert.ToDateTime(nascimento);
                var try_cpf = Convert.ToUInt64(cpf);
                var try_telefone = Convert.ToUInt64(telefone);

                Usuario.DadosPessoais.Add(new Pessoa(nome, telefone, email, try_date, senha));
                Usuario.dictCPF.Add(cpf, Usuario.DadosPessoais[Usuario.DadosPessoais.Count - 1]);
                Usuario.hashEmail.Add(email);

                MessageBox.Show(
                    $"Bem-vindo, SR {nome},\nclique em OK para seguir direto para sua conta.",
                    "Cadastrado com sucesso!");

                intoLogin(cpf);
            }
            catch (FormatException)
            {
                if (!DateTime.TryParse(nascimento, out _))
                {
                    lbDataNascimento.Foreground = Brushes.Red;

                    MessageBox.Show(
                        "Insira a data de nascimento no formato correto. (XX/XX/XXXX)",
                        "Erro na data de nascimento");
                }

                if (!UInt64.TryParse(telefone, out _))
                {
                    lbTelefone.Foreground = Brushes.Red;

                    MessageBox.Show(
                        "Insira o telefone com apenas números.",
                        "Erro no número de telefone");
                }

                if (!UInt64.TryParse(cpf, out _))
                {
                    lbCPF.Foreground = Brushes.Red;

                    MessageBox.Show(
                        "Insira o CPF com apenas números.",
                        "Erro no número de CPF");
                }
            }
        }

        private void CadastrarButton_Click(object sender, RoutedEventArgs e)
        {
            cadCliente();
        }

        private void VoltarButton_Click(object sender, RoutedEventArgs e)
        {
            main_Win main = new main_Win();
            main.Show();

            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
