using FondationBB_ARDIET_BUJOR.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class WindowAnimal : Window
    {
        private bool _donneesValidees = false;

        public WindowAnimal(object unAnimal, List<Race> lesRacesDisponibles, List<Statut> lesStatutsDisponibles, List<Etat> lesEtatsDisponibles)
        {
            InitializeComponent();
            this.DataContext = unAnimal;

            // Liaison des collections aux ComboBox
            comboRace.ItemsSource = lesRacesDisponibles;
            comboEspece.ItemsSource = new Espece().FindAll();
            cbStatut.ItemsSource = lesStatutsDisponibles;  // NOUVEAU
            cbEtat.ItemsSource = lesEtatsDisponibles;      // NOUVEAU

            if (unAnimal is Animal animalActuel && animalActuel.UnSexe.HasValue)
            {
                if (animalActuel.UnSexe == Sexe.Male)
                    radioMale.IsChecked = true;
                else if (animalActuel.UnSexe == Sexe.Femelle)
                    radioFemelle.IsChecked = true;
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            // 1. Forcer la mise à jour des Bindings
            textIcad.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            textNom.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            comboEspece.GetBindingExpression(ComboBox.SelectedValueProperty)?.UpdateSource();
            comboRace.GetBindingExpression(ComboBox.SelectedValueProperty)?.UpdateSource();
            textPoids.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            dateNaissancePicker.GetBindingExpression(DatePicker.SelectedDateProperty)?.UpdateSource();
            dateArriveePicker.GetBindingExpression(DatePicker.SelectedDateProperty)?.UpdateSource();

            // Récupération de l'objet Animal en cours
            Animal animalActuel = this.DataContext as Animal;

            // Dans WindowAnimal.xaml.cs -> btnValider_Click

            if (animalActuel != null)
            {
                animalActuel.UnStatut = cbStatut.SelectedItem as Statut;
                animalActuel.UnEtat = cbEtat.SelectedItem as Etat;
                animalActuel.UneRace = comboRace.SelectedItem as Race;

                // CORRECTION : Utilisation de la vraie propriété "IdCreateur" de votre modèle Animal
                if (MainWindow.EmployeConnecte != null)
                {
                    animalActuel.IdCreateur = MainWindow.EmployeConnecte.Id;
                }
            }

            // 2. Réinitialisation visuelle des champs
            Brush couleurDefaut = Brushes.White;
            textIcad.Background = couleurDefaut;
            textNom.Background = couleurDefaut;
            comboRace.Background = couleurDefaut;
            borderSexe.Background = Brushes.Transparent;
            dateNaissancePicker.Background = Brushes.Transparent;
            textPoids.Background = couleurDefaut;
            dateArriveePicker.Background = Brushes.Transparent;
            cbStatut.Background = couleurDefaut;
            cbEtat.Background = couleurDefaut;

            // Listes pour stocker les messages d'erreurs
            System.Collections.Generic.List<string> erreurs = new List<string>();
            Brush couleurErreur = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCDD2")); // Rouge pastel pour le fond

            // 3. Vérifications des contraintes
            string icadText = textIcad.Text?.Trim();
            if (string.IsNullOrEmpty(icadText) || icadText.Length != 15)
            {
                textIcad.Background = couleurErreur;
                erreurs.Add("- Le champ I-CAD doit faire exactement 15 caractères.");
            }

            if (string.IsNullOrEmpty(textNom.Text?.Trim()))
            {
                textNom.Background = couleurErreur;
                erreurs.Add("- Le nom de l'animal est obligatoire.");
            }

            if (comboEspece.SelectedValue == null)
            {
                comboEspece.Background = couleurErreur;
                erreurs.Add("- L'espèce est obligatoire.");
            }

            if (comboRace.SelectedValue == null)
            {
                comboRace.Background = couleurErreur;
                erreurs.Add("- La race de l'animal est obligatoire.");
            }

            if (radioMale.IsChecked != true && radioFemelle.IsChecked != true)
            {
                borderSexe.Background = couleurErreur;
                erreurs.Add("- Le sexe de l'animal doit être sélectionné.");
            }

            if (dateNaissancePicker.SelectedDate == null)
            {
                dateNaissancePicker.Background = couleurErreur;
                erreurs.Add("- La date de naissance est obligatoire.");
            }

            if (string.IsNullOrEmpty(textPoids.Text?.Trim()))
            {
                textPoids.Background = couleurErreur;
                erreurs.Add("- Le poids de l'animal est obligatoire.");
            }
            else if (!double.TryParse(textPoids.Text.Replace('.', ','), out _))
            {
                textPoids.Background = couleurErreur;
                erreurs.Add("- Le poids doit être un nombre valide.");
            }

            if (dateArriveePicker.SelectedDate == null)
            {
                dateArriveePicker.Background = couleurErreur;
                erreurs.Add("- La date d'arrivée est obligatoire.");
            }

            // MODIFICATION : Validation basée sur la sélection du ComboBox Statut
            if (cbStatut.SelectedItem == null)
            {
                cbStatut.Background = couleurErreur;
                erreurs.Add("- Le statut de l'animal est obligatoire.");
            }

            // MODIFICATION : Validation basée sur la sélection du ComboBox État
            if (cbEtat.SelectedItem == null)
            {
                cbEtat.Background = couleurErreur;
                erreurs.Add("- L'état de santé de l'animal est obligatoire.");
            }

            // 4. Traitement du résultat de la validation
            if (erreurs.Count == 0)
            {
                _donneesValidees = true;
                DialogResult = true;
            }
            else
            {
                string messageComplet = "Impossible de valider le formulaire. Les erreurs suivantes ont été détectées :\n\n" + string.Join("\n", erreurs);
                MessageBox.Show(messageComplet, "Erreurs de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void btnAjouterSoin_Click(object sender, RoutedEventArgs e)
        {
            WindowAjoutSoins fenetreSoins = new WindowAjoutSoins();
            fenetreSoins.Owner = this;
            if (fenetreSoins.ShowDialog() == true)
            {
                Animal animalActuel = this.DataContext as Animal;
                if (animalActuel != null)
                {
                    Recoit nouveauSoinRecu = new Recoit();
                    nouveauSoinRecu.UnSoin = fenetreSoins.SoinSelectionne;
                    nouveauSoinRecu.DateSoin = fenetreSoins.DateSelectionnee;
                    animalActuel.SoinReçus.Add(nouveauSoinRecu);
                }
            }
        }

        private void btnAjouterComportement_Click(object sender, RoutedEventArgs e)
        {
            WindowAjoutComportement fenetreComportement = new WindowAjoutComportement();
            fenetreComportement.Owner = this;
            if (fenetreComportement.ShowDialog() == true)
            {
                Animal animalActuel = this.DataContext as Animal;
                if (animalActuel != null)
                {
                    Comportement nouveauComportement = new Comportement();
                    nouveauComportement.Libelle = fenetreComportement.ComportementSelectionne;
                    animalActuel.Comportements.Add(nouveauComportement);
                }
            }
        }

        private void btnSupprimerSoin_Click(object sender, RoutedEventArgs e)
        {
            if (dgSoins.SelectedItem is Recoit soinSelectionne)
            {
                if (this.DataContext is Animal animalActuel)
                {
                    animalActuel.SoinReçus.Remove(soinSelectionne);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un soin dans le tableau pour le supprimer.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnSupprimerComportement_Click(object sender, RoutedEventArgs e)
        {
            if (dgComportements.SelectedItem is Comportement comportementSelectionne)
            {
                if (this.DataContext is Animal animalActuel)
                {
                    animalActuel.Comportements.Remove(comportementSelectionne);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un comportement dans le tableau pour le supprimer.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is Animal animalActuel)
            {
                if (radioMale.IsChecked == true)
                {
                    animalActuel.UnSexe = Sexe.Male;
                }
                else if (radioFemelle.IsChecked == true)
                {
                    animalActuel.UnSexe = Sexe.Femelle;
                }
            }
        }
    }
}