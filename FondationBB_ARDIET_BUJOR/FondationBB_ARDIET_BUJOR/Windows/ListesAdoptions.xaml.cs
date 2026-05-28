using FondationBB_ARDIET_BUJOR.Windows;
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


namespace TD3_BindingBDPension.Windows
{
    public partial class ListeAdoptions : Window
    {
        public ListeAdoptions()
        {
            InitializeComponent();
        }

        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir vous déconnecter ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Connexion listeWindow = new Connexion();
                listeWindow.Show();
                this.Close();
            }
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            ListeClients listeWindow = new ListeClients();
            listeWindow.Show();
            this.Close();
        }

        private void btnAnimaux_Click(object sender, RoutedEventArgs e)
        {
            ListeAnimaux listeWindow = new ListeAnimaux();
            listeWindow.Show();
            this.Close();
        }
        private void btnDemandes_Click(object sender, RoutedEventArgs e)
        {
            ListeDemandes listeWindow = new ListeDemandes();
            listeWindow.Show();
            this.Close();
        }

        private void btnStatistiques_Click(object sender, RoutedEventArgs e)
        {
            Statistiques listeWindow = new Statistiques();
            listeWindow.Show();
            this.Close();
        }
    }
}
