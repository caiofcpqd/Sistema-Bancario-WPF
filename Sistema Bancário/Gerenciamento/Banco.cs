using Sistema_Bancário.Clientes;
using System.Windows;

namespace Sistema_Bancário.Gerenciamento
{
    public static class Banco
    {
        public static bool onTransacao(bool tipoConta, Pessoa pessoa, string tipo, decimal try_valor, string nomeOrigem, string descricao2, string cpf_origem)
        {
            pessoa.publicExtrato.Add(new Clientes.Conta.Extrato
            {
                publicData = DateTime.Now,
                publicDescricao = tipo,
                publicTipo = tipoConta == true ? "Corrente" : "Poupança",
                publicValor = try_valor,
                publicOrigem = nomeOrigem,
                publicDescricao2 = descricao2,
                publicCPFOrigem = cpf_origem
            });

            _ = tipoConta == true
                ? pessoa.publicCorrente.publicSaldo += (tipo != "Pagamento" && tipo != "Transferência") ? try_valor : -try_valor
                : pessoa.publicPoupanca.publicSaldo += (tipo != "Pagamento" && tipo != "Transferência") ? try_valor : -try_valor;

            return true;
        }

        public static bool onDeposito(Pessoa cliente, bool corrente, decimal try_valor)
        {
            if (try_valor < 1)
            {
                MessageBox.Show(
                    "Insira um valor mínimo de R$ 1.00",
                    "Erro");

                return false;
            }
            else
            {
                if (Banco.onTransacao(
                    corrente,
                    cliente,
                    "Depósito",
                    try_valor,
                    cliente.publicNome,
                    null,
                    Usuario.CPFLogado))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool onTransferencia(Pessoa cliente, Pessoa destinatario, bool corrente, decimal try_valor, string descricao, string tipo)
        {
            if (try_valor < 1)
            {
                MessageBox.Show(
                    "Insira um valor mínimo de R$ 1.00",
                    "Erro");

                return false;
            }
            else
            {
                if (!Banco.onTransacao(
                    corrente,
                    cliente,
                    "Transferência",
                    try_valor,
                    cliente.publicNome,
                    descricao,
                    Usuario.CPFLogado))
                {
                    return false;
                }

                if (!Banco.onTransacao(
                    corrente,
                    destinatario,
                    "Recebimento",
                    try_valor,
                    cliente.publicNome,
                    descricao,
                    Usuario.CPFLogado))
                {
                    return false;
                }
            }

            return true;
        }
    }
}