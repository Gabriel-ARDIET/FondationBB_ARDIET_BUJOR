using FondationBB_ARDIET_BUJOR.Model;
using System;
using System.Windows;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class WindowAjoutComportement : Window
    {
        public Comportement ComportementSelectionne { get; private set; }

        private bool _donneesValidees = false;

        public WindowAjoutComportement()
        {
            InitializeComponent();

            try
            {
                cbComportements.ItemsSource = new Comportement().FindAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des comportements : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            Comportement comportementChoisi = cbComportements.SelectedItem as Comportement;

            if (comportementChoisi == null)
            {
                MessageBox.Show("Veuillez sélectionner un comportement.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ComportementSelectionne = comportementChoisi;

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