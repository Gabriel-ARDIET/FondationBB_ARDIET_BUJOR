using FondationBB_ARDIET_BUJOR.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class WindowAjoutSoins : Window
    {
        public Soin SoinSelectionne { get; private set; }
        public DateTime DateSelectionnee { get; private set; }

        // Indicateur de validation
        private bool _donneesValidees = false;

        public WindowAjoutSoins()
        {
            InitializeComponent();
            dpDateSoin.SelectedDate = DateTime.Now;
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioCoche = stackSoins.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);

            if (radioCoche == null || dpDateSoin.SelectedDate == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SoinSelectionne = new Soin { Libelle = radioCoche.Content.ToString() };
            DateSelectionnee = dpDateSoin.SelectedDate.Value;

            _donneesValidees = true; // /!\ IMPORTANT : On signale que c'est enregistré
            this.DialogResult = true;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Déclenchera automatiquement l'événement Window_Closing
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Si validé, on ferme sans demander
            if (_donneesValidees) return;

            // Déclenché par la croix ou le bouton Annuler
            MessageBoxResult result = MessageBox.Show("Annuler la saisie ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true; // Bloque la fermeture
            }
        }
    }
}