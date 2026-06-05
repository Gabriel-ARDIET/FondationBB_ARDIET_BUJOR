using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FondationBB_ARDIET_BUJOR.Model;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class WindowClient : Window
    {
        private bool _donneesValidees = false;

        public WindowClient(Personne unePersonne)
        {
            InitializeComponent();
            this.DataContext = unePersonne;
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            textNom.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            textPrenom.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

            datePickerNaissance.GetBindingExpression(DatePicker.SelectedDateProperty)?.UpdateSource();

            textNumero.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            textRue.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            textCp.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            textVille.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            textTelephone.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            textMail.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

            var bNom = textNom.GetBindingExpression(TextBox.TextProperty); if (bNom != null) Validation.ClearInvalid(bNom);
            var bPrenom = textPrenom.GetBindingExpression(TextBox.TextProperty); if (bPrenom != null) Validation.ClearInvalid(bPrenom);

            var bDateNais = datePickerNaissance.GetBindingExpression(DatePicker.SelectedDateProperty); if (bDateNais != null) Validation.ClearInvalid(bDateNais);

            var bNumero = textNumero.GetBindingExpression(TextBox.TextProperty); if (bNumero != null) Validation.ClearInvalid(bNumero);
            var bRue = textRue.GetBindingExpression(TextBox.TextProperty); if (bRue != null) Validation.ClearInvalid(bRue);
            var bCp = textCp.GetBindingExpression(TextBox.TextProperty); if (bCp != null) Validation.ClearInvalid(bCp);
            var bVille = textVille.GetBindingExpression(TextBox.TextProperty); if (bVille != null) Validation.ClearInvalid(bVille);
            var bTelephone = textTelephone.GetBindingExpression(TextBox.TextProperty); if (bTelephone != null) Validation.ClearInvalid(bTelephone);
            var bMail = textMail.GetBindingExpression(TextBox.TextProperty); if (bMail != null) Validation.ClearInvalid(bMail);

            List<string> erreurs = new List<string>();
            var rule = new ExceptionValidationRule();

            if (string.IsNullOrEmpty(textNom.Text?.Trim()))
            {
                if (bNom != null) Validation.MarkInvalid(bNom, new ValidationError(rule, bNom, "Nom obligatoire", null));
                erreurs.Add("- Le nom est obligatoire.");
            }
            else if (textNom.Text.Length > 100)
            {
                if (bNom != null) Validation.MarkInvalid(bNom, new ValidationError(rule, bNom, "Trop long", null));
                erreurs.Add("- Le nom doit faire moins de 100 caractères.");
            }

            if (string.IsNullOrEmpty(textPrenom.Text?.Trim()))
            {
                if (bPrenom != null) Validation.MarkInvalid(bPrenom, new ValidationError(rule, bPrenom, "Prénom obligatoire", null));
                erreurs.Add("- Le prénom est obligatoire.");
            }
            else if (textPrenom.Text.Length > 100)
            {
                if (bPrenom != null) Validation.MarkInvalid(bPrenom, new ValidationError(rule, bPrenom, "Trop long", null));
                erreurs.Add("- Le prénom doit faire moins de 100 caractères.");
            }

            if (datePickerNaissance.SelectedDate.HasValue && datePickerNaissance.SelectedDate.Value > DateTime.Today)
            {
                if (bDateNais != null) Validation.MarkInvalid(bDateNais, new ValidationError(rule, bDateNais, "Date invalide", null));
                erreurs.Add("- La date de naissance ne peut pas être dans le futur.");
            }

            if (string.IsNullOrEmpty(textNumero.Text?.Trim()) || textNumero.Text.Length > 10)
            {
                if (bNumero != null) Validation.MarkInvalid(bNumero, new ValidationError(rule, bNumero, "N° invalide", null));
                erreurs.Add("- Le numéro de rue est obligatoire et doit faire moins de 10 caractères.");
            }

            if (string.IsNullOrEmpty(textRue.Text?.Trim()) || textRue.Text.Length > 100)
            {
                if (bRue != null) Validation.MarkInvalid(bRue, new ValidationError(rule, bRue, "Rue invalide", null));
                erreurs.Add("- Le nom de la rue est obligatoire et doit faire moins de 100 caractères.");
            }

            if (string.IsNullOrEmpty(textCp.Text?.Trim()) || textCp.Text.Length > 10)
            {
                if (bCp != null) Validation.MarkInvalid(bCp, new ValidationError(rule, bCp, "CP invalide", null));
                erreurs.Add("- Le code postal est obligatoire et doit faire moins de 10 caractères.");
            }

            if (string.IsNullOrEmpty(textVille.Text?.Trim()) || textVille.Text.Length > 50)
            {
                if (bVille != null) Validation.MarkInvalid(bVille, new ValidationError(rule, bVille, "Ville invalide", null));
                erreurs.Add("- La ville est obligatoire et doit faire moins de 50 caractères.");
            }

            string tel = textTelephone.Text?.Trim();
            if (string.IsNullOrEmpty(tel) || tel.Length != 10)
            {
                if (bTelephone != null) Validation.MarkInvalid(bTelephone, new ValidationError(rule, bTelephone, "Téléphone invalide", null));
                erreurs.Add("- Le numéro de téléphone doit faire exactement 10 caractères.");
            }

            if (!string.IsNullOrEmpty(textMail.Text) && textMail.Text.Length > 100)
            {
                if (bMail != null) Validation.MarkInvalid(bMail, new ValidationError(rule, bMail, "Mail trop long", null));
                erreurs.Add("- L'adresse e-mail doit faire moins de 100 caractères.");
            }

            if (erreurs.Count == 0)
            {
                _donneesValidees = true;
                DialogResult = true;
            }
            else
            {
                string messageComplet = "Impossible de valider la fiche client. Erreurs détectées :\n\n" + string.Join("\n", erreurs);
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