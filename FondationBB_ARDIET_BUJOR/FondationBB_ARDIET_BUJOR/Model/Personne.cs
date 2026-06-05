using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Personne : ICrud<Personne>
    {
        private int id;
        private string nom;
        private string prenom;
        private DateTime? dateNaissance;
        private string telephone;
        private string numero;
        private string rue;
        private string cp;
        private string ville;
        private string? mail;
        private DateTime dateCreation;
        public Personne()
        {
        }

        public Personne(int id, string nom, string prenom, DateTime? dateNaissance, string numero, string rue, string cp, string ville, string telephone, string? mail, DateTime dateCreation)
        {
            this.Id = id;
            this.Nom = nom;
            this.Prenom = prenom;
            this.DateNaissance = dateNaissance;
            this.Telephone = telephone;
            this.Rue = rue;
            this.Cp = cp;
            this.Ville = ville;
            this.Mail = mail;
            this.DateCreation = dateCreation;
            this.Numero = numero;
        }

        public Personne(int id, string nom, string prenom, DateTime? dateNaissance, string telephone, string rue, string cp, string ville, string? mail, string numero)
        {
            this.Id = id;
            this.Nom = nom;
            this.Prenom = prenom;
            this.DateNaissance = dateNaissance;
            this.Telephone = telephone;
            this.Rue = rue;
            this.Cp = cp;
            this.Ville = ville;
            this.Mail = mail;
            this.Numero = numero;
            this.DateCreation = DateTime.Today;
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
                /*if (value.Length > 100)
                    throw new ArgumentOutOfRangeException("Le nom doit faire moins de 100 caractères");*/
                this.nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return this.prenom;
            }

            set
            {
                if (value.Length > 100)
                    throw new ArgumentOutOfRangeException("Le prénom doit faire moins de 100 caractères");
                this.prenom = value;
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

        public string Telephone
        {
            get
            {
                return this.telephone;
            }

            set
            {
                
                this.telephone = value;
            }
        }

        public string Rue
        {
            get
            {
                return this.rue;
            }

            set
            {
                if (value.Length > 100)
                    throw new ArgumentOutOfRangeException("Le nom de la rue doit faire moins de 100 caractères");
                this.rue = value;
            }
        }

        public string Cp
        {
            get
            {
                return this.cp;
            }

            set
            {
                if (value.Length > 10)
                    throw new ArgumentOutOfRangeException("Le code postal doit faire moins de 10 caractères");
                this.cp = value;
            }
        }

        public string Ville
        {
            get
            {
                return this.ville;
            }

            set
            {
                if (value.Length > 50)
                    throw new ArgumentOutOfRangeException("Le nom de la ville doit faire moins de 50 caractères");
                this.ville = value;
            }
        }

        public string? Mail
        {
            get
            {
                return this.mail;
            }

            set
            {
                if (value.Length > 100)
                    throw new ArgumentOutOfRangeException("Le mail doit faire moins de 100 caractères");
                this.mail = value;
            }
        }

        public DateTime DateCreation
        {
            get
            {
                return this.dateCreation;
            }

            set
            {
                this.dateCreation = value;
            }
        }

        public string Numero
        {
            get
            {
                return this.numero;
            }

            set
            {
                if (value.Length > 10)
                    throw new ArgumentOutOfRangeException("le numéro doit être inférieur à 10 caractère");
                this.numero = value;
            }
        }

        public int Create()
        {
            string sql = "INSERT INTO personne (nom_personne, prenom_personne, date_naissance_personne, telephone_personne, numero_personne, rue_personne, cp_personne, ville_personne, mail_personne, date_creation_personne) " +
                         "VALUES (@nom, @prenom, @dateNaissance, @telephone, @numero, @rue, @cp, @ville, @mail, @dateCreation) " +
                         "RETURNING id_personne;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@nom", this.Nom);
                cmd.Parameters.AddWithValue("@prenom", this.Prenom);
                cmd.Parameters.AddWithValue("@dateNaissance", this.DateNaissance.HasValue ? DateOnly.FromDateTime(this.DateNaissance.Value) : DBNull.Value);
                cmd.Parameters.AddWithValue("@telephone", this.Telephone);
                cmd.Parameters.AddWithValue("@numero", this.Numero);
                cmd.Parameters.AddWithValue("@rue", this.Rue);
                cmd.Parameters.AddWithValue("@cp", this.Cp);
                cmd.Parameters.AddWithValue("@ville", this.Ville);
                cmd.Parameters.AddWithValue("@mail", string.IsNullOrEmpty(this.Mail) ? DBNull.Value : this.Mail);
                cmd.Parameters.AddWithValue("@dateCreation", DateOnly.FromDateTime(this.DateCreation));
                this.Id = DataAccess.ExecuteInsert(cmd);

                return this.Id > 0 ? 1 : 0;
            }
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            string sql = "UPDATE personne SET nom_personne = @nom, prenom_personne = @prenom, date_naissance_personne = @dateNaissance, " +
                         "telephone_personne = @telephone, numero_personne = @numero, rue_personne = @rue, cp_personne = @cp, ville_personne = @ville, " +
                         "mail_personne = @mail, date_creation_personne = @dateCreation " +
                         "WHERE id_personne = @id;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", this.Id);
                cmd.Parameters.AddWithValue("@nom", this.Nom);
                cmd.Parameters.AddWithValue("@prenom", this.Prenom);
                cmd.Parameters.AddWithValue("@dateNaissance", this.DateNaissance.HasValue ? DateOnly.FromDateTime(this.DateNaissance.Value) : DBNull.Value);
                cmd.Parameters.AddWithValue("@telephone", this.Telephone);
                cmd.Parameters.AddWithValue("@numero", this.Numero);
                cmd.Parameters.AddWithValue("@rue", this.Rue);
                cmd.Parameters.AddWithValue("@cp", this.Cp);
                cmd.Parameters.AddWithValue("@ville", this.Ville);
                cmd.Parameters.AddWithValue("@mail", string.IsNullOrEmpty(this.Mail) ? DBNull.Value : this.Mail);
                cmd.Parameters.AddWithValue("@dateCreation", DateOnly.FromDateTime(this.DateCreation));

                return DataAccess.ExecuteSet(cmd);
            }
        }

        public int Delete()
        {
            string sql = "DELETE FROM personne WHERE id_personne = @id;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", this.Id);

                return DataAccess.ExecuteSet(cmd);
            }
        }

        public List<Personne> FindAll()
        {
            List<Personne> list = new List<Personne>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from personne;"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    list.Add(new Personne(
                        (int)dr["id_personne"],
                        (string)dr["nom_personne"],
                        (string)dr["prenom_personne"],
                        new DateTime((DateOnly)dr["date_naissance_personne"],TimeOnly.MinValue),
                        (string)dr["numero_personne"],
                        (string)dr["rue_personne"],
                        (string)dr["cp_personne"],
                        (string)dr["ville_personne"],
                        (string)dr["telephone_personne"],
                        (string?)dr["mail_personne"],                     
                        new DateTime((DateOnly)dr["date_creation_personne"],TimeOnly.MinValue)
                        ));
            }
            return list;
        }
        
        public List<Personne> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }
    }
}
