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

        private void BtnAjouter_Click(object sender, RoutedEventArgs e) { }
        private void BtnEditer_Click(object sender, RoutedEventArgs e) { }
        private void BtnSupprimer_Click(object sender, RoutedEventArgs e) { }
    }
}