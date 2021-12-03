using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += AcButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalsButton.Click += EqualsButton_Click;
        }

        private void EqualsButton_Click( object sender, RoutedEventArgs e ) {
            double newNumber;

            if (double.TryParse( resultLabel.Content.ToString(), out newNumber )) {
                switch (selectedOperator) {
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Subtract( lastNumber, newNumber );
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply( lastNumber, newNumber );
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Divide( lastNumber, newNumber );
                        break;
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add( lastNumber, newNumber );
                        break;
                }

                resultLabel.Content = result.ToString();
            }

        }

        private void PercentageButton_Click( object sender, RoutedEventArgs e ) {
            if (double.TryParse( resultLabel.Content.ToString(), out lastNumber )) {
                lastNumber = lastNumber / 100;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void NegativeButton_Click( object sender, RoutedEventArgs e ) {
            if(double.TryParse(resultLabel.Content.ToString(), out lastNumber )) {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click( object sender, RoutedEventArgs e ) {
            resultLabel.Content = 0;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e ) {

            if (double.TryParse( resultLabel.Content.ToString(), out lastNumber )) {
                resultLabel.Content = "0";
            }

            if(sender == multiplyButton) {
                selectedOperator = SelectedOperator.Multiplication;
            } else if (sender == divideButton) {
                selectedOperator = SelectedOperator.Division;
            } else if (sender == plusButton) {
                selectedOperator = SelectedOperator.Addition;
            } else if (sender == minusButton) {
                selectedOperator = SelectedOperator.Subtraction;
            }
            
        }

        private void periodButton_Click( object sender, RoutedEventArgs e ) {

            if (!resultLabel.Content.ToString().Contains( "." )) {
                resultLabel.Content = $"{resultLabel.Content}.";
            }
        }

        private void NumberButton_Click( object sender, RoutedEventArgs e ) {

            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if(resultLabel.Content.ToString() == "0") {
                resultLabel.Content = $"{selectedValue}";
            } else {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }
    }

    public class SimpleMath {
        public static double Add(double n1, double n2 ) {
            return n1 + n2;
        }
        public static double Subtract( double n1, double n2 ) {
            return n1 - n2;
        }
        public static double Multiply( double n1, double n2 ) {
            return n1 * n2;
        }
        public static double Divide( double n1, double n2 ) {
            return n1 / n2;
        }
    }

    public enum SelectedOperator {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }
}
