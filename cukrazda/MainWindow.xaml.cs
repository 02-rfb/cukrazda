using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cukrazda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<OrderItem> items;
        public MainWindow()
        { 
            InitializeComponent();

        }
        void Megnyit_Click(object sender, RoutedEventArgs args)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            try
            {
                items = File.ReadAllLines(dialog.FileName)
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(l => l.Split(';'))
                    .Select(p => new OrderItem { Name = p[0].Trim(), Price = int.Parse(p[1].Trim()) })
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hibás fájl: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            RendelButton.IsEnabled = true;
            new arlista(items).ShowDialog();


        }

        void Rendel_Click(object sender, RoutedEventArgs args)
        {
            new arlista(items).ShowDialog();
        }

    }
}