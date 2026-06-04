using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public enum Sexe
    {
        Male,
        Femelle
    }
    public class Animal
    {
        private int id;
        private string nom;
        private DateTime? dateNaissance;
        private string? icad;
        private Sexe? unSexe;
        private string? annotation;
        private DateTime dateArrivee;
        private decimal? poids;
        private Race uneRace;
        private Employe? employeCreateur;
        private Statut? unStatut;
        private Etat? unEtat;
        private Adoption? uneAdoption;
        private ObservableCollection<Comportement> comportements;
        private ObservableCollection<Recoit> soinReçus;
        private int idRace;
        private int idCreateur;
        private int? idStatut;
        private int? idEtat;
        private int? idAdoption;

        public Animal()
        {
            this.Comportements = new ObservableCollection<Comportement>();
            this.SoinReçus = new ObservableCollection<Recoit>();
            this.DateArrivee = DateTime.Today;
        }

        public Animal(int id, string nom, DateTime? dateNaissance, string? icad, Sexe unSexe, string? annotation, DateTime dateArrivee, decimal poids, Race uneRace, Employe? employeCreateur,
            Statut? unStatut, Etat? unEtat, ObservableCollection<Comportement> comportements, ObservableCollection<Recoit> soinReçus, Adoption uneAdoption)
        {
            this.Id = id;
            this.Nom = nom;
            this.DateNaissance = dateNaissance;
            this.Icad = icad;
            this.UnSexe = unSexe;
            this.Annotation = annotation;
            this.DateArrivee = dateArrivee;
            this.Poids = poids;
            this.UneRace = uneRace;
            this.EmployeCreateur = employeCreateur;
            this.UnStatut = unStatut;
            this.UnEtat = unEtat;
            this.UneAdoption = uneAdoption;
            this.Comportements = comportements;
            this.SoinReçus = soinReçus;
        }

        public Animal(int id, string nom, DateTime dateNaissance, string icad, Sexe unSexe, string annotation,
            DateTime dateArrivee, decimal poids, int idCreateur, int? idStatut, int? idEtat, int idRace, int? idAdoption)
        {
            this.Id = id;
            this.Nom = nom;
            this.DateNaissance = dateNaissance;
            this.Icad = icad;
            this.UnSexe = unSexe;
            this.Annotation = annotation;
            this.DateArrivee = dateArrivee;
            this.Poids = poids;
            this.Comportements = new ObservableCollection<Comportement>();
            this.SoinReçus = new ObservableCollection<Recoit>();
            this.IdCreateur = idCreateur;
            this.IdStatut = idStatut;
            this.IdEtat = idEtat;
            this.IdRace = idRace;
            this.IdAdoption = idAdoption;
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                if (value.Length > 50)
                    throw new ArgumentOutOfRangeException("Le nom doit faire moins de 50 caractères");
                this.nom = value;
            }
        }

        public DateTime? DateNaissance
        {
            get
            {
                return this.dateNaissance;
            }

            set
            {
                this.dateNaissance = value;
            }
        }

        public string? Icad
        {
            get
            {
                return this.icad;
            }

            set
            {
                
                this.icad = value;
            }
        }

        public Sexe? UnSexe
        {
            get
            {
                return this.unSexe;
            }

            set
            {
                this.unSexe = value;
            }
        }

        public string? Annotation
        {
            get
            {
                return this.annotation;
            }

            set
            {
                this.annotation = value;
            }
        }

        public DateTime DateArrivee
        {
            get
            {
                return this.dateArrivee;
            }

            set
            {
                this.dateArrivee = value;
            }
        }

        public decimal? Poids
        {
            get
            {
                return this.poids;
            }

            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Le poids doit être positif");
                if (value >= 1000)
                    throw new ArgumentOutOfRangeException("Le poids doit être inférieur à 1000");
                this.poids = value;
            }
        }

        public Employe? EmployeCreateur
        {
            get
            {
                return this.employeCreateur;
            }

            set
            {
                this.employeCreateur = value;
            }
        }

        public Statut? UnStatut
        {
            get
            {
                return this.unStatut;
            }

            set
            {
                this.unStatut = value;
            }
        }

        public Etat? UnEtat
        {
            get
            {
                return this.unEtat;
            }

            set
            {
                this.unEtat = value;
            }
        }

        public ObservableCollection<Comportement> Comportements
        {
            get
            {
                return this.comportements;
            }

            set
            {
                this.comportements = value;
            }
        }

        public ObservableCollection<Recoit> SoinReçus
        {
            get
            {
                return this.soinReçus;
            }

            set
            {
                this.soinReçus = value;
            }
        }

        public Race UneRace
        {
            get
            {
                return this.uneRace;
            }

            set
            {
                this.uneRace = value;
            }
        }

        public int IdCreateur
        {
            get
            {
                return this.idCreateur;
            }

            set
            {
                this.idCreateur = value;
            }
        }

        public int? IdStatut
        {
            get
            {
                return this.idStatut;
            }

            set
            {
                this.idStatut = value;
            }
        }

        public int? IdEtat
        {
            get
            {
                return this.idEtat;
            }

            set
            {
                this.idEtat = value;
            }
        }

        public int IdRace
        {
            get
            {
                return this.idRace;
            }

            set
            {
                this.idRace = value;
            }
        }

        public Adoption? UneAdoption
        {
            get
            {
                return this.uneAdoption;
            }

            set
            {
                this.uneAdoption = value;
            }
        }

        public int? IdAdoption
        {
            get
            {
                return this.idAdoption;
            }

            set
            {
                this.idAdoption = value;
            }
        }
        public List<Animal> FindAll()
        {
            List<Animal> list = new List<Animal>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from animal;"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    list.Add(new Animal(
                        (int)dr["id_animal"],
                        (string)dr["nom_animal"],
                        new DateTime((DateOnly)dr["date_naissance_animal"], TimeOnly.MinValue),
                        (string)dr["i_cad_animal"],
                        EnumConverter.ConvertStringToSexe((string)dr["sexe_animal"]),
                        dr["annotation_animal"].ToString(),
                        new DateTime((DateOnly)dr["date_arrivee_animal"], TimeOnly.MinValue),
                        (decimal)dr["poids_animal"],
                        (int)dr["id_employe"],
                        (int?)dr["id_statut"],
                        (int?)dr["id_etat"],
                        (int)dr["id_race"],
                        dr["id_adoption"] is System.DBNull ? null : (int?)dr["id_adoption"]
                        ));
            }
            return list;
        }
        public int Create()
        {
            // On prépare la requête en utilisant les vrais noms de colonnes de tes captures d'écran
            string query = @"INSERT INTO animal 
                     (id_statut, id_race, id_etat, nom_animal, date_naissance_animal, i_cad_animal, sexe_animal, date_arrivee_animal, poids_animal) 
                     VALUES 
                     (@id_statut, @id_race, @id_etat, @nom_animal, @date_naissance, @i_cad, @sexe, @date_arrivee, @poids) 
                     RETURNING id_animal;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                // 1. Récupération des IDs depuis les objets associés (avec sécurité anti-null)
                cmd.Parameters.AddWithValue("@id_statut", UnStatut != null ? (object)UnStatut.Id : DBNull.Value);
                cmd.Parameters.AddWithValue("@id_race", UneRace != null ? (object)UneRace.Id : DBNull.Value);
                cmd.Parameters.AddWithValue("@id_etat", UnEtat != null ? (object)UnEtat.Id : DBNull.Value);

                // 2. Mapping des autres propriétés de l'animal
                cmd.Parameters.AddWithValue("@nom_animal", Nom ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@date_naissance", DateNaissance.HasValue ? (object)DateNaissance.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@i_cad", Icad ?? (object)DBNull.Value);

                // Convertit l'enum Sexe en 'M' ou 'F' comme attendu dans ta base
                cmd.Parameters.AddWithValue("@sexe", UnSexe == Sexe.Male ? "M" : "F");

                cmd.Parameters.AddWithValue("@date_arrivee", DateArrivee);
                cmd.Parameters.AddWithValue("@poids", Poids);

                // Exécution de la requête via ton DataAccess (renvoie l'id_animal généré)
                return DataAccess.ExecuteInsert(cmd);
            }
        }
        //inspirer de create pour faire la suite (update, delete, read)
        /*public void Read()
        {
            using (var cmdSelect = new NpgsqlCommand("select * from  chiens  where idchien =@id;"))
            {
                cmdSelect.Parameters.AddWithValue("id", this.id);

                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                this.Nom = (String)dt.Rows[0]["nom"];
                this.Poids = (double)dt.Rows[0]["poids"];

            }

        }
        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update chiens set nom =@nom ,  maitre = @maitre,  poids = @poids  where idchien =@id;"))
            {
                cmdUpdate.Parameters.AddWithValue("nom", this.Nom);
                cmdUpdate.Parameters.AddWithValue("poids", this.Poids);
                cmdUpdate.Parameters.AddWithValue("id", this.Id);
                return DataAccess.ExecuteSet(cmdUpdate);
            }
        }
        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from chiens  where idchien =@id;"))
            {
                cmdUpdate.Parameters.AddWithValue("id", this.Id);
                return DataAccess.ExecuteSet(cmdUpdate);
            }
        }*/
        public string SoinsRecusResume
        {
            get
            {
                if (SoinReçus == null || !SoinReçus.Any()) return "Aucun soin enregistré";
                // Joint les libellés des soins avec leur date
                return string.Join(Environment.NewLine, SoinReçus.Select(s => $"- {s.UnSoin?.Libelle} ({s.DateSoin:dd/MM/yyyy})"));
            }
        }

        public string ComportementsResume
        {
            get
            {
                if (Comportements == null || !Comportements.Any()) return "Aucun comportement enregistré";
                // À ajuster selon la structure de ta classe Comportement (supposons qu'elle possède une propriété 'Libelle')
                return string.Join(", ", Comportements.Select(c => c.Libelle));
            }
        }
    }
}
