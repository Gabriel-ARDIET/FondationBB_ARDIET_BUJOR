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
        private Sexe unSexe;
        private string? annotation;
        private DateTime dateArrivee;
        private double poids;
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
                if (value.Length == 15)
                    throw new ArgumentOutOfRangeException("L'ICAD doit faire 15 caractères");
                this.icad = value;
            }
        }

        public Sexe UnSexe
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

        public double Poids
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
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from  animal;"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    DateOnly dateN = (DateOnly)dr["date_naissance_animal"];
                    DateTime dtNaissance = dateN.ToDateTime(TimeOnly.MinValue);
                    DateOnly dateA = (DateOnly)dr["date_arrivee_animal"];
                    DateTime dtArrivee = dateA.ToDateTime(TimeOnly.MinValue);

                    lesAnimaux.Add(new Animal((int)dr["id_animal"], (String)dr["nom_animal"], dtNaissance,
                    (String)dr["i_cad_animal"], (Sexe)dr["sexe_animal"], (String)dr["anotation_animal"],
                    dtArrivee, (double)dr["poids_animal"], (int)dr["id_employe"], (int)dr["id_status"],
                    (int)dr["id_etat"], (int)dr["id_race"], (int)dr["id_adoption"]));
                }
            }
            return lesAnimaux;
        }
        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into animal (i_cad_animal,nom_animal,id_race,sexe_animal,date_naissance_animal,poids_animal,date_arrivee_animal,anotation_animal,id_status,id_etat) values (@i_cad_animal,@nom_animal,@id_race,@sexe_animal,@date_naissance_animal,@poids_animal,@date_arrivee_animal,@anotation_animal,@id_status,@id_etat) RETURNING id_animal"))
            //il faut aussi insert le soin et le comportement (voir le bouton ajouter pour voir tout ce qu'il faut) ATTENTION de bien tout modifier au niveau de l'XML Window animal au niveau du binding
            {
                cmdInsert.Parameters.AddWithValue("i_cad_animal", this.Icad);
                cmdInsert.Parameters.AddWithValue("nom_animal", this.Nom);
                //On fait comment pour espece ?
                cmdInsert.Parameters.AddWithValue("id_race", this.UneRace);
                cmdInsert.Parameters.AddWithValue("sexe_animal", this.UnSexe);
                cmdInsert.Parameters.AddWithValue("date_naissance_animal", this.DateNaissance);
                cmdInsert.Parameters.AddWithValue("poids_animal", this.Poids);
                cmdInsert.Parameters.AddWithValue("date_arrivee_animal", this.DateArrivee);
                //cmdInsert.Parameters.AddWithValue("libelle_soin", this.SoinReçus); recus -> soins, (ne se trouve pas dans SoinReçus) comment faire pour prendre seulement libelle_soin?
                //cmdInsert.Parameters.AddWithValue("date_soin", this.SoinReçus); comment faire pour prendre juste la date ?
                cmdInsert.Parameters.AddWithValue("anotation_animal", this.Annotation);
                //cmdInsert.Parameters.AddWithValue("libelle_comportement", this.Comportements); animal_comportement -> comportement, (ne se trouve pas dans Comportements) comment faire pour prendre seulement libelle_comportement ?
                cmdInsert.Parameters.AddWithValue("id_status", this.UnStatut);
                cmdInsert.Parameters.AddWithValue("id_etat", this.UnEtat);
                
                nb = DataAccess.ExecuteInsert(cmdInsert);
            }
            this.Id = nb;
            return nb;
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
    }
}
