using Sistema_Bancário.Cadastro;
using Sistema_Bancário.Clientes;
using Sistema_Bancário.Gerenciamento;
using Sistema_Bancário.Principal;
using System.Windows;

namespace Sistema_Bancário
{
    public partial class main_Win : Window
    {
        public main_Win()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void intoLogin(string cpf)
        {
            Usuario.CPFLogado = cpf;

            main_Principal main = new main_Principal();
            main.Show();

            this.Close();
        }

        void ckLogin()
        {
            var cpf = CpfTextBox.Text;
            var senha = SenhaPasswordBox.Password;

            if (string.IsNullOrEmpty(cpf) || 
                string.IsNullOrEmpty(senha))
            {
                MessageBox.Show(
                    "Preencha todos os campos",
                    "Erro"); 
                
                return;
            }

            if (!Usuario.dictCPF.ContainsKey(cpf))
            {
                MessageBox.Show(
                    "CPF não encontrado",
                    "Erro"); 
                
                return;
            }
            else
            {
                if (Usuario.dictCPF[cpf].publicSenha != senha)
                {
                    MessageBox.Show(
                        "Senha inválida.",
                        "Erro"); 
                    
                    return;
                }
                else
                {
                    intoLogin(cpf);
                }
            }
        }

        private void EntrarButton_Click(object sender, RoutedEventArgs e)
        {
            ckLogin();
        }

        private void CadastrarButton_Click(object sender, RoutedEventArgs e)
        {
            main_Reg registro = new main_Reg();
            registro.Show();

            this.Close();
        }
    }
}
