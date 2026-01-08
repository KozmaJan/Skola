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

using System.Windows.Threading;

namespace Míchátko
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _hoverTimer;
        private Random r = new Random();
        public MainWindow()
        {
            InitializeComponent();

            _hoverTimer = new DispatcherTimer();
            _hoverTimer.Interval = TimeSpan.FromSeconds(0.005);
            _hoverTimer.Tick += HoverTimer_Tick;
        }
        public void ButtonClick(object sender, RoutedEventArgs s)
        {
            var bc = new BrushConverter();
            Random r = new Random();

            if (txtBox.Text == "")
            {
                //  MessageBox.Show("Zadej text debil", "Pozor", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Butt.Content = txtBox.Text;
                //Butt.Background = (Brush)bc.ConvertFrom("#FF297C77");
                Butt.Background = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255),
                (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
            }
        }
        public void Hover(object sender, RoutedEventArgs s)
        {
            var bc = new BrushConverter();
            Butt.Background = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255),
                    (byte)r.Next(1, 255), (byte)r.Next(1, 233)));

            _hoverTimer.Start();
        }
        public void HoverLeave(object sender, RoutedEventArgs s) {
            var bc = new BrushConverter();
            Butt.Background = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255),
                    (byte)r.Next(1, 255), (byte)r.Next(1, 233)));


            _hoverTimer.Stop();
        }
        private void HoverTimer_Tick(object? sender, EventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromRgb(
                (byte)r.Next(1, 255),
                (byte)r.Next(1, 255),
                (byte)r.Next(1, 233)
            ));
        }

    }
}
