using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class UCListeAnimaux : UserControl // <-- Attention, ça hérite de UserControl maintenant
    {
        public UCListeAnimaux()
        {
            InitializeComponent();
        }

        private void FiltreAnimal_Changed(object sender, RoutedEventArgs e)
        {
            if (dgAnimaux.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(dgAnimaux.ItemsSource).Refresh();
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e) { }
        private void BtnAjouter_Click(object sender, RoutedEventArgs e) { }
        private void BtnEditer_Click(object sender, RoutedEventArgs e) { }

    }
}