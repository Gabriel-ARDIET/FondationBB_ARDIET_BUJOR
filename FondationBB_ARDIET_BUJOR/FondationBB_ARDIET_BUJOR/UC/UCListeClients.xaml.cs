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

        private void BtnSupprimerClient_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem is Personne clientSelectionne)
            {
                MessageBoxResult confirmation = MessageBox.Show(
                    $"Êtes-vous sûr de vouloir supprimer définitivement le client {clientSelectionne.Prenom} {clientSelectionne.Nom} ?",
                    "Confirmation de suppression",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (confirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        laData.LesPersonnes.Remove(clientSelectionne);

                        MessageBox.Show("Le client a été supprimé.", "Suppression réussie", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Impossible de supprimer ce client. Il est probablement lié à d'autres enregistrements.\n\nDétails : " + ex.Message,
                                        "Erreur de contrainte", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client à supprimer.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnAjouterClient_Click(object sender, RoutedEventArgs e)
        {
            Personne nouveauClient = new Personne();

            WindowClient fenetreClient = new WindowClient(nouveauClient);
            fenetreClient.Owner = Application.Current.MainWindow;

            if (fenetreClient.ShowDialog() == true)
            {
                try
                {
                    laData.LesPersonnes.Add(nouveauClient);

                    MessageBox.Show("Le client a été créé avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l'enregistrement en base de données :\n" + ex.Message, "Erreur SQL", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnEditerClient_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem is Personne clientSelectionne)
            {
                WindowClient fenetreClient = new WindowClient(clientSelectionne);
                fenetreClient.Owner = Application.Current.MainWindow;

                if (fenetreClient.ShowDialog() == true)
                {
                    try
                    {
                        CollectionViewSource.GetDefaultView(dgClients.ItemsSource).Refresh();

                        MessageBox.Show("La fiche client a été mise à jour.", "Modification enregistrée", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la mise à jour :\n" + ex.Message, "Erreur SQL", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    CollectionViewSource.GetDefaultView(dgClients.ItemsSource).Refresh();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client à modifier dans le tableau.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}