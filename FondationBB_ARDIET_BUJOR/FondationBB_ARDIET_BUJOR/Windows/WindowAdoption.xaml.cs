using FondationBB_ARDIET_BUJOR.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class WindowAdoption : Window
    {
        private bool _donneesValidees = false;
        private Adoption _adoptionEnCours;

        public WindowAdoption(Adoption uneAdoption)
        {
            InitializeComponent();

            _adoptionEnCours = uneAdoption;

            if (_adoptionEnCours.Adoptant == null) _adoptionEnCours.Adoptant = new Personne();
            if (_adoptionEnCours.UnAnimal == null) _adoptionEnCours.UnAnimal = new Animal();

            this.DataContext = _adoptionEnCours;

            MettreAJourAffichageNoms();
        }

        private void btnChoisirClient_Click(object sender, RoutedEventArgs e)
        {
            WindowAjoutClientAdoption fenetreSelection = new WindowAjoutClientAdoption();
            fenetreSelection.Owner = this;

            if (fenetreSelection.ShowDialog() == true)
            {
                _adoptionEnCours.Adoptant = fenetreSelection.ClientSelectionne;
                MettreAJourAffichageNoms();
            }
        }

        private void btnChoisirAnimal_Click(object sender, RoutedEventArgs e)
        {
            WindowAjoutAnimalAdoption fenetreChoix = new WindowAjoutAnimalAdoption();
            fenetreChoix.Owner = this;

            if (fenetreChoix.ShowDialog() == true)
            {
                _adoptionEnCours.UnAnimal = fenetreChoix.AnimalSelectionne;
                MettreAJourAffichageNoms();
            }
        }

        private void MettreAJourAffichageNoms()
        {
            textClient.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            textAnimal.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            datePickerAdoption.GetBindingExpression(DatePicker.SelectedDateProperty)?.UpdateTarget();
            textMontant.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();

            if (!string.IsNullOrEmpty(_adoptionEnCours.Adoptant?.Nom))
            {
                textClient.Text = $"{_adoptionEnCours.Adoptant.Nom} {_adoptionEnCours.Adoptant.Prenom}";
            }

            if (!string.IsNullOrEmpty(_adoptionEnCours.UnAnimal?.Nom))
            {
                textAnimal.Text = $"{_adoptionEnCours.UnAnimal.Nom}";
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            textClient.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            textAnimal.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            datePickerAdoption.GetBindingExpression(DatePicker.SelectedDateProperty)?.UpdateSource();
            textMontant.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

            var bClient = textClient.GetBindingExpression(TextBox.TextProperty); if (bClient != null) Validation.ClearInvalid(bClient);
            var bAnimal = textAnimal.GetBindingExpression(TextBox.TextProperty); if (bAnimal != null) Validation.ClearInvalid(bAnimal);
            var bDateAdoption = datePickerAdoption.GetBindingExpression(DatePicker.SelectedDateProperty); if (bDateAdoption != null) Validation.ClearInvalid(bDateAdoption);
            var bMontant = textMontant.GetBindingExpression(TextBox.TextProperty); if (bMontant != null) Validation.ClearInvalid(bMontant);

            List<string> erreurs = new List<string>();
            var rule = new ExceptionValidationRule();

            if (_adoptionEnCours.Adoptant == null || string.IsNullOrEmpty(_adoptionEnCours.Adoptant.Nom) || _adoptionEnCours.Adoptant.Id == 0)
            {
                if (bClient != null) Validation.MarkInvalid(bClient, new ValidationError(rule, bClient, "Client requis", null));
                erreurs.Add("- Vous devez choisir un client adoptant valide.");
            }

            if (_adoptionEnCours.UnAnimal == null || string.IsNullOrEmpty(_adoptionEnCours.UnAnimal.Nom) || _adoptionEnCours.UnAnimal.Id == 0)
            {
                if (bAnimal != null) Validation.MarkInvalid(bAnimal, new ValidationError(rule, bAnimal, "Animal requis", null));
                erreurs.Add("- Vous devez choisir un animal pour cette adoption.");
            }

            if (!datePickerAdoption.SelectedDate.HasValue)
            {
                if (bDateAdoption != null) Validation.MarkInvalid(bDateAdoption, new ValidationError(rule, bDateAdoption, "Date obligatoire", null));
                erreurs.Add("- La date d'adoption est obligatoire.");
            }

            decimal montant = 0;
            if (string.IsNullOrEmpty(textMontant.Text?.Trim()))
            {
                if (bMontant != null) Validation.MarkInvalid(bMontant, new ValidationError(rule, bMontant, "Montant obligatoire", null));
                erreurs.Add("- Le montant des frais est obligatoire.");
            }
            else if (!decimal.TryParse(textMontant.Text, out montant) || montant < 0)
            {
                if (bMontant != null) Validation.MarkInvalid(bMontant, new ValidationError(rule, bMontant, "Montant invalide", null));
                erreurs.Add("- Le montant des frais doit être un nombre numérique positif.");
            }

            if (erreurs.Count == 0)
            {
                try
                {
                    int idClient = _adoptionEnCours.Adoptant.Id;
                    int idAnimal = _adoptionEnCours.UnAnimal.Id;

                    int idEmploye = 1;

                    _adoptionEnCours.IdAdoptant = idClient;
                    _adoptionEnCours.IdAnimal = idAnimal;
                    _adoptionEnCours.IdCreateur = idEmploye;

                    _adoptionEnCours.Frais = montant;
                    _adoptionEnCours.DateAdoption = datePickerAdoption.SelectedDate.Value;

                    _donneesValidees = true;
                    DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la préparation des données d'adoption : {ex.Message}", "Erreur technique", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                string messageComplet = "Impossible de valider le contrat d'adoption. Erreurs détectées :\n\n" + string.Join("\n", erreurs);
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
            MessageBoxResult result = MessageBox.Show("Annuler la saisie et perdre les modifications ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}