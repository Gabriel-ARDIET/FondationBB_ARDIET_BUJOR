using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Personne
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
                if (value.Length > 100)
                    throw new ArgumentOutOfRangeException("Le nom doit faire moins de 100 caractères");
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
                if (value.Length == 10)
                    throw new ArgumentOutOfRangeException("le numéro de téléphone doit faire 10 caractère");
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
                if (value.Length == 5)
                    throw new ArgumentOutOfRangeException("Le code postal doit faire moins de 5 caractères");
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
    }
}
