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
        private double? poids;
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

        public Animal(int id, string nom, DateTime? dateNaissance, string? icad, Sexe unSexe, string? annotation, DateTime dateArrivee, double poids, Race uneRace, Employe? employeCreateur,
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
            DateTime dateArrivee, double poids, int idCreateur, int? idStatut, int? idEtat, int idRace, int? idAdoption)
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

        public double? Poids
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
            List<Animal> lesAnimaux = new List<Animal>();

            // Modification des colonnes sélectionnées pour correspondre à la structure réelle de votre BDD
            string query = @"
        SELECT a.*, 
               r.libelle_race, r.taille_race, 
               e.id_espece, e.libelle_espece
        FROM animal a
        INNER JOIN race r ON a.id_race = r.id_race
        INNER JOIN espece e ON r.id_espece = e.id_espece;";

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(query))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    DateTime dtNaissance = dr["date_naissance_animal"] != DBNull.Value ? ((DateOnly)dr["date_naissance_animal"]).ToDateTime(TimeOnly.MinValue) : DateTime.MinValue;
                    DateTime dtArrivee = dr["date_arrivee_animal"] != DBNull.Value ? ((DateOnly)dr["date_arrivee_animal"]).ToDateTime(TimeOnly.MinValue) : DateTime.Today;

                    Sexe unSexeEnum = (dr["sexe_animal"].ToString() == "F") ? Sexe.Femelle : Sexe.Male;

                    // 1. Instanciation de l'objet Espece (avec libelle_espece)
                    int idEspece = Convert.ToInt32(dr["id_espece"]);
                    string libelleEspece = dr["libelle_espece"].ToString();
                    Espece lEspece = new Espece(idEspece, libelleEspece);

                    // 2. Instanciation de l'objet Race (avec libelle_race et taille_race)
                    int idRace = Convert.ToInt32(dr["id_race"]);
                    string libelleRace = dr["libelle_race"].ToString();

                    Taille tailleRaceEnum = Taille.Moyen;
                    if (dr["taille_race"] != DBNull.Value)
                    {
                        Enum.TryParse(dr["taille_race"].ToString(), true, out tailleRaceEnum);
                    }

                    Race laRace = new Race(idRace, libelleRace, tailleRaceEnum, lEspece);

                    // 3. Création de l'animal complet
                    Animal nouvelAnimal = new Animal(
                        Convert.ToInt32(dr["id_animal"]),
                        dr["nom_animal"].ToString(),
                        dtNaissance,
                        dr["i_cad_animal"] != DBNull.Value ? dr["i_cad_animal"].ToString() : null,
                        unSexeEnum,
                        dr["annotation_animal"] != DBNull.Value ? dr["annotation_animal"].ToString() : null,
                        dtArrivee,
                        dr["poids_animal"] != DBNull.Value ? Convert.ToDouble(dr["poids_animal"]) : 0.0,
                        Convert.ToInt32(dr["id_employe"]),
                        dr["id_statut"] != DBNull.Value ? Convert.ToInt32(dr["id_statut"]) : null,
                        dr["id_etat"] != DBNull.Value ? Convert.ToInt32(dr["id_etat"]) : null,
                        idRace,
                        dr["id_adoption"] != DBNull.Value ? Convert.ToInt32(dr["id_adoption"]) : null
                    );

                    // Liaison de l'objet Race à l'animal
                    nouvelAnimal.UneRace = laRace;

                    lesAnimaux.Add(nouvelAnimal);
                }
            }
            return lesAnimaux;
        }
        public int Create()
        {
            int idGenere = 0;

            // Requête SQL d'insertion conforme à ton modèle
            string query = @"
        INSERT INTO animal 
        (id_race, nom_animal, date_naissance_animal, i_cad_animal, sexe_animal, annotation_animal, date_arrivee_animal, poids_animal) 
        VALUES 
        (@id_race, @nom, @dateNaiss, @icad, @sexe, @annotation, @dateArrivee, @poids) 
        RETURNING id_animal;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, DataAccess.GetConnection()))
            {
                // 1. Liaison de la race (ton code d'origine parfait)
                int raceId = this.UneRace != null ? this.UneRace.Id : this.IdRace;
                cmd.Parameters.AddWithValue("@id_race", raceId);

                cmd.Parameters.AddWithValue("@nom", this.Nom);

                // 2. Sexe : Conversion de ton énumération en chaîne "F" ou "M"
                cmd.Parameters.AddWithValue("@sexe", this.UnSexe == Model.Sexe.Femelle ? "F" : "M");

                // 3. CORRECTION DES TYPES AVANCÉS POUR POSTGRESQL
                // Date d'arrivée : On force le type NpgsqlDbType.Date pour éviter que PostgreSQL ne le confonde avec un Timestamp
                cmd.Parameters.Add("@dateArrivee", NpgsqlTypes.NpgsqlDbType.Date).Value = this.DateArrivee;

                // Poids : Ton double? doit être envoyé sous forme de Decimal pour coller au type numeric(5,2) de la BDD
                cmd.Parameters.Add("@poids", NpgsqlTypes.NpgsqlDbType.Numeric).Value = this.Poids.HasValue ? Convert.ToDecimal(this.Poids.Value) : (object)DBNull.Value;

                // Date de naissance (optionnelle) : Forcée en type Date également
                if (this.DateNaissance.HasValue)
                    cmd.Parameters.Add("@dateNaiss", NpgsqlTypes.NpgsqlDbType.Date).Value = this.DateNaissance.Value;
                else
                    cmd.Parameters.Add("@dateNaiss", NpgsqlTypes.NpgsqlDbType.Date).Value = DBNull.Value;

                // 4. Gestion des chaînes optionnelles (ton code d'origine)
                // ICAD
                if (!string.IsNullOrWhiteSpace(this.Icad))
                    cmd.Parameters.AddWithValue("@icad", this.Icad);
                else
                    cmd.Parameters.AddWithValue("@icad", DBNull.Value);

                // Annotation
                if (!string.IsNullOrWhiteSpace(this.Annotation))
                    cmd.Parameters.AddWithValue("@annotation", this.Annotation);
                else
                    cmd.Parameters.AddWithValue("@annotation", DBNull.Value);

                // 5. SÉCURITÉ DE LA CONNEXION
                // Si la connexion retournée par DataAccess n'est pas encore ouverte, on l'ouvre avant l'exécution
                if (cmd.Connection.State != System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                // Exécution et récupération de l'ID auto-incrémenté
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    idGenere = Convert.ToInt32(result);
                    this.Id = idGenere; // Met à jour l'objet actuel
                }
            }

            return idGenere;
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
