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
        private int idRace;
        private int idCreateur;
        private int? idStatut;
        private int? idEtat;
        private int? idAdoption;

        public Animal()
        {
            this.DateArrivee = DateTime.Today;
        }

        public Animal(int id, string nom, DateTime? dateNaissance, string? icad, Sexe unSexe, string? annotation, DateTime dateArrivee, decimal poids, Race uneRace, Employe? employeCreateur,
            Statut? unStatut, Etat? unEtat, Adoption uneAdoption)
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
            this.IdCreateur = idCreateur;
            this.IdStatut = idStatut;
            this.IdEtat = idEtat;
            this.IdRace = idRace;
            this.IdAdoption = idAdoption;
        }
        public Animal Copy()
        {
            Animal autre = (Animal)this.MemberwiseClone();
            autre.DateNaissance = new DateTime(this.DateNaissance.Value.Ticks);
            autre.DateArrivee = new DateTime(this.DateArrivee.Ticks);
            return autre;
        }
        public void UpdateFrom(Animal a)
        {
            this.Nom = a.Nom;
            this.DateNaissance = a.DateNaissance;
            this.Icad = a.Icad;
            this.UnSexe = a.UnSexe;
            this.Annotation = a.Annotation;
            this.DateArrivee = a.DateArrivee;
            this.Poids = a.Poids;
            this.IdCreateur = a.IdCreateur;
            this.EmployeCreateur = a.EmployeCreateur;
            this.IdStatut = a.IdStatut;
            this.UnStatut = a.UnStatut;
            this.IdEtat = a.IdEtat;
            this.UnEtat = a.UnEtat;
            this.IdRace = a.IdRace;
            this.UneRace = a.UneRace;
            this.IdAdoption = a.IdAdoption;
            this.UneAdoption = a.UneAdoption;
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
            // MODIFICATION : Ajout de la colonne id_employe et de son paramètre @id_employe
            string query = @"INSERT INTO animal 
                     (id_statut, id_race, id_etat, id_employe, nom_animal, date_naissance_animal, i_cad_animal, sexe_animal, date_arrivee_animal, poids_animal) 
                     VALUES 
                     (@id_statut, @id_race, @id_etat, @id_employe, @nom_animal, @date_naissance, @i_cad, @sexe, @date_arrivee, @poids) 
                     RETURNING id_animal;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                // 1. Récupération des IDs depuis les objets associés (avec sécurité anti-null)
                cmd.Parameters.AddWithValue("@id_statut", UnStatut != null ? (object)UnStatut.Id : DBNull.Value);
                cmd.Parameters.AddWithValue("@id_race", UneRace != null ? (object)UneRace.Id : DBNull.Value);
                cmd.Parameters.AddWithValue("@id_etat", UnEtat != null ? (object)UnEtat.Id : DBNull.Value);

                // MODIFICATION : Liaison du paramètre SQL avec la propriété IdCreateur de l'objet
                cmd.Parameters.AddWithValue("@id_employe", IdCreateur != 0 ? (object)IdCreateur : DBNull.Value);

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
        public int Delete()
        {
            // Chaîne de requêtes pour nettoyer les tables liées puis supprimer l'animal
            string query = @"
        DELETE FROM recoit WHERE id_animal = @id;
        DELETE FROM animal_comportement WHERE id_animal = @id;
        DELETE FROM animal WHERE id_animal = @id;";

            using (NpgsqlCommand cmdDelete = new NpgsqlCommand(query))
            {
                // Association du paramètre avec l'ID de l'animal actuel
                cmdDelete.Parameters.AddWithValue("@id", this.Id);

                // Utilisation de votre méthode DataAccess existante
                return DataAccess.ExecuteSet(cmdDelete);
            }
        }
    }
}
