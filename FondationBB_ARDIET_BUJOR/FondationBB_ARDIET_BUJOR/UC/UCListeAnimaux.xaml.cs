using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using FondationBB_ARDIET_BUJOR.Model;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class UCListeAnimaux : UserControl
    {
        private Data laData;

        public UCListeAnimaux()
        {
            InitializeComponent();

            laData = (Data)Application.Current.MainWindow.DataContext;
            this.DataContext = laData;

            // CRUCIAL : On applique la fonction FiltreCombine à la vue par défaut du DataGrid
            ICollectionView view = CollectionViewSource.GetDefaultView(laData.LesAnimaux);
            if (view != null)
            {
                view.Filter = FiltreCombine;
            }
        }

        /// <summary>
        /// Combine absolument tous les filtres (Nom, Race/Espèce, Sexe et Statut/Disponibilité)
        /// </summary>
        private bool FiltreCombine(object obj)
        {
            Animal unAnimal = obj as Animal;
            if (unAnimal == null) return false;

            // 1. --- FILTRE NOM ---
            if (!RechercheMotClefAnimal_Nom(unAnimal)) return false;

            // 2. --- FILTRE RACE --- (Adapté selon ton souhait "Recherche de race")
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
            // Récupère la chaîne de caractères (Libelle ou ToString) représentant le statut
            string statutAnimal = unAnimal.UnStatut?.Libelle ?? unAnimal.UnStatut?.ToString() ?? "";

            if (rbAdopte.IsChecked == true)
            {
                if (!string.Equals(statutAnimal, "Adopte", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(statutAnimal, "Adopté", StringComparison.OrdinalIgnoreCase))
                    return false;
            }
            else if (rbAuRefuge.IsChecked == true)
            {
                // Un animal est au refuge s'il n'est pas "Adopté" et pas "Décédé" (ajuste selon tes besoins)
                bool estAuRefuge = string.Equals(statutAnimal, "En soin", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(statutAnimal, "Disponible", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(statutAnimal, "Reserve", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(statutAnimal, "Réservé", StringComparison.OrdinalIgnoreCase);

                if (!estAuRefuge) return false;
            }

            // Si l'animal passe toutes les étapes, il est affiché !
            return true;
        }

        private void FiltreAnimal_Changed(object sender, RoutedEventArgs e)
        {
            // Rafraîchit l'affichage dès qu'un texte change ou qu'un bouton radio est cliqué
            if (dgAnimaux.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(dgAnimaux.ItemsSource).Refresh();
            }
        }

        // --- SOUS-FONCTIONS DE RECHERCHE TEXTUELLE ---

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

            // Sécurité si les objets liés sont null
            if (unAnimal.UneRace == null) return false;

            // On cherche d'abord dans le libellé de la Race, sinon subsidiairement dans l'Espèce
            string raceLibelle = unAnimal.UneRace.Libelle ?? unAnimal.UneRace.ToString() ?? "";
            string especeLibelle = unAnimal.UneRace.UneEspece?.Libelle ?? unAnimal.UneRace.UneEspece?.ToString() ?? "";

            return raceLibelle.StartsWith(txtFiltreEspece.Text, StringComparison.OrdinalIgnoreCase) ||
                   especeLibelle.StartsWith(txtFiltreEspece.Text, StringComparison.OrdinalIgnoreCase);
        }

        // --- BOUTONS D'ACTION (Ajouter, Éditer, Supprimer) ---

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Animal unAnimal = new Animal();
            WindowAnimal wAnimal = new WindowAnimal(unAnimal);
            bool? result = wAnimal.ShowDialog();

            if (result == true)
            {
                try
                {
                    unAnimal.Id = unAnimal.Create();
                    ((Data)this.DataContext).LesAnimaux.Add(unAnimal);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"L'animal n'a pas pu être créé.\nDétails : {ex.Message}", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnEditer_Click(object sender, RoutedEventArgs e)
        {
            if (dgAnimaux.SelectedItem is Animal animalSelectionne)
            {
                // Ouvre la fenêtre d'édition en lui passant l'animal sélectionné
                WindowAnimal wAnimal = new WindowAnimal(animalSelectionne);
                bool? result = wAnimal.ShowDialog();

                if (result == true)
                {
                    try
                    {
                        // Option A : Si tu as une méthode SQL pour sauvegarder les modifs, décommente la ligne suivante :
                        // animalSelectionne.Update(); 

                        // Option B : Si la modification en base de données est gérée directement à l'intérieur de 'WindowAnimal',
                        // il suffit de forcer le rafraîchissement de l'affichage ici :
                        FiltreAnimal_Changed(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la modification : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un animal à modifier.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dgAnimaux.SelectedItem is Animal animalSelectionne)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Êtes-vous sûr de vouloir supprimer définitivement l'animal '{animalSelectionne.Nom}' (ICAD: {animalSelectionne.Icad}) ?",
                    "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        int lignesAffectees = animalSelectionne.Delete();
                        if (lignesAffectees > 0)
                        {
                            MessageBox.Show("L'animal a bien été supprimé.", "Suppression réussie", MessageBoxButton.OK, MessageBoxImage.Information);
                            if (this.DataContext is ObservableCollection<Animal> listeObservable)
                            {
                                listeObservable.Remove(animalSelectionne);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Une erreur est survenue lors de la suppression : {ex.Message}", "Erreur critique", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un animal avant de cliquer sur supprimer.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dgAnimaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Recoit>soins = new List<Recoit>();
            foreach (Recoit r in ((Data)DataContext).LesSoinsReçus)
            {
                if (dgAnimaux.SelectedItem != null)
                    if (r.IdAnimal == ((Animal)dgAnimaux.SelectedItem).Id)
                        soins.Add(r);
            }
            dgSoins.ItemsSource = soins;
            CollectionViewSource.GetDefaultView(dgSoins.ItemsSource).Refresh();
        }
    }
}