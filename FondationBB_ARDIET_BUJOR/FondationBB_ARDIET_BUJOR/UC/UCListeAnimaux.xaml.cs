using FondationBB_ARDIET_BUJOR.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class UCListeAnimaux : UserControl // <-- Attention, ça hérite de UserControl maintenant
    {
        public UCListeAnimaux()
        {
            InitializeComponent();
        }

        private void FiltreAnimal_Changed(object sender, RoutedEventArgs e)
        {
            if (dgAnimaux.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(dgAnimaux.ItemsSource).Refresh();
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e) { }
        private void BtnAjouter_Click(object sender, RoutedEventArgs e) { }
        private void BtnEditer_Click(object sender, RoutedEventArgs e) { }
        private bool RechercheMotClefAnimal_Animal(object obj)
        {
            if (String.IsNullOrEmpty(txtFiltreNom.Text))
                return true;
            Animal unAnimal = obj as Animal;
            return unAnimal.Nom.StartsWith(txtFiltreNom.Text, StringComparison.OrdinalIgnoreCase);
        }
        private bool RechercheMotClefAnimal_Espece(object obj)
        {
            if (String.IsNullOrEmpty(txtFiltreEspece.Text))
                return true;
            Animal unAnimal = obj as Animal;
            return unAnimal.UneRace.UneEspece.ToString().StartsWith(txtFiltreEspece.Text, StringComparison.OrdinalIgnoreCase);
        }
    }
}