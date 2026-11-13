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
using System.Windows.Shapes;

namespace cukrazda
{
    /// <summary>
    /// Interaction logic for arlista.xaml
    /// </summary>
    public partial class arlista : Window
    {
        private readonly List<OrderItem> items;

        public arlista(List<OrderItem> items)
        {
            InitializeComponent();
            this.items = items;
            ItemsGrid.ItemsSource = items;
        }

        private List<OrderItem> Selected()
        {
            ItemsGrid.CommitEdit(DataGridEditingUnit.Row, true);
            return items.Where(i => i.Order && i.Amount > 0).ToList();
        }

        private bool HasSelected(List<OrderItem> rendeles)
        {
            if (rendeles.Count > 0) return true;
            MessageBox.Show("Nincs kiválasztva egyetlen sütemény sem!", "Hiba",
                            MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        private void Rendel_Click(object sender, RoutedEventArgs e)
        {
            var rendeles = Selected();
            if (!HasSelected(rendeles)) return;

            MessageBox.Show($"Rendelés leadva: {rendeles.Count} tétel, összesen {rendeles.Sum(i => i.Price * i.Amount)} Ft.",
                            "Rendelés");
        }

        private void SzamlatKer_Click(object sender, RoutedEventArgs e)
        {
            var rendeles = Selected();
            if (!HasSelected(rendeles)) return;

            var doc = new FlowDocument();
            doc.Blocks.Add(new Paragraph(new Bold(new Run("SZÁMLA"))) { FontSize = 18 });
            foreach (var i in rendeles)
                doc.Blocks.Add(new Paragraph(new Run($"{i.Name}: {i.Amount} db × {i.Price} Ft = {i.Amount * i.Price} Ft"))
                { Margin = new Thickness(0) });
            doc.Blocks.Add(new Paragraph(new Bold(new Run($"Összesen: {rendeles.Sum(i => i.Amount * i.Price)} Ft"))));

            SzamlaBox.Document = doc;
        }
    }
}
