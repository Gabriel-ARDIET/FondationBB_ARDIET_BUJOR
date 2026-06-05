using FondationBB_ARDIET_BUJOR.Model;
using System;
using System.Windows;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class WindowAjoutSoins : Window
    {
        public Soin SoinSelectionne { get; private set; }
        public DateTime DateSelectionnee { get; private set; }

        private bool _donneesValidees = false;

        public WindowAjoutSoins()
        {
            InitializeComponent();
            dpDateSoin.SelectedDate = DateTime.Now;

            try
            {
                cbSoins.ItemsSource = new Soin().FindAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des soins : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            Soin soinChoisi = cbSoins.SelectedItem as Soin;

            if (soinChoisi == null || dpDateSoin.SelectedDate == null)
            {
                MessageBox.Show("Veuillez sélectionner un soin et une date.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SoinSelectionne = soinChoisi;
            DateSelectionnee = dpDateSoin.SelectedDate.Value;

            _donneesValidees = true;
            this.DialogResult = true;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_donneesValidees) return;

            MessageBoxResult result = MessageBox.Show("Annuler la saisie ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}