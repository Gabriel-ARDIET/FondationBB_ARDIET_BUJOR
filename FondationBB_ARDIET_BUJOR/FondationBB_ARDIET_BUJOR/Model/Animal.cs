using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public enum Sexe
    {
        Male,
        Femelle
    }
    public class Animal : ICrud<Animal>
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
            this.IdRace = uneRace.Id;
            this.EmployeCreateur = employeCreateur;
            this.IdCreateur = employeCreateur.Id;
            this.UnStatut = unStatut;
            this.IdStatut = unStatut.Id;
            this.UnEtat = unEtat;
            this.IdEtat = unEtat.Id;
            this.UneAdoption = uneAdoption;
            this.IdAdoption = uneAdoption?.Id;
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
                if (value.Length != 15)
                    throw new ArgumentOutOfRangeException("L'icad doit faire 15 caractères");
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
        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - this.DateNaissance.Value.Year;
                if (DateTime.Now.DayOfYear > this.DateNaissance.Value.DayOfYear)
                    age--;
                return age;
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
                        dr["annotation_animal"] is DBNull ? null : (string?)dr["annotation_animal"],
                        new DateTime((DateOnly)dr["date_arrivee_animal"], TimeOnly.MinValue),
                        (decimal)dr["poids_animal"],
                        (int)dr["id_employe"],
                        (int?)dr["id_statut"],
                        (int?)dr["id_etat"],
                        (int)dr["id_race"],
                        dr["id_adoption"] is DBNull ? null : (int?)dr["id_adoption"]
                        ));
            }
            return list;
        }
        public int Create()
        {
            int nb = 0;
            string sql = "INSERT INTO animal (nom_animal, date_naissance_animal, i_cad_animal, sexe_animal, annotation_animal, date_arrivee_animal, poids_animal, id_race, id_employe, id_statut, id_etat) " +
                         "VALUES (@nom, @dateNaissance, @icad, @sexe, @annotation, @dateArrivee, @poids, @idRace, @idCreateur, @idStatut, @idEtat) " +
                         "RETURNING id_animal;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@nom", this.Nom);
                cmd.Parameters.AddWithValue("@dateNaissance", this.DateNaissance.HasValue ? DateOnly.FromDateTime(this.DateNaissance.Value) : DBNull.Value);
                cmd.Parameters.AddWithValue("@icad", string.IsNullOrEmpty(this.Icad) ? DBNull.Value : this.Icad);
                cmd.Parameters.AddWithValue("@sexe", EnumConverter.ConvertSexeToString((Sexe)this.UnSexe));
                cmd.Parameters.AddWithValue("@annotation", string.IsNullOrEmpty(this.Annotation) ? DBNull.Value : this.Annotation);
                cmd.Parameters.AddWithValue("@dateArrivee", DateOnly.FromDateTime(this.DateArrivee));
                cmd.Parameters.AddWithValue("@poids", this.Poids.HasValue ? this.Poids.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@idRace", this.IdRace);
                cmd.Parameters.AddWithValue("@idCreateur", this.IdCreateur);
                cmd.Parameters.AddWithValue("@idStatut", this.IdStatut.HasValue ? this.IdStatut.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@idEtat", this.IdEtat.HasValue ? this.IdEtat.Value : DBNull.Value);
                nb = DataAccess.ExecuteInsert(cmd);
            }
            this.Id = nb;
            return nb;
        }
        public int Delete()
        {
            string query = @"
        DELETE FROM recoit WHERE id_animal = @id;
        DELETE FROM animal_comportement WHERE id_animal = @id;
        DELETE FROM animal WHERE id_animal = @id;";

            using (NpgsqlCommand cmdDelete = new NpgsqlCommand(query))
            {
                cmdDelete.Parameters.AddWithValue("@id", this.Id);

                return DataAccess.ExecuteSet(cmdDelete);
            }
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            string sql = "UPDATE animal SET nom_animal = @nom, date_naissance_animal = @dateNaissance, i_cad_animal = @icad, " +
                         "sexe_animal = @sexe, annotation_animal = @annotation, date_arrivee_animal = @dateArrivee, poids_animal = @poids, " +
                         "id_race = @idRace, id_employe = @idCreateur, id_statut = @idStatut, id_etat = @idEtat " +
                         "WHERE id_animal = @id;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", this.Id);
                cmd.Parameters.AddWithValue("@nom", this.Nom);
                cmd.Parameters.AddWithValue("@dateNaissance", this.DateNaissance.HasValue ? DateOnly.FromDateTime(this.DateNaissance.Value) : DBNull.Value);
                cmd.Parameters.AddWithValue("@icad", string.IsNullOrEmpty(this.Icad) ? DBNull.Value : this.Icad);
                cmd.Parameters.AddWithValue("@sexe", EnumConverter.ConvertSexeToString((Sexe)this.UnSexe));
                cmd.Parameters.AddWithValue("@annotation", string.IsNullOrEmpty(this.Annotation) ? DBNull.Value : this.Annotation);
                cmd.Parameters.AddWithValue("@dateArrivee", DateOnly.FromDateTime(this.DateArrivee));
                cmd.Parameters.AddWithValue("@poids", this.Poids.HasValue ? this.Poids.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@idRace", this.IdRace);
                cmd.Parameters.AddWithValue("@idCreateur", this.IdCreateur);
                cmd.Parameters.AddWithValue("@idStatut", this.IdStatut.HasValue ? this.IdStatut.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@idEtat", this.IdEtat.HasValue ? this.IdEtat.Value : DBNull.Value);

                return DataAccess.ExecuteSet(cmd);
            }
        }

        public List<Animal> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }
    }
}
