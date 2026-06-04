using FondationBB_ARDIET_BUJOR.Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class UCListeClients : UserControl
    {
        private Data laData;

        public UCListeClients()
        {
            InitializeComponent();

            laData = (Data)Application.Current.MainWindow.DataContext;
            this.DataContext = laData.LesPersonnes;

            // Abonnement au filtre sur la vue par défaut de la collection des personnes
            ICollectionView view = CollectionViewSource.GetDefaultView(this.DataContext);
            if (view != null)
            {
                view.Filter = FiltreCombineClient;
            }
        }

        /// <summary>
        /// Combine les filtres Nom et Prénom pour la liste des clients
        /// </summary>
        private bool FiltreCombineClient(object obj)
        {
            // Remplacer 'Personne' par le nom exact de votre classe modèle si nécessaire (ex: Client)
            // D'après votre code, laData.LesPersonnes contient des objets de ce type.
            var unClient = obj as Personne;
            if (unClient == null) return false;

            // 1. Filtrage par Nom
            if (!string.IsNullOrEmpty(txtFiltreNom.Text))
            {
                if (unClient.Nom == null || !unClient.Nom.StartsWith(txtFiltreNom.Text, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            // 2. Filtrage par Prénom
            if (!string.IsNullOrEmpty(txtFiltrePrenom.Text))
            {
                if (unClient.Prenom == null || !unClient.Prenom.StartsWith(txtFiltrePrenom.Text, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Déclenché à chaque modification du texte dans les filtres
        /// </summary>
        private void FiltreClient_Changed(object sender, TextChangedEventArgs e)
        {
            if (dgClients.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(dgClients.ItemsSource).Refresh();
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e) { }
        private void BtnAjouter_Click(object sender, RoutedEventArgs e) { }
        private void BtnEditer_Click(object sender, RoutedEventArgs e) { }
    }
}