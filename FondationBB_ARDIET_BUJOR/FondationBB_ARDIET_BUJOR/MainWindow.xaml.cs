using FondationBB_ARDIET_BUJOR.Model;
using System.Data;
using System.Windows;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class MainWindow : Window
    {
        private Employe employeConnecte;
        public Data laData;
        public MainWindow()
        {
            InitializeComponent();
            MenuHeader.Visibility= Visibility.Collapsed;
            ShowConnexion();
            laData = (Data)this.DataContext;
        }

        private void ShowConnexion()
        {
            UCConnexion connexion = new UCConnexion();
            //on abonne la méthode ci-dessous auprès de l’evenement     
            connexion.LoginReussi += ValiderConnexion;
            ZoneContenu.Content = connexion;
        }

        /// <summary>
        /// Méthode appelée suite à une authentification réussie
        /// </summary>
        public void ValiderConnexion(object? sender, Employe emp)
        {
            employeConnecte = emp;
            MenuHeader.Visibility = Visibility.Visible;
            TxtNomEmploye.Text = "Bienvenue " + employeConnecte.Nom + " " + employeConnecte.Prenom;
            laData.ChargerAnimaux();
            ZoneContenu.Content = new UCListeAnimaux();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            var bouton = sender as System.Windows.Controls.Button;
            if (bouton == null) return;

            switch (bouton.Name)
            {
                case "btnAnimaux":
                    laData.ChargerAnimaux();
                    ZoneContenu.Content = new UCListeAnimaux();
                    break;
                case "btnClients":
                    laData.ChargerPersonnes();
                    ZoneContenu.Content = new UCListeClients();
                    break;
                case "btnAdoptions":
                    laData.ChargerAdoptions();
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