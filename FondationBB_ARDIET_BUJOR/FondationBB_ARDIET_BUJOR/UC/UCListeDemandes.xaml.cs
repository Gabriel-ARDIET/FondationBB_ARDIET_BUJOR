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
            // On attend que l'élément soit complètement chargé pour assigner le filtre
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

        // Logique de filtrage calquée sur WindowAdoption
        private bool ModeleFiltreDemande(object item)
        {
            // Sécurité si l'élément n'est pas conforme
            if (!(item is Demande deman)) return true;

            bool nomOk = true;
            bool prenomOk = true;

            // Filtrage sur le Nom du client
            if (!string.IsNullOrWhiteSpace(txtFiltreNomClient.Text))
            {
                nomOk = deman.UnePersonne != null &&
                        deman.UnePersonne.Nom.Contains(txtFiltreNomClient.Text, StringComparison.OrdinalIgnoreCase);
            }

            // Filtrage sur le Prénom du client
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

                // Si le filtre n'est pas encore lié suite à une réaffectation d'ItemsSource
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
                // 1. Récupération du contexte de données global
                var laData = (Data)Application.Current.MainWindow.DataContext;

                // 2. Instanciation d'une nouvelle demande vierge
                Demande nouvelleDemande = new Demande();

                // 3. Ouverture de la fenêtre graphique que vous avez fournie
                WindowDemande fenetreSaisie = new WindowDemande(nouvelleDemande);
                fenetreSaisie.Owner = Application.Current.MainWindow;

                // 4. Si la validation est un succès (DialogResult == true)
                if (fenetreSaisie.ShowDialog() == true)
                {
                    // Appel de la méthode de persistance de votre modèle (ex: Insert ou Save)
                    // selon l'architecture de vos classes métiers (comme FindAll, etc.)
                    nouvelleDemande.Create();

                    // Ajout dans la collection liée à l'IHM pour mise à jour instantanée
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
            // 1. Détection de la demande sélectionnée dans l'IHM (ex: dgDemandes)
            // Remplacer "dgDemandes" par le nom réel de votre DataGrid principal
            if (dgDemandes.SelectedItem is Demande demandeSelectionnee)
            {
                try
                {
                    // 2. Ouverture de la même fenêtre en lui passant la demande existante
                    WindowDemande fenetreEdition = new WindowDemande(demandeSelectionnee);
                    fenetreEdition.Owner = Application.Current.MainWindow;

                    if (fenetreEdition.ShowDialog() == true)
                    {
                        // 3. Validation réussie : Mise à jour en base de données
                        demandeSelectionnee.Update();

                        // 4. Rafraîchissement de la vue par défaut pour répercuter les changements visuels
                        var laData = (Data)Application.Current.MainWindow.DataContext;
                        System.ComponentModel.ICollectionView view = CollectionViewSource.GetDefaultView(laData.LesDemandes);
                        view?.Refresh();

                        MessageBox.Show("Le dossier de demande a été mis à jour.",
                                        "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        // Si l'utilisateur a annulé, il peut être nécessaire de recharger 
                        // l'état de l'objet depuis la mémoire pour annuler les modifications locales.
                        // ex: demandeSelectionnee.Reload();
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
            // 1. Détection de la demande à supprimer
            if (dgDemandes.SelectedItem is Demande demandeASupprimer)
            {
                // 2. Message de courtoisie et de sécurité personnalisé
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
                        // 3. Suppression dans la base de données via votre modèle
                        demandeASupprimer.Delete();

                        // 4. Retrait de la liste observable globale pour mise à jour de l'IHM
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
    }
}