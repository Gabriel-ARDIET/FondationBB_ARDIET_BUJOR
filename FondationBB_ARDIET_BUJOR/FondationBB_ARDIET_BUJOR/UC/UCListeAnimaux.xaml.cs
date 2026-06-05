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
        private List<Recoit> soins;
        private List<Animal_Comportement> comportements;
        public UCListeAnimaux()
        {
            InitializeComponent();

            laData = (Data)Application.Current.MainWindow.DataContext;
            this.DataContext = laData;

            ICollectionView view = CollectionViewSource.GetDefaultView(laData.LesAnimaux);
            if (view != null)
            {
                view.Filter = FiltreCombine;
            }
            if (MainWindow.EmployeConnecte != null && MainWindow.EmployeConnecte.UnRole == Role.Bénévole)
            {
                btnAjouter.Visibility = Visibility.Collapsed;
            }
        }

        private bool FiltreCombine(object obj)
        {
            Animal unAnimal = obj as Animal;
            if (unAnimal == null) return false;

            if (!RechercheMotClefAnimal_Nom(unAnimal)) return false;

            if (!RechercheMotClefAnimal_Race(unAnimal)) return false;

            if (rbMale.IsChecked == true)
            {
                string sexeStr = unAnimal.UnSexe?.ToString() ?? "";
                if (!string.Equals(sexeStr, "Male", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(sexeStr, "Mâle", StringComparison.OrdinalIgnoreCase))
                    return false;
            }
            else if (rbFemelle.IsChecked == true)
            {
                string sexeStr = unAnimal.UnSexe?.ToString() ?? "";
                if (!string.Equals(sexeStr, "Femelle", StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            string statutAnimal = unAnimal.UnStatut?.Libelle ?? unAnimal.UnStatut?.ToString() ?? "";

            if (rbAdopte.IsChecked == true)
            {
                if (!string.Equals(statutAnimal, "Adopte", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(statutAnimal, "Adopté", StringComparison.OrdinalIgnoreCase))
                    return false;
            }
            else if (rbAuRefuge.IsChecked == true)
            {
                bool estAuRefuge = string.Equals(statutAnimal, "En soin", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(statutAnimal, "Disponible", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(statutAnimal, "Reserve", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(statutAnimal, "Réservé", StringComparison.OrdinalIgnoreCase);

                if (!estAuRefuge) return false;
            }

            return true;
        }

        private void FiltreAnimal_Changed(object sender, RoutedEventArgs e)
        {
            if (dgAnimaux.ItemsSource != null)
            {
                CollectionViewSource.GetDefaultView(dgAnimaux.ItemsSource).Refresh();
            }
        }

        private bool RechercheMotClefAnimal_Nom(Animal unAnimal)
        {
            if (string.IsNullOrEmpty(txtFiltreNom.Text))
                return true;

            if (unAnimal.Nom == null) return false;

            return unAnimal.Nom.StartsWith(txtFiltreNom.Text, StringComparison.OrdinalIgnoreCase);
        }

        private bool RechercheMotClefAnimal_Race(Animal unAnimal)
        {
            if (string.IsNullOrEmpty(txtFiltreEspece.Text))
                return true;
            if (unAnimal.UneRace == null) return false;
            string raceLibelle = unAnimal.UneRace.Libelle ?? unAnimal.UneRace.ToString() ?? "";
            string especeLibelle = unAnimal.UneRace.UneEspece?.Libelle ?? unAnimal.UneRace.UneEspece?.ToString() ?? "";

            return raceLibelle.StartsWith(txtFiltreEspece.Text, StringComparison.OrdinalIgnoreCase) ||
                   especeLibelle.StartsWith(txtFiltreEspece.Text, StringComparison.OrdinalIgnoreCase);
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Animal unAnimal = new Animal();
            this.soins = new List<Recoit>();
            this.comportements = new List<Animal_Comportement>();
            WindowAnimal wAnimal = new WindowAnimal(unAnimal, soins, comportements);
            bool? result = wAnimal.ShowDialog();

            if (result == true)
            {
                try
                {
                    unAnimal.IdStatut = unAnimal.UnStatut?.Id;
                    unAnimal.IdEtat = unAnimal.UnEtat?.Id;
                    unAnimal.Id = unAnimal.Create();
                    laData.LesAnimaux.Add(unAnimal);
                    foreach (Recoit r in soins)
                    {
                        r.IdAnimal = unAnimal.Id;
                        laData.LesSoinsReçus.Add(r);
                        r.Create();
                    }
                    foreach (Animal_Comportement c in comportements)
                    {
                        c.IdAnimal = unAnimal.Id;
                        laData.LesComportementsDesAnimaux.Add(c);
                        c.Create();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"L'animal n'a pas pu être créé.\nDétails : {ex.Message}", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnEditer_Click(object sender, RoutedEventArgs e)
        {
            if (dgAnimaux.SelectedItem is Animal animalSelectionne)
            {
                Animal copieAnimal = animalSelectionne.Copy();
                List<Recoit> copieSoins = this.soins.Select(s => s).ToList();
                List<Animal_Comportement> copieComportements = this.comportements.Select(c => c).ToList();

                WindowAnimal wAnimal = new WindowAnimal(copieAnimal, copieSoins, copieComportements);
                bool? result = wAnimal.ShowDialog();

                if (result == true)
                {
                    try
                    {
                        animalSelectionne.UpdateFrom(copieAnimal);
                        animalSelectionne.IdStatut = animalSelectionne.UnStatut?.Id;
                        animalSelectionne.IdEtat = animalSelectionne.UnEtat?.Id;
                        animalSelectionne.Update();
                        foreach (Recoit r in copieSoins)
                        {
                            if (soins.FirstOrDefault(s => s == r) == null)
                            {
                                laData.LesSoinsReçus.Add(r);
                                r.Create();
                            }
                        }
                        foreach (Animal_Comportement c in copieComportements)
                        {
                            if (comportements.FirstOrDefault(co => co == c) == null)
                            {
                                laData.LesComportementsDesAnimaux.Add(c);
                                c.Create();
                            }
                        }
                        dgAnimaux_SelectionChanged(null, null);
                        FiltreAnimal_Changed(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la modification : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un animal à modifier.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dgAnimaux.SelectedItem is Animal animalSelectionne)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Êtes-vous sûr de vouloir supprimer définitivement l'animal '{animalSelectionne.Nom}' (ICAD: {animalSelectionne.Icad}) ?",
                    "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        int lignesAffectees = animalSelectionne.Delete();
                        laData.SupprimerAnimal(animalSelectionne);
                        if (lignesAffectees > 0)
                        {
                            MessageBox.Show("L'animal a bien été supprimé.", "Suppression réussie", MessageBoxButton.OK, MessageBoxImage.Information);
                            laData.SupprimerAnimal(animalSelectionne);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Une erreur est survenue lors de la suppression : {ex.Message}", "Erreur critique", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un animal avant de cliquer sur supprimer.", "Sélection manquante", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dgAnimaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            soins = UpdateDataGridFiche(((Data)DataContext).LesSoinsReçus, dgSoins);
            comportements = UpdateDataGridFiche(((Data)DataContext).LesComportementsDesAnimaux, dgComportements);
        }
        private List<T> UpdateDataGridFiche<T>(ObservableCollection<T> values, DataGrid dg)
        {
            List<T> result = new List<T>();
            foreach (T v in values)
            {
                if ((Animal)dgAnimaux.SelectedItem != null)
                {
                    if (v is Recoit r && r.IdAnimal == ((Animal)dgAnimaux.SelectedItem).Id)
                        result.Add(v);
                    else if (v is Animal_Comportement c && c.IdAnimal == ((Animal)dgAnimaux.SelectedItem).Id)
                        result.Add(v);
                }
            }
            dg.ItemsSource = result;
            CollectionViewSource.GetDefaultView(dg.ItemsSource).Refresh();
            return result;
        }
    }
}