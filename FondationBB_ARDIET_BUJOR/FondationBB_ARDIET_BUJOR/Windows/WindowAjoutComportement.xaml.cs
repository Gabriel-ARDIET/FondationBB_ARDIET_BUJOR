using FondationBB_ARDIET_BUJOR.Model;
using System;
using System.Windows;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowAjoutComportement.xaml
    /// </summary>
    public partial class WindowAjoutComportement : Window
    {
        // Propriété exposant l'objet Comportement complet sélectionné
        public Comportement ComportementSelectionne { get; private set; }

        // Indicateur de validation
        private bool _donneesValidees = false;

        public WindowAjoutComportement()
        {
            InitializeComponent();

            // Chargement dynamique des comportements depuis PostgreSQL
            try
            {
                cbComportements.ItemsSource = new Comportement().FindAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des comportements : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            // Récupération directe de l'objet métier sélectionné
            Comportement comportementChoisi = cbComportements.SelectedItem as Comportement;

            if (comportementChoisi == null)
            {
                MessageBox.Show("Veuillez sélectionner un comportement.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Sauvegarde de l'instance sélectionnée (contient l'Id et le Libelle)
            ComportementSelectionne = comportementChoisi;

            _donneesValidees = true; // Empêche le prompt de confirmation à la fermeture
            this.DialogResult = true;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Déclenchera Window_Closing
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_donneesValidees) return;

            MessageBoxResult result = MessageBox.Show("Annuler la saisie ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true; // Bloque la fermeture de la fenêtre
            }
        }
    }
}