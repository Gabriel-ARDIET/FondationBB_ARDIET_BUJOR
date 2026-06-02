using FondationBB_ARDIET_BUJOR.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class WindowAnimal : Window
    {
        // Indicateur pour savoir si on ferme suite à une validation réussie
        private bool _donneesValidees = false;

        public WindowAnimal(object unAnimal)
        {
            this.DataContext = unAnimal;
            InitializeComponent();
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            foreach (UIElement uie in panelFormAnimal.Children)
            {
                if (uie is TextBox txt)
                {
                    var binding = txt.GetBindingExpression(TextBox.TextProperty);
                    binding?.UpdateSource();
                }
                if (Validation.GetHasError(uie)) ok = false;
            }

            if (ok)
            {
                _donneesValidees = true; // /!\ IMPORTANT : On indique que c'est validé
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Veuillez corriger les erreurs de saisie.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {

        }

        // Cet événement gère la croix ET le bouton annuler
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Si l'utilisateur a cliqué sur "Valider", on ferme directement sans poser de question
            if (_donneesValidees) return;

            // Sinon, on demande confirmation
            MessageBoxResult result = MessageBox.Show("Annuler la saisie ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true; // Annule la fermeture de la fenêtre
            }
        }
        private void btnAjouterSoin_Click(object sender, RoutedEventArgs e)
        {
            WindowAjoutSoins fenetreSoins = new WindowAjoutSoins();
            fenetreSoins.Owner = this;

            // Si l'utilisateur clique sur "Enregistrer le soin" et que le DialogResult est true
            if (fenetreSoins.ShowDialog() == true)
            {
                // 1. Récupération de l'animal depuis le DataContext
                Animal animalActuel = this.DataContext as Animal;

                if (animalActuel != null)
                {
                    // 2. Création de la relation "Recoit" entre l'Animal et le Soin
                    Recoit nouveauSoinRecu = new Recoit();
                    nouveauSoinRecu.UnSoin = fenetreSoins.SoinSelectionne;
                    nouveauSoinRecu.DateSoin = fenetreSoins.DateSelectionnee;

                    // 3. Ajout à la collection (la DataGrid se mettra à jour automatiquement !)
                    animalActuel.SoinReçus.Add(nouveauSoinRecu);
                }
            }
        }
        private void btnAjouterComportement_Click(object sender, RoutedEventArgs e)
        {
            WindowAjoutComportement fenetreComportement = new WindowAjoutComportement();
            fenetreComportement.Owner = this;

            // Si l'utilisateur clique sur "Enregistrer" et que DialogResult est true
            if (fenetreComportement.ShowDialog() == true)
            {
                // 1. Récupération de l'animal depuis le DataContext
                Animal animalActuel = this.DataContext as Animal;

                if (animalActuel != null)
                {
                    // 2. Création de l'objet Comportement
                    Comportement nouveauComportement = new Comportement();

                    // /!\ Attention : Remplacez ".Libelle" par le nom exact de la propriété 
                    // de votre classe Comportement (par exemple .Nom, .Libelle, etc.)
                    nouveauComportement.Libelle = fenetreComportement.ComportementSelectionne;

                    // 3. Ajout à la collection
                    animalActuel.Comportements.Add(nouveauComportement);
                }
            }
        }
        private void btnSupprimerSoin_Click(object sender, RoutedEventArgs e)
        {
            // 1. Vérification et récupération de la ligne sélectionnée dans le DataGrid
            if (dgSoins.SelectedItem is Recoit soinSelectionne)
            {
                // 2. Récupération de l'animal depuis le DataContext
                if (this.DataContext is Animal animalActuel)
                {
                    // 3. Suppression de l'élément (le DataGrid se mettra à jour si c'est une ObservableCollection)
                    animalActuel.SoinReçus.Remove(soinSelectionne);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un soin dans le tableau pour le supprimer.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnSupprimerComportement_Click(object sender, RoutedEventArgs e)
        {
            // 1. Vérification et récupération de la ligne sélectionnée dans le DataGrid
            if (dgComportements.SelectedItem is Comportement comportementSelectionne)
            {
                // 2. Récupération de l'animal depuis le DataContext
                if (this.DataContext is Animal animalActuel)
                {
                    // 3. Suppression de l'élément
                    animalActuel.Comportements.Remove(comportementSelectionne);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un comportement dans le tableau pour le supprimer.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
