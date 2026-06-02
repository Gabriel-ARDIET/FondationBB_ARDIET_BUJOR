using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowAjoutComportement.xaml
    /// </summary>
    public partial class WindowAjoutComportement : Window
    {
        // Propriété accessible depuis la fenêtre parente après la fermeture
        public string ComportementSelectionne { get; private set; }

        // Indicateur de validation
        private bool _donneesValidees = false;

        public WindowAjoutComportement()
        {
            InitializeComponent();
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            // Récupération du RadioButton coché
            RadioButton radioCoche = stackComportements.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);

            if (radioCoche == null)
            {
                MessageBox.Show("Veuillez sélectionner un comportement.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Sauvegarde du choix
            ComportementSelectionne = radioCoche.Content.ToString();

            _donneesValidees = true; // Empêche le prompt de confirmation à la fermeture
            this.DialogResult = true;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Déclenchera Window_Closing
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_donneesValidees) return;

            MessageBoxResult result = MessageBox.Show("Annuler la saisie ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true; // Bloque la fermeture de la fenêtre
            }
        }
    }
}