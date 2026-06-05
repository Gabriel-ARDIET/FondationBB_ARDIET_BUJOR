using FondationBB_ARDIET_BUJOR.Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class UCListeAdoptions : UserControl
    {
        private Data laData;

        public UCListeAdoptions()
        {
            InitializeComponent();

            laData = (Data)Application.Current.MainWindow.DataContext;
            this.DataContext = laData.LesAdoptions;

            // Applique le filtre combiné sur la vue par défaut liée à la collection
            ICollectionView view = CollectionViewSource.GetDefaultView(this.DataContext);
            if (view != null)
            {
                view.Filter = FiltreCombineAdoption;
            }
        }

        private bool FiltreCombineAdoption(object obj)
        {
            Adoption uneAdoption = obj as Adoption;
            if (uneAdoption == null) return false;

            // 1. --- FILTRE NOM CLIENT ---
            if (!string.IsNullOrEmpty(txtFiltreNom.Text))
            {
                if (uneAdoption.Adoptant == null || uneAdoption.Adoptant.Nom == null ||
                    !uneAdoption.Adoptant.Nom.StartsWith(txtFiltreNom.Text, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            // 2. --- FILTRE PRÉNOM CLIENT ---
            if (!string.IsNullOrEmpty(txtFiltrePrenom.Text))
            {
                if (uneAdoption.Adoptant == null || uneAdoption.Adoptant.Prenom == null ||
                    !uneAdoption.Adoptant.Prenom.StartsWith(txtFiltrePrenom.Text, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            // 3. --- FILTRE NOM ANIMAL ---
            if (!string.IsNullOrEmpty(txtFiltreAnimal.Text))
            {
                if (uneAdoption.UnAnimal == null || uneAdoption.UnAnimal.Nom == null ||
                    !uneAdoption.UnAnimal.Nom.StartsWith(txtFiltreAnimal.Text, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }

        private void FiltreAdoption_Changed(object sender, TextChangedEventArgs e)
        {
            if (dgAdoptions.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(dgAdoptions.ItemsSource).Refresh();
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dgAdoptions.SelectedItem is Adoption adoptionSelectionnee)
            {
                string nomAdoptant = adoptionSelectionnee.Adoptant?.Nom ?? "Inconnu";
                string nomAnimal = adoptionSelectionnee.UnAnimal?.Nom ?? "l'animal";

                MessageBoxResult confirmation = MessageBox.Show(
                    $"Êtes-vous sûr de vouloir supprimer définitivement le contrat d'adoption de {nomAnimal} par {nomAdoptant} ?",
                    "Confirmation de suppression",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (confirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Retrait de la collection observable globale
                        laData.SupprimerAdoption(adoptionSelectionnee);
                        MessageBox.Show("Le contrat d'adoption a été supprimé.", "Suppression réussie", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Impossible de supprimer cette adoption.\n\nDétails : " + ex.Message,
                                        "Erreur de contrainte", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une adoption à supprimer.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Adoption nouvelleAdoption = new Adoption();
            nouvelleAdoption.Adoptant = new Personne(); // Initialisation de l'objet imbriqué
            nouvelleAdoption.DateAdoption = DateTime.Today; // Valeur par défaut pratique

            WindowAdoption fenetreAdoption = new WindowAdoption(nouvelleAdoption);
            fenetreAdoption.Owner = Application.Current.MainWindow;

            if (fenetreAdoption.ShowDialog() == true)
            {
                try
                {
                    // Ajout direct dans la liste globale
                    laData.LesAdoptions.Add(nouvelleAdoption);
                    nouvelleAdoption.Create();
                    MessageBox.Show("Le contrat d'adoption a été créé avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l'enregistrement :\n" + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnEditer_Click(object sender, RoutedEventArgs e)
        {
            if (dgAdoptions.SelectedItem is Adoption adoptionSelectionnee)
            {
                WindowAdoption fenetreAdoption = new WindowAdoption(adoptionSelectionnee);
                fenetreAdoption.Owner = Application.Current.MainWindow;

                if (fenetreAdoption.ShowDialog() == true)
                {
                    try
                    {
                        // Rafraîchit l'affichage du DataGrid pour répercuter les modifications
                        adoptionSelectionnee.Update();
                        CollectionViewSource.GetDefaultView(dgAdoptions.ItemsSource).Refresh();
                        MessageBox.Show("Le contrat d'adoption a été mis à jour.", "Modification enregistrée", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la mise à jour :\n" + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    CollectionViewSource.GetDefaultView(dgAdoptions.ItemsSource).Refresh();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un contrat d'adoption à modifier dans le tableau.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void BtnTelechargerContrat_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fonction non implémentée", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}