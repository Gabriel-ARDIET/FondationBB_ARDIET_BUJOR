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

        /// <summary>
        /// Filtre combinant les critères de recherche : Nom adoptant, Prénom adoptant et Nom de l'animal.
        /// </summary>
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

            // Si le contrat d'adoption valide tous les critères actifs
            return true;
        }

        /// <summary>
        /// Déclenché à chaque saisie utilisateur dans l'un des trois champs de texte
        /// </summary>
        private void FiltreAdoption_Changed(object sender, TextChangedEventArgs e)
        {
            if (dgAdoptions.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(dgAdoptions.ItemsSource).Refresh();
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e) { }
        private void BtnAjouter_Click(object sender, RoutedEventArgs e) { }
        private void BtnEditer_Click(object sender, RoutedEventArgs e) { }
    }
}