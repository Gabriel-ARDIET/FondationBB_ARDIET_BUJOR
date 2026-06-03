using FondationBB_ARDIET_BUJOR.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class WindowAnimal : Window
    {
        private bool _donneesValidees = false;

        public WindowAnimal(object unAnimal, System.Collections.Generic.List<Race> lesRacesDisponibles)
        {
            InitializeComponent();
            this.DataContext = unAnimal;

            // On lie la liste des races au ComboBox
            comboRace.ItemsSource = lesRacesDisponibles;
            comboEspece.ItemsSource = Espece.FindAll();

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
            // 1. Forcer la mise à jour des Bindings pour être sûr d'avoir les dernières valeurs saisies
            textIcad.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            textNom.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            comboEspece.GetBindingExpression(ComboBox.SelectedValueProperty)?.UpdateSource();
            comboRace.GetBindingExpression(ComboBox.SelectedValueProperty)?.UpdateSource();
            textPoids.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            dateNaissancePicker.GetBindingExpression(DatePicker.SelectedDateProperty)?.UpdateSource();
            dateArriveePicker.GetBindingExpression(DatePicker.SelectedDateProperty)?.UpdateSource();

            // Récupération de l'objet Animal en cours
            Animal animalActuel = this.DataContext as Animal;

            // 2. Réinitialisation visuelle des champs (remise à blanc / transparent)
            Brush couleurDefaut = Brushes.White;
            textIcad.Background = couleurDefaut;
            textNom.Background = couleurDefaut;
            comboRace.Background = couleurDefaut;
            borderSexe.Background = Brushes.Transparent;
            dateNaissancePicker.Background = Brushes.Transparent;
            textPoids.Background = couleurDefaut;
            dateArriveePicker.Background = Brushes.Transparent;
            textStatut.Background = couleurDefaut;
            textSante.Background = couleurDefaut;

            // Listes pour stocker les messages d'erreurs
            System.Collections.Generic.List<string> erreurs = new System.Collections.Generic.List<string>();
            Brush couleurErreur = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCDD2")); // Rouge pastel pour le fond

            // 3. Vérifications des contraintes

            // I-CAD : exactitude à 15 caractères
            string icadText = textIcad.Text?.Trim();
            if (string.IsNullOrEmpty(icadText) || icadText.Length != 15)
            {
                textIcad.Background = couleurErreur;
                erreurs.Add("- Le champ I-CAD doit faire exactement 15 caractères.");
            }

            // Nom obligatoire
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

            // Race obligatoire
            if (comboRace.SelectedValue == null)
            {
                comboRace.Background = couleurErreur;
                erreurs.Add("- La race de l'animal est obligatoire et doit être sélectionnée dans la liste.");
            }

            // Sexe obligatoire
            if (radioMale.IsChecked != true && radioFemelle.IsChecked != true)
            {
                borderSexe.Background = couleurErreur;
                erreurs.Add("- Le sexe de l'animal doit être sélectionné.");
            }

            // Date de naissance obligatoire
            if (dateNaissancePicker.SelectedDate == null)
            {
                dateNaissancePicker.Background = couleurErreur;
                erreurs.Add("- La date de naissance est obligatoire.");
            }

            // Poids obligatoire (et doit être un nombre valide)
            if (string.IsNullOrEmpty(textPoids.Text?.Trim()))
            {
                textPoids.Background = couleurErreur;
                erreurs.Add("- Le poids de l'animal est obligatoire.");
            }
            else if (!double.TryParse(textPoids.Text.Replace('.', ','), out _)) // Vérification basique du format numérique
            {
                textPoids.Background = couleurErreur;
                erreurs.Add("- Le poids doit être un nombre valide.");
            }

            // Date d'arrivée obligatoire
            if (dateArriveePicker.SelectedDate == null)
            {
                dateArriveePicker.Background = couleurErreur;
                erreurs.Add("- La date d'arrivée est obligatoire.");
            }

            // Statut obligatoire
            if (animalActuel?.UnStatut == null || string.IsNullOrEmpty(animalActuel.UnStatut.Libelle))
            {
                textStatut.Background = couleurErreur;
                erreurs.Add("- Le statut de l'animal est obligatoire.");
            }

            // État de santé obligatoire
            if (animalActuel?.UnEtat == null || string.IsNullOrEmpty(animalActuel.UnEtat.Libelle))
            {
                textSante.Background = couleurErreur;
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
                // Construction du message d'erreur listant les éléments manquants
                string messageComplet = "Impossible de valider le formulaire. Les erreurs suivantes ont été détectées :\n\n" + string.Join("\n", erreurs);
                MessageBox.Show(messageComplet, "Erreurs de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {

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
        private void btnEditerStatut_Click(object sender, RoutedEventArgs e)
        {
            WindowAjoutSatut fenetreStatut = new WindowAjoutSatut();
            fenetreStatut.Owner = this;

            if (fenetreStatut.ShowDialog() == true)
            {
                Animal animalActuel = this.DataContext as Animal;
                if (animalActuel != null)
                {
                    if (animalActuel.UnStatut == null)
                    {
                        animalActuel.UnStatut = new Statut();
                    }
                    animalActuel.UnStatut.Libelle = fenetreStatut.StatutSelectionne;
                    textStatut.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
                }
            }
        }

        private void btnEditerEtat_Click(object sender, RoutedEventArgs e)
        {
            WindowAjoutEtat fenetreEtat = new WindowAjoutEtat();
            fenetreEtat.Owner = this;

            if (fenetreEtat.ShowDialog() == true)
            {
                Animal animalActuel = this.DataContext as Animal;
                if (animalActuel != null)
                {
                    if (animalActuel.UnEtat == null)
                    {
                        animalActuel.UnEtat = new Etat();
                    }
                    animalActuel.UnEtat.Libelle = fenetreEtat.EtatSelectionne;
                    textSante.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
                }
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // On récupère l'animal en cours d'édition
            if (this.DataContext is Animal animalActuel)
            {
                // Si c'est le bouton mâle qui est coché
                if (radioMale.IsChecked == true)
                {
                    animalActuel.UnSexe = Sexe.Male;
                }
                // Sinon si c'est le bouton femelle
                else if (radioFemelle.IsChecked == true)
                {
                    animalActuel.UnSexe = Sexe.Femelle;
                }
            }
        }
    }
}
