using System.Windows;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ZoneContenu.Content = new UCConnexion();
        }

        /// <summary>
        /// Méthode appelée suite à une authentification réussie
        /// </summary>
        public void ValiderConnexion()
        {
            MenuHeader.Visibility = Visibility.Visible;
            ZoneContenu.Content = new UCListeAnimaux();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            var bouton = sender as System.Windows.Controls.Button;
            if (bouton == null) return;

            switch (bouton.Name)
            {
                case "btnAnimaux":
                    ZoneContenu.Content = new UCListeAnimaux();
                    break;
                case "btnClients":
                    ZoneContenu.Content = new UCListeClients();
                    break;
                case "btnAdoptions":
                    ZoneContenu.Content = new UCListeAdoptions();
                    break;
                case "btnDemandes":
                    ZoneContenu.Content = new UCListeDemandes();
                    break;
                case "btnStatistiques":
                    ZoneContenu.Content = new UCStatistiques();
                    break;
            }
        }

        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Êtes-vous sûr de vouloir vous déconnecter ?",
                "Confirmation de déconnexion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MenuHeader.Visibility = Visibility.Collapsed;
                ZoneContenu.Content = new UCConnexion();
            }
        }
    }
}