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
        private Data laData;

        public UCListeAnimaux()
        {
            InitializeComponent();

            laData = (Data)Application.Current.MainWindow.DataContext;
            this.DataContext = laData.LesAnimaux;
        }

        /// <summary>
        /// Combine tes deux filtres existants (Nom et Espèce) pour qu'ils fonctionnent ensemble
        /// </summary>
        private bool FiltreCombine(object obj)
        {
            // 1. Vérification des filtres textuels (Nom, Espèce)
            bool matchTextes = RechercheMotClefAnimal_Animal(obj) && RechercheMotClefAnimal_Espece(obj);
            if (!matchTextes) return false;

            Animal unAnimal = obj as Animal;
            if (unAnimal == null) return false;

            // --- LOGIQUE FILTRE SEXE ---
            bool matchSexe = true;
            if (rbMale.IsChecked == true)
            {
                matchSexe = unAnimal.UnSexe != null && string.Equals(unAnimal.UnSexe.ToString(), "Male", StringComparison.OrdinalIgnoreCase);
            }
            else if (rbFemelle.IsChecked == true)
            {
                matchSexe = unAnimal.UnSexe != null && string.Equals(unAnimal.UnSexe.ToString(), "Femelle", StringComparison.OrdinalIgnoreCase);
            }

            // --- LOGIQUE FILTRE STATUT (CORRIGÉE) ---
            bool matchStatut = true;

            // On récupère le texte du statut actuel de l'animal (ex: "En soin", "Adopté"...)
            // Remplace '.ToString()' par '.Libelle' ou '.Nom' si UnStatut est un objet complexe
            string statutAnimal = unAnimal.UnStatut != null ? unAnimal.UnStatut.ToString() : "";

            if (rbAdopte.IsChecked == true)
            {
                // L'animal doit avoir explicitement le statut "Adopté"
                matchStatut = string.Equals(statutAnimal, "Adopte", StringComparison.OrdinalIgnoreCase);
            }
            else if (rbAuRefuge.IsChecked == true)
            {
                // L'animal est considéré "Au refuge" s'il a l'un de ces statuts :
                matchStatut = string.Equals(statutAnimal, "En soin", StringComparison.OrdinalIgnoreCase) ||
                              string.Equals(statutAnimal, "Disponible", StringComparison.OrdinalIgnoreCase) ||
                              string.Equals(statutAnimal, "Reserve", StringComparison.OrdinalIgnoreCase) ||
                              string.Equals(statutAnimal, "Decede", StringComparison.OrdinalIgnoreCase);
                // Note : Enlève "Décédé" de la liste ci-dessus si tu ne souhaites pas l'inclure dans les animaux "au refuge"
            }

            // L'animal doit valider les filtres de Sexe ET de Statut
            return matchSexe && matchStatut;
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

            // 2. Passage de l'animal et des 3 listes au constructeur de la fenêtre
            WindowAnimal wAnimal = new WindowAnimal(unAnimal);

            bool? result = wAnimal.ShowDialog();

            if (result == true)
            {
                try
                {
                    unAnimal.Id = unAnimal.Create();
                    ((ObservableCollection<Animal>)this.DataContext).Add(unAnimal);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"L'animal n'a pas pu être créé.\nDétails : {ex.Message}", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnEditer_Click(object sender, RoutedEventArgs e)
        {
            // Code pour l'édition à venir
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            // 1. On récupère l'animal sélectionné dans le DataGrid
            if (dgAnimaux.SelectedItem is Animal animalSelectionne)
            {
                // 2. Fenêtre de confirmation pour éviter les fausses manipulations
                MessageBoxResult result = MessageBox.Show(
                    $"Êtes-vous sûr de vouloir supprimer définitivement l'animal '{animalSelectionne.Nom}' (ICAD: {animalSelectionne.Icad}) ?",
                    "Confirmation de suppression",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // 3. Appel de la méthode Delete du modèle
                        int lignesAffectees = animalSelectionne.Delete();

                        if (lignesAffectees > 0)
                        {
                            MessageBox.Show("L'animal a bien été supprimé de la base de données.", "Suppression réussie", MessageBoxButton.OK, MessageBoxImage.Information);

                            // 4. Mise à jour de l'interface graphique
                            if (dgAnimaux.ItemsSource is System.Collections.ObjectModel.ObservableCollection<Animal> listeObservable)
                            {
                                // Si votre DataGrid est lié à une ObservableCollection, le retirer suffit à rafraîchir l'écran
                                listeObservable.Remove(animalSelectionne);
                            }
                            else
                            {
                                // Si c'est une List<Animal> classique, on recharge simplement le tableau depuis la base de données
                                dgAnimaux.ItemsSource = new Animal().FindAll();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Aucune ligne n'a été modifiée en base de données. L'animal a peut-être déjà été supprimé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Gestion des erreurs (perte de connexion, contrainte SQL imprévue...)
                        MessageBox.Show($"Une erreur est survenue lors de la suppression : {ex.Message}", "Erreur critique", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                // Si l'utilisateur clique sur le bouton sans avoir sélectionné de ligne
                MessageBox.Show("Veuillez sélectionner un animal dans le tableau avant de cliquer sur supprimer.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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