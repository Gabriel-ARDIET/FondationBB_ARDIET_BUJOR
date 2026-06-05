using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using FondationBB_ARDIET_BUJOR.Model;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class WindowAjoutClientAdoption : Window
    {
        private Data laData;

        public Personne ClientSelectionne { get; private set; }

        public WindowAjoutClientAdoption()
        {
            InitializeComponent();

            laData = (Data)Application.Current.MainWindow.DataContext;
            this.DataContext = laData.LesPersonnes;

            ICollectionView view = CollectionViewSource.GetDefaultView(laData.LesPersonnes);
            if (view != null)
            {
                view.Filter = FiltreCombineClient;
            }
        }

        private bool FiltreCombineClient(object obj)
        {
            var unClient = obj as Personne;
            if (unClient == null) return false;

            if (!string.IsNullOrEmpty(txtFiltreNom.Text))
            {
                if (unClient.Nom == null || !unClient.Nom.StartsWith(txtFiltreNom.Text, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            if (!string.IsNullOrEmpty(txtFiltrePrenom.Text))
            {
                if (unClient.Prenom == null || !unClient.Prenom.StartsWith(txtFiltrePrenom.Text, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }

        private void FiltreClient_Changed(object sender, TextChangedEventArgs e)
        {
            if (dgClients.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(dgClients.ItemsSource).Refresh();
            }
        }

        private void btnSelectionner_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem is Personne clientChoisi)
            {
                ClientSelectionne = clientChoisi;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client dans la liste avant de valider.", "Sélection requise", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}