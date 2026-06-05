using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using FondationBB_ARDIET_BUJOR.Model;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class UCListeDemandes : UserControl
    {
        public UCListeDemandes()
        {
            InitializeComponent();
            this.Loaded += UCListeDemandes_Loaded;
        }

        private void UCListeDemandes_Loaded(object sender, RoutedEventArgs e)
        {
            if (dgDemandes.ItemsSource != null)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(dgDemandes.ItemsSource);
                view.Filter = ModeleFiltreDemande;
            }
        }

        private bool ModeleFiltreDemande(object item)
        {
            if (!(item is Demande deman)) return true;

            bool nomOk = true;
            bool prenomOk = true;

            if (!string.IsNullOrWhiteSpace(txtFiltreNomClient.Text))
            {
                nomOk = deman.UnePersonne != null &&
                        deman.UnePersonne.Nom.Contains(txtFiltreNomClient.Text, StringComparison.OrdinalIgnoreCase);
            }

            if (!string.IsNullOrWhiteSpace(txtFiltrePrenomClient.Text))
            {
                prenomOk = deman.UnePersonne != null &&
                           deman.UnePersonne.Prenom.Contains(txtFiltrePrenomClient.Text, StringComparison.OrdinalIgnoreCase);
            }

            return nomOk && prenomOk;
        }

        private void FiltreDemande_Changed(object sender, RoutedEventArgs e)
        {
            if (dgDemandes.ItemsSource != null)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(dgDemandes.ItemsSource);

                if (view.Filter == null)
                {
                    view.Filter = ModeleFiltreDemande;
                }

                view.Refresh();
            }
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                var laData = (Data)Application.Current.MainWindow.DataContext;

                Demande nouvelleDemande = new Demande();

                WindowDemande fenetreSaisie = new WindowDemande(nouvelleDemande);
                fenetreSaisie.Owner = Application.Current.MainWindow;

                if (fenetreSaisie.ShowDialog() == true)
                {
                    nouvelleDemande.Create();
                    laData.LesDemandes.Add(nouvelleDemande);

                    MessageBox.Show("La demande d'adoption a été enregistrée avec succès.",
                                    "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la création de la demande : {ex.Message}",
                                "Erreur technique", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnEditer_Click(object sender, RoutedEventArgs e)
        {
            if (dgDemandes.SelectedItem is Demande demandeSelectionnee)
            {
                try
                {
                    WindowDemande fenetreEdition = new WindowDemande(demandeSelectionnee);
                    fenetreEdition.Owner = Application.Current.MainWindow;

                    if (fenetreEdition.ShowDialog() == true)
                    {
                        demandeSelectionnee.Update();

                        var laData = (Data)Application.Current.MainWindow.DataContext;
                        System.ComponentModel.ICollectionView view = CollectionViewSource.GetDefaultView(laData.LesDemandes);
                        view?.Refresh();

                        MessageBox.Show("Le dossier de demande a été mis à jour.",
                                        "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la modification de la demande : {ex.Message}",
                                    "Erreur technique", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une demande dans la liste avant de cliquer sur Modifier.",
                                "Sélection requise", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dgDemandes.SelectedItem is Demande demandeASupprimer)
            {
                string messageConfirmation = $"Êtes-vous sûr de vouloir supprimer définitivement la demande de " +
                                             $"{demandeASupprimer.UnePersonne.Nom} {demandeASupprimer.UnePersonne.Prenom} " +
                                             $"pour la race {demandeASupprimer.UneRace.Libelle} ?";

                MessageBoxResult result = MessageBox.Show(messageConfirmation,
                                                          "Confirmation de suppression",
                                                          MessageBoxButton.YesNo,
                                                          MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        demandeASupprimer.Delete();

                        var laData = (Data)Application.Current.MainWindow.DataContext;
                        laData.LesDemandes.Remove(demandeASupprimer);

                        MessageBox.Show("La demande a été supprimée avec succès.",
                                        "Suppression effectuée", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la suppression de la demande : {ex.Message}",
                                        "Erreur technique", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner la demande à supprimer dans la liste.",
                                "Sélection requise", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void BtnComparer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fonction non implémentée", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}