using System;
using System.Windows.Forms;

namespace CalculadoraSemiCientifica
{
    public partial class Calculadora : Form
    {
        // Variáveis de estado da calculadora
        double valor1 = 0;
        double valor2 = 0;
        string operacao = "";
        bool operacaoPressionada = false;
        string expressaoAtual = "";

        public Calculadora()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Evento não utilizado no momento.
        }

        // ===================== MÉTODOS AUXILIARES =====================
        private void AddNumero(object sender)
        {
            Button botao = (Button)sender;

            // Se uma operação foi recém-selecionada, a flag é reiniciada
            // para permitir a digitação do próximo número.
            if (operacaoPressionada)
            {
                operacaoPressionada = false;
            }

            // Concatena o novo dígito à expressão e atualiza o visor.
            expressaoAtual += botao.Text;
            txtVisor.Text = expressaoAtual;
        }

        private void AdicionarOperacao(string op)
        {
            // Permite que o usuário corrija a operação (ex: troca '+' por '-')
            // sem acumular operadores na expressão.
            if (operacaoPressionada)
            {
                // Garante que há algo para remover antes de tentar.
                if (expressaoAtual.Length > 0)
                {
                    // Remove o operador anterior da string.
                    expressaoAtual = expressaoAtual.Substring(0, expressaoAtual.Length - 1);
                }
            }

            // Tenta converter a expressão atual para o primeiro valor da operação.
            // Isso só funciona se a expressão for um número válido.
            if (!string.IsNullOrEmpty(expressaoAtual) && double.TryParse(expressaoAtual, out double temp))
            {
                valor1 = temp;
            }

            // Define a operação atual e sinaliza que um operador foi pressionado.
            operacao = op;
            operacaoPressionada = true;

            // Anexa o novo operador à expressão e atualiza o visor.
            expressaoAtual += op;
            txtVisor.Text = expressaoAtual;
        }

        // ===================== NÚMEROS =====================
        private void btnNumber0_Click(object sender, EventArgs e) => AddNumero(sender);
        private void btnNumber1_Click(object sender, EventArgs e) => AddNumero(sender);
        private void btnNumber2_Click(object sender, EventArgs e) => AddNumero(sender);
        private void btnNumber3_Click(object sender, EventArgs e) => AddNumero(sender);
        private void btnNumber4_Click(object sender, EventArgs e) => AddNumero(sender);
        private void btnNumber5_Click(object sender, EventArgs e) => AddNumero(sender);
        private void btnNumber6_Click(object sender, EventArgs e) => AddNumero(sender);
        private void btnNumber7_Click(object sender, EventArgs e) => AddNumero(sender);
        private void btnNumber8_Click(object sender, EventArgs e) => AddNumero(sender);
        private void btnNumber9_Click(object sender, EventArgs e) => AddNumero(sender);

        // ===================== OPERADORES =====================
        private void btnAdicao_Click(object sender, EventArgs e) => AdicionarOperacao("+");
        private void btnSubtracao_Click(object sender, EventArgs e) => AdicionarOperacao("-");
        private void btnMultiplicacao_Click(object sender, EventArgs e) => AdicionarOperacao("×");
        private void button2_Click(object sender, EventArgs e) => AdicionarOperacao("÷"); // divisão
        private void btnPotencia_Click(object sender, EventArgs e) => AdicionarOperacao("^");
        private void btnRaiz_Click(object sender, EventArgs e) => AdicionarOperacao("√");

        // ===================== RESULTADO =====================
        private void btnResultado_Click(object sender, EventArgs e)
        {
            try
            {
                // Impede o cálculo se nenhuma operação foi selecionada.
                if (string.IsNullOrEmpty(operacao)) return;

                // Usa LastIndexOf para encontrar o operador, tratando corretamente
                // casos com números negativos, como "-5-6".
                int indiceOperador = expressaoAtual.LastIndexOf(operacao);

                // Previne erro caso a expressão seja apenas um operador (ex: "-").
                if (indiceOperador == 0 && expressaoAtual.Length == 1) return;

                string strValor2 = expressaoAtual.Substring(indiceOperador + 1);

                // Garante que existe um segundo número para a operação.
                if (string.IsNullOrEmpty(strValor2)) return;

                valor2 = double.Parse(strValor2);

                double resultado = 0;

                switch (operacao)
                {
                    case "+":
                        resultado = valor1 + valor2;
                        break;
                    case "-":
                        resultado = valor1 - valor2;
                        break;
                    case "×":
                        resultado = valor1 * valor2;
                        break;
                    case "÷":
                        if (valor2 == 0)
                        {
                            MessageBox.Show("Divisão por zero não é permitida!");
                            return;
                        }
                        resultado = valor1 / valor2;
                        break;
                    case "^":
                        resultado = Math.Pow(valor1, valor2);
                        break;
                    case "√":
                        if (valor2 < 0)
                        {
                            MessageBox.Show("Não é possível calcular raiz quadrada de número negativo!");
                            return;
                        }
                        resultado = Math.Sqrt(valor2);
                        break;
                    default:
                        resultado = valor2;
                        break;
                }

                // Exibe o resultado e prepara o estado para a próxima conta.
                expressaoAtual = resultado.ToString();
                txtVisor.Text = expressaoAtual;
                valor1 = resultado;
                operacao = "";
                operacaoPressionada = false;
            }
            catch
            {
                MessageBox.Show("Expressão inválida!");
            }
        }

        // ===================== OUTROS BOTÕES =====================
        private void btnPonto_Click(object sender, EventArgs e)
        {
            // Impede a inserção de múltiplas vírgulas no mesmo número.
            string[] partes = expressaoAtual.Split(new string[] { "+", "-", "×", "÷", "^" }, StringSplitOptions.None);
            string ultimo = partes[partes.Length - 1];

            if (!ultimo.Contains(","))
            {
                expressaoAtual += ",";
                txtVisor.Text = expressaoAtual;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            // Reseta todas as variáveis de estado para o valor inicial.
            txtVisor.Text = "0";
            expressaoAtual = "";
            valor1 = 0;
            valor2 = 0;
            operacao = "";
            operacaoPressionada = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lógica do botão de apagar (backspace).
            if (expressaoAtual.Length > 0)
            {
                string ultimoCaractere = expressaoAtual.Substring(expressaoAtual.Length - 1);

                // Remove o último caractere da expressão.
                expressaoAtual = expressaoAtual.Substring(0, expressaoAtual.Length - 1);

                // Se o caractere apagado era o operador ativo, reverte o estado da operação.
                if (ultimoCaractere == operacao)
                {
                    operacao = "";
                    operacaoPressionada = false;
                }

                // Se a expressão ficou vazia após apagar, exibe "0".
                if (expressaoAtual.Length == 0)
                {
                    txtVisor.Text = "0";
                }
                else
                {
                    txtVisor.Text = expressaoAtual;
                }
            }
        }

        private void Calculadora_Load(object sender, EventArgs e)
        {
            // Evento não utilizado no momento.
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FormSobre telaSobre = new FormSobre();
            telaSobre.Show();
        }
    }
}