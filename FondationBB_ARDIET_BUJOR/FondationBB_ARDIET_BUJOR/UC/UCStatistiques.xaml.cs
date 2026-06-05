using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FondationBB_ARDIET_BUJOR.Model;
using Npgsql;

namespace FondationBB_ARDIET_BUJOR.Windows
{
    public partial class UCStatistiques : UserControl
    {
        public class DataPoint
        {
            public string MoisNom { get; set; }
            public double YPosition { get; set; } 
            public string ValeurText { get; set; } 
        }

        private readonly string[] moisLabels = { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre" };

        public UCStatistiques()
        {
            InitializeComponent();
            ChargerCompteursGlobaux();
            RemplirSelecteurAnnee();
        }

        private void ChargerCompteursGlobaux()
        {
            try
            {
                string sqlChiens = @"
            SELECT COUNT(*) 
            FROM animal a 
            INNER JOIN race r ON a.id_race = r.id_race 
            INNER JOIN espece e ON r.id_espece = e.id_espece 
            WHERE e.libelle_espece ILIKE '%chien%';";

                using (NpgsqlCommand cmdChiens = new NpgsqlCommand(sqlChiens))
                {
                    TxtNbChiens.Text = DataAccess.ExecuteSelectOneValue(cmdChiens);
                }
                string sqlChats = @"
            SELECT COUNT(*) 
            FROM animal a 
            INNER JOIN race r ON a.id_race = r.id_race 
            INNER JOIN espece e ON r.id_espece = e.id_espece 
            WHERE e.libelle_espece ILIKE '%chat%';";

                using (NpgsqlCommand cmdChats = new NpgsqlCommand(sqlChats))
                {
                    TxtNbChats.Text = DataAccess.ExecuteSelectOneValue(cmdChats);
                }
                string sqlDemandes = "SELECT COUNT(*) FROM demande;";

                using (NpgsqlCommand cmdDemandes = new NpgsqlCommand(sqlDemandes))
                {
                    TxtDemandesAttente.Text = DataAccess.ExecuteSelectOneValue(cmdDemandes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des compteurs globaux : " + ex.Message);
            }
        }

        private void RemplirSelecteurAnnee()
        {
            List<int> annees = new List<int>();
            int anneeActuelle = DateTime.Now.Year;
            for (int i = anneeActuelle; i >= 2020; i--)
            {
                annees.Add(i);
            }
            ComboAnnee.ItemsSource = annees;
            ComboAnnee.SelectedIndex = 0;
        }

        private void ComboAnnee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboAnnee.SelectedItem is int anneeSelectionnee)
            {
                MettreAJourGraphiqueAdoptions(anneeSelectionnee);
                MettreAJourGraphiqueArrivees(anneeSelectionnee);
            }
        }

        private void MettreAJourGraphiqueAdoptions(int annee)
        {
            int[] valeursMensuelles = new int[12];

            string sql = "SELECT EXTRACT(MONTH FROM date_adoption) as mois, COUNT(*) as total " +
                         "FROM adoption " +
                         "WHERE EXTRACT(YEAR FROM date_adoption) = @annee " +
                         "GROUP BY mois;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@annee", annee);
                DataTable dt = DataAccess.ExecuteSelect(cmd);

                foreach (DataRow dr in dt.Rows)
                {
                    int mois = Convert.ToInt32(dr["mois"]);
                    int total = Convert.ToInt32(dr["total"]);
                    valeursMensuelles[mois - 1] = total;
                }
            }

            List<DataPoint> points = new List<DataPoint>();
            PointCollection polylinePoints = new PointCollection();

            double xPas = 70.0;
            double xDepart = 75.0;

            for (int i = 0; i < 12; i++)
            {
                int valeur = valeursMensuelles[i];
                double yPos = 220.0 - (valeur * (200.0 / 30.0));
                if (yPos < 20) yPos = 20;

                points.Add(new DataPoint
                {
                    MoisNom = moisLabels[i],
                    YPosition = yPos - 5,
                    ValeurText = $"{valeur} adoption(s) en {moisLabels[i]}"
                });

                polylinePoints.Add(new Point(xDepart + (i * xPas), yPos));
            }

            ItemsAdoptions.ItemsSource = points;
            PolylineAdoptions.Points = polylinePoints;
        }

        private void MettreAJourGraphiqueArrivees(int annee)
        {
            int[] valeursMensuelles = new int[12];

            string sql = "SELECT EXTRACT(MONTH FROM date_arrivee_animal) as mois, COUNT(*) as total " +
                         "FROM animal " +
                         "WHERE EXTRACT(YEAR FROM date_arrivee_animal) = @annee " +
                         "GROUP BY mois;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@annee", annee);
                DataTable dt = DataAccess.ExecuteSelect(cmd);

                foreach (DataRow dr in dt.Rows)
                {
                    int mois = Convert.ToInt32(dr["mois"]);
                    int total = Convert.ToInt32(dr["total"]);
                    valeursMensuelles[mois - 1] = total;
                }
            }

            List<DataPoint> points = new List<DataPoint>();
            PointCollection polylinePoints = new PointCollection();

            double xPas = 70.0;
            double xDepart = 75.0;

            for (int i = 0; i < 12; i++)
            {
                int valeur = valeursMensuelles[i];
                double yPos = 220.0 - (valeur * (200.0 / 30.0));
                if (yPos < 20) yPos = 20;

                points.Add(new DataPoint
                {
                    MoisNom = moisLabels[i],
                    YPosition = yPos - 5,
                    ValeurText = $"{valeur} arrivée(s) en {moisLabels[i]}"
                });

                polylinePoints.Add(new Point(xDepart + (i * xPas), yPos));
            }

            ItemsArrivees.ItemsSource = points;
            PolylineArrivees.Points = polylinePoints;
        }
    }
}