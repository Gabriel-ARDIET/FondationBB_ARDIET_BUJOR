using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class WindowAjoutEtat : Window
    {
        public string EtatSelectionne { get; private set; }
        private bool _donneesValidees = false;

        public WindowAjoutEtat()
        {
            InitializeComponent();
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioCoche = stackEtats.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);

            if (radioCoche == null)
            {
                MessageBox.Show("Veuillez sélectionner un état de santé.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            EtatSelectionne = radioCoche.Content.ToString();
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

            MessageBoxResult result = MessageBox.Show("Annuler la sélection ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}