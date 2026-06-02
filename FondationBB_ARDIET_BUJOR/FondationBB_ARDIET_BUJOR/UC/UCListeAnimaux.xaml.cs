using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using FondationBB_ARDIET_BUJOR.Model;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class UCListeAnimaux : UserControl
    {
        // Déclaration de la liste observable globale qui stocke les animaux et alimente le DataGrid
        public ObservableCollection<Animal> ListeDesAnimaux { get; set; }

        public UCListeAnimaux()
        {
            InitializeComponent();

            // Initialisation de la collection et chargement des données
            ListeDesAnimaux = new ObservableCollection<Animal>();
            ChargerDonnees();

            // Rendre cette classe accessible au XAML pour le Binding ({Binding ListeDesAnimaux})
            this.DataContext = this;
        }

        /// <summary>
        /// Récupère les animaux depuis la BDD et configure les filtres de recherche
        /// </summary>
        private void ChargerDonnees()
        {
            ListeDesAnimaux.Clear();
            Animal outilAnimal = new Animal();

            // Appel de la méthode de base de données corrigée
            var liste = outilAnimal.FindAll();

            foreach (var animal in liste)
            {
                ListeDesAnimaux.Add(animal);
            }

            // Liaison de tes fonctions de recherche (Filtres) à la vue du DataGrid
            ICollectionView view = CollectionViewSource.GetDefaultView(ListeDesAnimaux);
            if (view != null)
            {
                view.Filter = new Predicate<object>(FiltreCombine);
            }
        }

        /// <summary>
        /// Combine tes deux filtres existants (Nom et Espèce) pour qu'ils fonctionnent ensemble
        /// </summary>
        private bool FiltreCombine(object obj)
        {
            return RechercheMotClefAnimal_Animal(obj) && RechercheMotClefAnimal_Espece(obj);
        }

        private void FiltreAnimal_Changed(object sender, RoutedEventArgs e)
        {
            // Rafraîchit l'affichage du DataGrid dès qu'une lettre est tapée ou changée
            if (dgAnimaux.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(dgAnimaux.ItemsSource).Refresh();
            }
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Animal unAnimal = new Animal();
            WindowAnimal wAnimal = new WindowAnimal(unAnimal);
            bool? result = wAnimal.ShowDialog();

            if (result == true)
            {
                try
                {
                    unAnimal.Id = unAnimal.Create();

                    // Rafraîchir instantanément l'interface graphique après l'ajout en BDD
                    ChargerDonnees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("L'animal n'a pas pu être créé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnEditer_Click(object sender, RoutedEventArgs e)
        {
            // Code pour l'édition à venir
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            // Code pour la suppression à venir
        }

        private bool RechercheMotClefAnimal_Animal(object obj)
        {
            if (String.IsNullOrEmpty(txtFiltreNom.Text))
                return true;

            Animal unAnimal = obj as Animal;
            // Sécurité anti-null au cas où le nom de l'animal soit vide en BDD
            if (unAnimal == null || unAnimal.Nom == null) return false;

            return unAnimal.Nom.StartsWith(txtFiltreNom.Text, StringComparison.OrdinalIgnoreCase);
        }

        private bool RechercheMotClefAnimal_Espece(object obj)
        {
            if (String.IsNullOrEmpty(txtFiltreEspece.Text))
                return true;

            Animal unAnimal = obj as Animal;
            // Sécurité pour éviter le crash (NullReferenceException) si UneRace ou UneEspece n'est pas instanciée
            if (unAnimal == null || unAnimal.UneRace == null || unAnimal.UneRace.UneEspece == null)
                return false;

            return unAnimal.UneRace.UneEspece.ToString().StartsWith(txtFiltreEspece.Text, StringComparison.OrdinalIgnoreCase);
        }
    }
}