using System.Diagnostics.SymbolStore;

namespace WF_Calculator
{
    public partial class Form1 : Form
    {
        bool newNumber = false;
        double number1 = 0;
        double number2 = 0;
        char symbol;
        double currentresult;
        public Form1()
        {
            InitializeComponent();
        }

        private void button_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (textBox_Result.Text == "0" || newNumber == true)
            {
                textBox_Result.Clear();
                newNumber = false;
            }
            if (button.Text == "." && textBox_Result.Text.Contains("."))
            {
                textBox_Result.Text = textBox_Result.Text;

            }
            else
            {
                textBox_Result.Text = textBox_Result.Text + button.Text;
                label_equation.Text += button.Text;
            } 
        }

        private void operator_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if ((button.Text == "+" || button.Text == "-" ||
                button.Text == "/" || button.Text == "*")
                && textBox_Result.Text.Length == 0)
            {
                label_equation.Text = label_equation.Text;
            }
            else
            {
                label_equation.Text += button.Text;
                newNumber = true;
            }
            textBox_Result.Clear();
        }

        private void clear_click(object sender, EventArgs e)
        {
            textBox_Result.Clear();
            label_equation.Text = "";
        }

        private void equals_click(object sender, EventArgs e)
        {
            textBox_Result.Clear();
            textBox_Result.Text = Operations(label_equation.Text, 0);
        }
       
        public void FindNumbers(string equation, int start, char symbol)
        {
           String number1String = string.Empty;
           String number2String = string.Empty;
                for (int j = start - 1; j >= 0; j--)
                {
                    if (char.IsDigit(equation[j]))
                    {
                        number1String = equation[j] + number1String;
                    }
                    else if (equation[j] == '-' && j == 0)
                    {
                        number1String = equation[j] + number1String;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int j = start + 1; j < equation.Length; j++)
                {
                    if (char.IsDigit(equation[j]))
                    {
                        number2String += equation[j];
                    }
                    else
                    {
                        break;
                    }
                }
                number1 = int.Parse(number1String);
                number2 = int.Parse(number2String);
            return;
        }
        
        public string Operations(string equation, int start)
        {
            for (int i = start; i < equation.Length; i++)
            {
                if (equation[i] == '*')
                {
                    symbol = equation[i];
                    FindNumbers(equation, i, symbol);
                    i = -1;
                    currentresult = number1 * number2;
                    equation = equation.Replace(number1.ToString() + symbol + number2.ToString(),
                        currentresult.ToString());
                }
                else if (equation[i] == '/')
                {
                    symbol = equation[i];
                    FindNumbers(equation, i, symbol);
                    i = -1;
                    currentresult = number1 / number2;
                    equation = equation.Replace(number1.ToString() + symbol + number2.ToString(),
                        currentresult.ToString());
                }
            }
            for (int i = start; i < equation.Length; i++)
            {
                if (equation[i] == '+')
                {
                    symbol = equation[i];
                    FindNumbers(equation, i, symbol);
                    i = -1;
                    currentresult = number1 + number2;
                    equation = equation.Replace(number1.ToString() + symbol + number2.ToString(),
                        currentresult.ToString());
                }
                else if (equation[i] == '-' && i != 0)
                {
                    symbol = equation[i];
                    FindNumbers(equation, i, symbol);
                    i = -1;
                    currentresult = number1 - number2;
                    equation = equation.Replace(number1.ToString() + symbol + number2.ToString(),
                        currentresult.ToString());
                }
            }
            return equation;
        }
    }
}
