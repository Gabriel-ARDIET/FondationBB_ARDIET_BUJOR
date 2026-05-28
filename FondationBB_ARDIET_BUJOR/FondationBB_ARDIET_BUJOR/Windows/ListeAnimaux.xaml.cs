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

namespace FondationBB_ARDIET_BUJOR.Windows
{
    /// <summary>
    /// Logique d'interaction pour ListeAnimaux.xaml
    /// </summary>
    public partial class ListeAnimaux : Window
    {
        public ListeAnimaux()
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

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}