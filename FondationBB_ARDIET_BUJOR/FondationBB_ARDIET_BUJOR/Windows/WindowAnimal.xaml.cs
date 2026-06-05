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
        private Animal UnAnimal;
        private List<Recoit> Soins;
        private List<Animal_Comportement> Comportements;

        public WindowAnimal(Animal unAnimal, List<Recoit>soins, List<Animal_Comportement>comportements)
        {
            InitializeComponent();
            this.UnAnimal = unAnimal;
            this.Soins = soins;
            this.Comportements = comportements;
            this.DataContext = unAnimal;
            dgSoins.ItemsSource = soins;
            dgComportements.ItemsSource = comportements;

            comboEspece.ItemsSource = new Espece().FindAll();
            comboRace.ItemsSource = new Race().FindAll();
            cbStatut.ItemsSource = new Statut().FindAll();
            cbEtat.ItemsSource = new Etat().FindAll();

            if (((Animal)unAnimal).UnSexe.HasValue)
            {
                if (UnAnimal.UnSexe == Sexe.Male)
                    radioMale.IsChecked = true;
                else if (UnAnimal.UnSexe == Sexe.Femelle)
                    radioFemelle.IsChecked = true;
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            textIcad.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            textNom.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            comboEspece.GetBindingExpression(ComboBox.SelectedValueProperty)?.UpdateSource();
            comboRace.GetBindingExpression(ComboBox.SelectedValueProperty)?.UpdateSource();
            textPoids.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            dateNaissancePicker.GetBindingExpression(DatePicker.SelectedDateProperty)?.UpdateSource();
            dateArriveePicker.GetBindingExpression(DatePicker.SelectedDateProperty)?.UpdateSource();

            if (UnAnimal != null)
            {
                UnAnimal.UnStatut = cbStatut.SelectedItem as Statut;
                UnAnimal.UnEtat = cbEtat.SelectedItem as Etat;
                UnAnimal.UneRace = comboRace.SelectedItem as Race;

                if (MainWindow.EmployeConnecte != null)
                {
                    UnAnimal.IdCreateur = MainWindow.EmployeConnecte.Id;
                }
                if (comboEspece.SelectedItem != null)
                {
                    UnAnimal.UneRace.UneEspece = comboEspece.SelectedItem as Espece;
                }
            }

            var bIcad = textIcad.GetBindingExpression(TextBox.TextProperty);
            if (bIcad != null) Validation.ClearInvalid(bIcad);

            var bNom = textNom.GetBindingExpression(TextBox.TextProperty);
            if (bNom != null) Validation.ClearInvalid(bNom);

            var bEspece = comboEspece.GetBindingExpression(ComboBox.SelectedValueProperty);
            if (bEspece != null) Validation.ClearInvalid(bEspece);

            var bRace = comboRace.GetBindingExpression(ComboBox.SelectedValueProperty);
            if (bRace != null) Validation.ClearInvalid(bRace);

            var bNaissance = dateNaissancePicker.GetBindingExpression(DatePicker.SelectedDateProperty);
            if (bNaissance != null) Validation.ClearInvalid(bNaissance);

            var bPoids = textPoids.GetBindingExpression(TextBox.TextProperty);
            if (bPoids != null) Validation.ClearInvalid(bPoids);

            var bArrivee = dateArriveePicker.GetBindingExpression(DatePicker.SelectedDateProperty);
            if (bArrivee != null) Validation.ClearInvalid(bArrivee);

            var bStatut = cbStatut.GetBindingExpression(ComboBox.SelectedValueProperty);
            if (bStatut != null) Validation.ClearInvalid(bStatut);

            var bEtat = cbEtat.GetBindingExpression(ComboBox.SelectedValueProperty);
            if (bEtat != null) Validation.ClearInvalid(bEtat);

            borderSexe.Background = Brushes.Transparent;

            List<string> erreurs = new List<string>();
            var rule = new ExceptionValidationRule();
            string icadText = textIcad.Text?.Trim();
            if (string.IsNullOrEmpty(icadText) || icadText.Length != 15)
            {
                if (bIcad != null) Validation.MarkInvalid(bIcad, new ValidationError(rule, bIcad, "I-CAD invalide", null));
                erreurs.Add("- Le champ I-CAD doit faire exactement 15 caractères.");
            }

            if (string.IsNullOrEmpty(textNom.Text?.Trim()))
            {
                if (bNom != null) Validation.MarkInvalid(bNom, new ValidationError(rule, bNom, "Nom obligatoire", null));
                erreurs.Add("- Le nom de l'animal est obligatoire.");
            }

            if (comboEspece.SelectedValue == null)
            {
                if (bEspece != null) Validation.MarkInvalid(bEspece, new ValidationError(rule, bEspece, "Espèce obligatoire", null));
                erreurs.Add("- L'espèce est obligatoire.");
            }

            if (comboRace.SelectedValue == null)
            {
                if (bRace != null) Validation.MarkInvalid(bRace, new ValidationError(rule, bRace, "Race obligatoire", null));
                erreurs.Add("- La race de l'animal est obligatoire.");
            }

            if (radioMale.IsChecked != true && radioFemelle.IsChecked != true)
            {
                borderSexe.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCDD2"));
                erreurs.Add("- Le sexe de l'animal doit être sélectionné.");
            }

            if (dateNaissancePicker.SelectedDate == null)
            {
                if (bNaissance != null) Validation.MarkInvalid(bNaissance, new ValidationError(rule, bNaissance, "Date de naissance obligatoire", null));
                erreurs.Add("- La date de naissance est obligatoire.");
            }

            if (string.IsNullOrEmpty(textPoids.Text?.Trim()))
            {
                if (bPoids != null) Validation.MarkInvalid(bPoids, new ValidationError(rule, bPoids, "Poids obligatoire", null));
                erreurs.Add("- Le poids de l'animal est obligatoire.");
            }
            else if (!double.TryParse(textPoids.Text.Replace('.', ','), out _))
            {
                if (bPoids != null) Validation.MarkInvalid(bPoids, new ValidationError(rule, bPoids, "Format du poids incorrect", null));
                erreurs.Add("- Le poids doit être un nombre valide.");
            }

            if (dateArriveePicker.SelectedDate == null)
            {
                if (bArrivee != null) Validation.MarkInvalid(bArrivee, new ValidationError(rule, bArrivee, "Date d'arrivée obligatoire", null));
                erreurs.Add("- La date d'arrivée est obligatoire.");
            }

            if (cbStatut.SelectedItem == null)
            {
                if (bStatut != null) Validation.MarkInvalid(bStatut, new ValidationError(rule, bStatut, "Statut obligatoire", null));
                erreurs.Add("- Le statut de l'animal est obligatoire.");
            }

            if (cbEtat.SelectedItem == null)
            {
                if (bEtat != null) Validation.MarkInvalid(bEtat, new ValidationError(rule, bEtat, "État obligatoire", null));
                erreurs.Add("- L'état de santé de l'animal est obligatoire.");
            }
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
                if (UnAnimal != null)
                {
                    Recoit nouveauSoinRecu = new Recoit(fenetreSoins.SoinSelectionne, UnAnimal, fenetreSoins.DateSelectionnee);
                    Soins.Add(nouveauSoinRecu);
                    CollectionViewSource.GetDefaultView(dgSoins.ItemsSource).Refresh();
                }
            }
        }

        private void btnAjouterComportement_Click(object sender, RoutedEventArgs e)
        {
            WindowAjoutComportement fenetreComportement = new WindowAjoutComportement();
            fenetreComportement.Owner = this;

            if (fenetreComportement.ShowDialog() == true)
            {
                if (UnAnimal != null)
                {
                    Animal_Comportement nouveauComportement = new Animal_Comportement(fenetreComportement.ComportementSelectionne, UnAnimal);
                    Comportements.Add(nouveauComportement);
                    CollectionViewSource.GetDefaultView(dgComportements.ItemsSource).Refresh();
                }
            }
        }

        private void btnSupprimerSoin_Click(object sender, RoutedEventArgs e)
        {
            if (dgSoins.SelectedItem is Recoit soinSelectionne)
            {
                if (this.DataContext is Animal animalActuel)
                {
                    this.Soins.Remove(soinSelectionne);
                    CollectionViewSource.GetDefaultView(dgComportements.ItemsSource).Refresh();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un soin dans le tableau pour le supprimer.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnSupprimerComportement_Click(object sender, RoutedEventArgs e)
        {
            if (dgComportements.SelectedItem is Animal_Comportement comportementSelectionne)
            {
                if (this.DataContext is Animal animalActuel)
                {
                    this.Comportements.Remove(comportementSelectionne);
                    CollectionViewSource.GetDefaultView(dgComportements.ItemsSource).Refresh();
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

        private void comboEspece_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboEspece.SelectedItem == null) return;

            List<Race> racesPossibles = new List<Race>();
            foreach (Race r in new Race().FindAll())
                if (r.IdEspece == ((Espece)comboEspece.SelectedItem).Id)
                    racesPossibles.Add(r);
            comboRace.ItemsSource = racesPossibles;
        }
    }
}