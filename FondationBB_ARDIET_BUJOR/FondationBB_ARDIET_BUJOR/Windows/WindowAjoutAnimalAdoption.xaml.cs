using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using FondationBB_ARDIET_BUJOR.Model;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class WindowAjoutAnimalAdoption : Window
    {
        private Data laData;

        // Propriété publique permettant de récupérer l'animal choisi après le ShowDialog()
        public Animal AnimalSelectionne { get; private set; }

        public WindowAjoutAnimalAdoption()
        {
            InitializeComponent();

            // Récupération du contexte de données global
            laData = (Data)Application.Current.MainWindow.DataContext;
            this.DataContext = laData;

            // Application de la fonction de filtrage combiné sur la vue par défaut
            ICollectionView view = CollectionViewSource.GetDefaultView(laData.LesAnimaux);
            if (view != null)
            {
                view.Filter = FiltreCombine;
                view.Refresh(); // Force le rafraîchissement à l'ouverture
            }
        }

        /// <summary>
        /// Logique de filtrage identique à celle de UCListeAnimaux
        /// </summary>
        private bool FiltreCombine(object obj)
        {
            Animal unAnimal = obj as Animal;
            if (unAnimal == null) return false;

            // 1. --- FILTRE NOM ---
            if (!RechercheMotClefAnimal_Nom(unAnimal)) return false;

            // 2. --- FILTRE RACE / ESPÈCE ---
            if (!RechercheMotClefAnimal_Race(unAnimal)) return false;

            // 3. --- FILTRE SEXE ---
            if (rbMale.IsChecked == true)
            {
                string sexeStr = unAnimal.UnSexe?.ToString() ?? "";
                if (!string.Equals(sexeStr, "Male", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(sexeStr, "Mâle", StringComparison.OrdinalIgnoreCase))
                    return false;
            }
            else if (rbFemelle.IsChecked == true)
            {
                string sexeStr = unAnimal.UnSexe?.ToString() ?? "";
                if (!string.Equals(sexeStr, "Femelle", StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            // 4. --- FILTRE STATUT / DISPONIBILITÉ ---
            string statutAnimal = unAnimal.UnStatut?.Libelle ?? unAnimal.UnStatut?.ToString() ?? "";

            if (rbAdopte.IsChecked == true)
            {
                if (!string.Equals(statutAnimal, "Adopte", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(statutAnimal, "Adopté", StringComparison.OrdinalIgnoreCase))
                    return false;
            }
            else if (rbAuRefuge.IsChecked == true)
            {
                bool estAuRefuge = string.Equals(statutAnimal, "En soin", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(statutAnimal, "Disponible", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(statutAnimal, "Reserve", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(statutAnimal, "Réservé", StringComparison.OrdinalIgnoreCase);

                if (!estAuRefuge) return false;
            }

            return true;
        }

        private void FiltreAnimal_Changed(object sender, RoutedEventArgs e)
        {
            if (dgAnimaux.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(dgAnimaux.ItemsSource).Refresh();
            }
        }

        private bool RechercheMotClefAnimal_Nom(Animal unAnimal)
        {
            if (string.IsNullOrEmpty(txtFiltreNom.Text))
                return true;

            if (unAnimal.Nom == null) return false;

            return unAnimal.Nom.StartsWith(txtFiltreNom.Text, StringComparison.OrdinalIgnoreCase);
        }

        private bool RechercheMotClefAnimal_Race(Animal unAnimal)
        {
            if (string.IsNullOrEmpty(txtFiltreEspece.Text))
                return true;

            if (unAnimal.UneRace == null) return false;

            string raceLibelle = unAnimal.UneRace.Libelle ?? unAnimal.UneRace.ToString() ?? "";
            string especeLibelle = unAnimal.UneRace.UneEspece?.Libelle ?? unAnimal.UneRace.UneEspece?.ToString() ?? "";

            return raceLibelle.StartsWith(txtFiltreEspece.Text, StringComparison.OrdinalIgnoreCase) ||
                   especeLibelle.StartsWith(txtFiltreEspece.Text, StringComparison.OrdinalIgnoreCase);
        }

        // --- GESTION DES ACTIONS ---

        private void ValiderSelection()
        {
            if (dgAnimaux.SelectedItem is Animal animalSelectionne)
            {
                AnimalSelectionne = animalSelectionne;
                this.DialogResult = true; // Ferme la fenêtre en indiquant un succès
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un animal dans la liste.", "Sélection requise", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnChoisir_Click(object sender, RoutedEventArgs e)
        {
            ValiderSelection();
        }

        private void DgAnimaux_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ValiderSelection(); // Double-cliquer sur une ligne valide directement
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}