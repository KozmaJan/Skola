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
using System.Text.RegularExpressions;

namespace Hry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BrushConverter bc = new BrushConverter();
        private static readonly Regex _numeric = new Regex("^[0-9]+$");

        public MainWindow()
        {
            InitializeComponent();
            SliderUpdate();

        }
        public void SliderUpdate()
        {
         //   TextboxWidth.Text = Convert.ToString((int)SliderWidth.Value);
         //   TextboxHeight.Text = Convert.ToString((int)SliderHeight.Value);
         //   TextboxMineCount.Text = Convert.ToString((int)SliderBombs.Value);
        }
        private void PreviewNumInput(object sender, TextCompositionEventArgs e)
        {

            if (!_numeric.IsMatch(e.Text)) //Zkontroluje jestli splňuje Regex a je jen číslo
            {
                e.Handled = true;
                return;
            }

            TextBox textbox = (TextBox)sender;

            int caretPos = textbox.SelectionStart + e.Text.Length; //uloží novou pozici kurzoru


            string newText = textbox.Text.Insert(textbox.SelectionStart, e.Text);//pokud splńuje regex, přídá nově prídáný znak (e.Text) jinak vrátí před jeho přidání

            if (!int.TryParse(newText, out int value)) //zkontroluje jestli je text není int, menší než nula, větší než 255
            {
                e.Handled = true; //ukončí event, nový znak se nezapíše
                return;
            }

            if (textbox.Name == "TextboxHeight" && value >= 4 && value   < 30)
            {
                SliderHeight.Value = value;
                textbox.Text = Convert.ToString(value);
            }
            else if (textbox.Name == "TextboxWidth" && value >= 4 && value < 30)
            {
                SliderWidth.Value = value;
                textbox.Text = Convert.ToString(value);
            }
            else if (textbox.Name == "TextboxMineCount" && value >= 1 && value < 120)
            {
                SliderBombs.Value = value;
                textbox.Text = Convert.ToString(value);
            }

            // Obnoví pozici kurzoru po změně Textu
            if (caretPos <= textbox.Text.Length)
                textbox.SelectionStart = caretPos;
            else
                textbox.SelectionStart = textbox.Text.Length;


            e.Handled = true; //brání zdvojení textu
            SliderUpdate();
        }
        private void SliderChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderUpdate();
        }
       
 

}
}
