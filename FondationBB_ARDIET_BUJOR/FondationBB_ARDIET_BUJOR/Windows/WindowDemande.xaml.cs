using FondationBB_ARDIET_BUJOR.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class WindowDemande : Window
    {
        private bool _donneesValidees = false;
        private Demande _demandeEnCours;
        private Personne _clientSelectionne;

        public WindowDemande(Demande uneDemande)
        {
            InitializeComponent();

            _demandeEnCours = uneDemande;

            if (_demandeEnCours.IdPersonne > 0)
            {
                _clientSelectionne = new Personne().FindAll().Find(p => p.Id == _demandeEnCours.IdPersonne);
                _demandeEnCours.UnePersonne = _clientSelectionne;
            }
            else
            {
                if (_demandeEnCours.UnePersonne != null && _demandeEnCours.UnePersonne.Id > 0)
                {
                    _clientSelectionne = _demandeEnCours.UnePersonne;
                }
                else
                {
                    _clientSelectionne = new Personne();
                    _demandeEnCours.UnePersonne = _clientSelectionne;
                }
            }

            if (_demandeEnCours.UneRace == null) _demandeEnCours.UneRace = new Race();

            this.DataContext = _demandeEnCours;

            comboRace.ItemsSource = new Race().FindAll();
            comboTrancheAge.ItemsSource = Enum.GetValues(typeof(TrancheAge));

            MettreAJourAffichageClient();
        }

        private void btnChoisirClient_Click(object sender, RoutedEventArgs e)
        {
            WindowAjoutClientAdoption fenetreSelection = new WindowAjoutClientAdoption();
            fenetreSelection.Owner = this;

            if (fenetreSelection.ShowDialog() == true)
            {
                _clientSelectionne = fenetreSelection.ClientSelectionne;
                if (_clientSelectionne != null)
                {
                    _demandeEnCours.UnePersonne = _clientSelectionne;
                    _demandeEnCours.IdPersonne = _clientSelectionne.Id;
                }
                MettreAJourAffichageClient();
            }
        }

        private void MettreAJourAffichageClient()
        {
            if (_demandeEnCours.UnePersonne != null && !string.IsNullOrEmpty(_demandeEnCours.UnePersonne.Nom))
            {
                textClient.Text = $"{_demandeEnCours.UnePersonne.Nom} {_demandeEnCours.UnePersonne.Prenom}";
            }
            else
            {
                textClient.Text = string.Empty;
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            comboRace.GetBindingExpression(ComboBox.SelectedValueProperty)?.UpdateSource();
            comboTrancheAge.GetBindingExpression(ComboBox.SelectedValueProperty)?.UpdateSource();

            var bClient = textClient.GetBindingExpression(TextBox.TextProperty); if (bClient != null) Validation.ClearInvalid(bClient);
            var bRace = comboRace.GetBindingExpression(ComboBox.SelectedValueProperty); if (bRace != null) Validation.ClearInvalid(bRace);
            var bTranche = comboTrancheAge.GetBindingExpression(ComboBox.SelectedValueProperty); if (bTranche != null) Validation.ClearInvalid(bTranche);

            List<string> erreurs = new List<string>();
            var rule = new ExceptionValidationRule();

            if (_demandeEnCours.UnePersonne == null || _demandeEnCours.UnePersonne.Id == 0)
            {
                if (bClient != null) Validation.MarkInvalid(bClient, new ValidationError(rule, bClient, "Client requis", null));
                erreurs.Add("- Vous devez associer un client demandeur valide.");
            }

            if (comboRace.SelectedValue == null)
            {
                if (bRace != null) Validation.MarkInvalid(bRace, new ValidationError(rule, bRace, "Race requise", null));
                erreurs.Add("- Veuillez sélectionner la race ciblée par la demande.");
            }

            if (comboTrancheAge.SelectedValue == null)
            {
                if (bTranche != null) Validation.MarkInvalid(bTranche, new ValidationError(rule, bTranche, "Tranche d'âge obligatoire", null));
                erreurs.Add("- Le choix d'une tranche d'âge est obligatoire.");
            }

            if (erreurs.Count == 0)
            {
                try
                {
                    if (_demandeEnCours.DateDemande == default(DateTime))
                    {
                        _demandeEnCours.DateDemande = DateTime.Today;
                    }

                    Race raceSelectionnee = (Race)comboRace.SelectedItem;
                    _demandeEnCours.UneRace = raceSelectionnee;
                    _demandeEnCours.IdRace = raceSelectionnee.Id;

                    _demandeEnCours.UneTrancheAge = (TrancheAge)comboTrancheAge.SelectedValue;
                    _demandeEnCours.IdPersonne = _demandeEnCours.UnePersonne.Id;

                    _donneesValidees = true;
                    DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la préparation des données : {ex.Message}", "Erreur technique", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                string messageComplet = "Impossible de valider le dossier. Erreurs rencontrées :\n\n" + string.Join("\n", erreurs);
                MessageBox.Show(messageComplet, "Erreurs de validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_donneesValidees) return;

            MessageBoxResult result = MessageBox.Show("Abandonner la saisie de cette demande d'adoption ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}