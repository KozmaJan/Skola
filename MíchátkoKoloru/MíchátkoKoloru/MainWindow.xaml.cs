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
using System.Windows.Threading;


namespace MíchátkoKoloru
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BrushConverter bc = new BrushConverter();
        private static readonly Regex _numeric = new Regex("^[0-9]+$");

        private DispatcherTimer PartyTimer = new DispatcherTimer();
        private DispatcherTimer LavaTimer = new DispatcherTimer();
        private Random r = new Random();

        private int targetR = 225;
        private int targetG = 225;
        private int targetB = 225;
        private double speed = 0.1;

        public MainWindow()
        {
            InitializeComponent();
            SliderUpdate();

            PartyTimer.Interval = TimeSpan.FromSeconds(0.5);
            PartyTimer.Tick += PartyTick;
            LavaTimer.Interval = TimeSpan.FromSeconds(0.05);
            LavaTimer.Tick += LavaTick;
        }
        public void SliderUpdate()
        {
            TextboxRed.Text = Convert.ToString((int)SliderRed.Value);
            TextboxGreen.Text = Convert.ToString((int)SliderGreen.Value);
            TextboxBlue.Text = Convert.ToString((int)SliderBlue.Value);
            ChangeColor((int)SliderRed.Value, (int)SliderGreen.Value, (int)SliderBlue.Value);
        }
        public void ChangeColor(int r, int g, int b) {
            Rect.Fill = new SolidColorBrush(Color.FromRgb((byte)r, (byte)g, (byte)b));
        }
        private void PreviewNumInput(object sender, TextCompositionEventArgs e)
        {
           
            if (!_numeric.IsMatch(e.Text)) //Zkontroluje jestli splňuje Regex a je jen číslo
            {
                e.Handled = true;
                MessageBox.Show("Jen celá čísla od 0 do 255.", "Pozor", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TextBox textbox = (TextBox)sender;

            int caretPos = textbox.SelectionStart + e.Text.Length; //uloží novou pozici kurzoru


            string newText = textbox.Text.Insert(textbox.SelectionStart, e.Text);//pokud splńuje regex, přídá nově prídáný znak (e.Text) jinak vrátí před jeho přidání

            if (!int.TryParse(newText, out int value) || value < 0 || value > 255) //zkontroluje jestli je text není int, menší než nula, větší než 255
            {
                e.Handled = true; //ukončí event, nový znak se nezapíše
                MessageBox.Show("Jen celá čísla od 0 do 255.", "Pozor", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (textbox.Name == "TextboxRed") {
                SliderRed.Value = value;
                textbox.Text = Convert.ToString(value);
            }
            else if (textbox.Name == "TextboxGreen") {
                SliderGreen.Value = value;
                textbox.Text = Convert.ToString(value);
            }
            else if (textbox.Name == "TextboxBlue") {
                SliderBlue.Value = value;
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
        public void Random(object sender, RoutedEventArgs s) {
            SliderRed.Value = r.Next(0, 256);
            SliderGreen.Value = r.Next(0, 256);
            SliderBlue.Value = r.Next(0, 256);
            SliderUpdate();
        }
        private void Lavalamp(object sender, RoutedEventArgs s)
        {
            if (!LavaTimer.IsEnabled)
            {
                LavaTimer.Start();
                targetR = r.Next(0, 255);
                targetG = r.Next(0, 255);
                targetB = r.Next(0, 255);
            }
            else
            {
                LavaTimer.Stop();
            }
        }
        private void Party(object sender, RoutedEventArgs s)
        {
           
            if (!PartyTimer.IsEnabled)
            {
                PartyTimer.Start();
            }
            else
            {
                PartyTimer.Stop();
            }

        }
        private void PartyTick(object? sender, EventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromRgb(
                (byte)r.Next(1, 255),
                (byte)r.Next(1, 255),
                (byte)r.Next(1, 255)
            ));
        }
        private void LavaTick(object? sender, EventArgs e)
        {
            if (targetR - SliderRed.Value <= 1 && targetR - SliderRed.Value >= -1)
            {
                targetR = r.Next(0, 255);
                targetG = r.Next(0, 255);
                targetB = r.Next(0, 255);
            }
            else
            {
                SliderRed.Value += (targetR - SliderRed.Value) * speed;
                SliderGreen.Value += (targetG - SliderGreen.Value) * speed;
                SliderBlue.Value += (targetB - SliderBlue.Value) * speed;
            }
            SliderUpdate();
        }
    }
}
