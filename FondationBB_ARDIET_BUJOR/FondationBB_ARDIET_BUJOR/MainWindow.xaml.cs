using System.Windows;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Au démarrage de la MainWindow, on charge le UserControl ListeAnimaux par défaut
            ZoneContenu.Content = new ListeAnimaux();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            // On récupère le bouton qui a été cliqué
            var bouton = sender as System.Windows.Controls.Button;

            // On change le contenu selon le bouton cliqué
            switch (bouton.Name)
            {
                case "btnAnimaux":
                    ZoneContenu.Content = new ListeAnimaux();
                    break;
                case "btnClients":
                    ZoneContenu.Content = new ListeClients();
                    break;
                case "btnAdoptions":
                    ZoneContenu.Content = new ListeAdoptions();
                    break;
                case "btnDemandes":
                    ZoneContenu.Content = new ListeDemandes();
                    break;
                case "btnStatistiques":
                    ZoneContenu.Content = new Statistiques();
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
                Connexion connexionWindow = new Connexion();
                connexionWindow.Show();
                this.Close();
            }
        }
    }
}