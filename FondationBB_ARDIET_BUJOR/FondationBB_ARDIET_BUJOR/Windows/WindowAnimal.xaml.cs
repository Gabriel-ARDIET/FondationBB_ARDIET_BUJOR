using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class WindowAnimal : Window
    {
        // Constructeur qui reçoit l'objet Animal en paramètre (comme ton exemple de référence)
        public WindowAnimal(object unAnimal) // Remplace 'object' par ton modèle (ex: Animal) si nécessaire
        {
            this.DataContext = unAnimal;
            InitializeComponent();
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            foreach (UIElement uie in panelFormAnimal.Children)
            {
                if (uie is TextBox txt)
                {
                    var binding = txt.GetBindingExpression(TextBox.TextProperty);
                    binding?.UpdateSource();
                }
                if (Validation.GetHasError(uie)) ok = false;
            }

            if (ok)
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Veuillez corriger les erreurs de saisie.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}