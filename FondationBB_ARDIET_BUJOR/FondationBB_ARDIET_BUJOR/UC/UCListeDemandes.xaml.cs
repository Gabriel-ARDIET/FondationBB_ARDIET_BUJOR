using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class UCListeDemandes : UserControl
    {
        public UCListeDemandes()
        {
            InitializeComponent();
        }

        private void FiltreDemande_Changed(object sender, RoutedEventArgs e)
        {
            if (dgDemandes.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(dgDemandes.ItemsSource).Refresh();
            }
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditer_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnSupprimer_Click(object sender, RoutedEventArgs e) { }
    }
}