using System;
using System.Windows;
using System.Windows.Controls;

namespace calculator_2
{
    public partial class MainWindow : Window
    {
        private const string SetFirstNumber = "FirstNumber";
        private const string SetSecondNumber = "SecondNumber";

        private bool _lastButtonWasOperator = false;
        private string _state;
        private string _operatorSymbol;
        private bool cleartext = false;

        public MainWindow()
        {
            InitializeComponent();
            _operatorSymbol = string.Empty;
            _state = SetFirstNumber;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            answerbox.Text = string.Empty;
            _lastButtonWasOperator = false;
        }

        private void Number_Click(object sender, RoutedEventArgs e)

        {
            if (cleartext == true)
            {
                answerbox.Text = "";
                cleartext = false;
            }
            Button clickedButton = (Button)sender;
            answerbox.Text += clickedButton.Content.ToString();
            _lastButtonWasOperator = false;
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            string operatorSymbol = clickedButton.Content.ToString() ?? string.Empty;
            if (!CanSetOperator(operatorSymbol))
            {
                return;
            }

            if (_lastButtonWasOperator == true)
            {
                // Remove the last operator from the answerbox
                answerbox.Text = answerbox.Text.Remove(answerbox.Text.Length - 1);
            }

            answerbox.Text += operatorSymbol;
            _lastButtonWasOperator = true;

            if (_state == SetFirstNumber)
            {
                _operatorSymbol = operatorSymbol;
                _state = SetSecondNumber;
                return;
            }

            string[] numbersInText = answerbox.Text.Split(_operatorSymbol);
            string numbersBeforeOperator = numbersInText[0];
            string numbersAfterOperator = numbersInText[1];

            if (numbersAfterOperator == string.Empty)
            {
                return;
            }

            numbersAfterOperator = numbersAfterOperator.Substring(0, numbersAfterOperator.Length - 1);

            double doubleBeforeOperator = Convert.ToDouble(numbersBeforeOperator);
            double doubleAfterOperator = Convert.ToDouble(numbersAfterOperator);
            double result = BerekenResultaaat(doubleBeforeOperator, doubleAfterOperator);
            answerbox.Text += result;
            _state = SetFirstNumber;
            numbersAfterOperator = null;
        }


        private double BerekenResultaaat(double doubleBeforePlus, double doubleAfterPlus)
        {
            double antwoord = 0;
            cleartext = true;
            _lastButtonWasOperator = false;

            if (double.IsNaN(doubleBeforePlus) || double.IsNaN(doubleAfterPlus))
            {
                MessageBox.Show("Ongeldige invoer. Voer geldige getallen in.");
                answerbox.Text = string.Empty; // Clear the textbox
                return antwoord;
            }

            switch (_operatorSymbol)
            {
                case "+":
                    antwoord = doubleBeforePlus + doubleAfterPlus;
                    break;
                case "-":
                    antwoord = doubleBeforePlus - doubleAfterPlus;
                    break;
                case "*":
                    antwoord = doubleBeforePlus * doubleAfterPlus;
                    break;
                case "÷":
                    antwoord = doubleBeforePlus / doubleAfterPlus;
                    break;

            }
            return antwoord;
            
        }

        private bool CanSetOperator(string value)
        {
            return !_lastButtonWasOperator;

        }
        

        private void exit_button(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Word gesloten");
            Environment.Exit(0);
        }

        private void backspace_Click(object sender, RoutedEventArgs e)
        {
            if (answerbox.Text.Length <= 0)
            {
                MessageBox.Show("Er is niets om te verwijderen");
                _lastButtonWasOperator = false;
            }
            else
            {
                answerbox.Text = answerbox.Text.Remove(answerbox.Text.Length - 1);
                _lastButtonWasOperator = false;
            }
        }

    }
}
