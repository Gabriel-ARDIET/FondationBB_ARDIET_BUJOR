using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Personne
    {
        private int id;
        private string nom;
        private string prenom;
        private DateTime dateNaissance;
        private string telephone;
        private string rue;
        private string cp;
        private string ville;
        private string mail;
        private DateTime dateCreation;

        public Personne()
        {
        }

        public Personne(int id, string nom, string prenom, DateTime dateNaissance, string telephone, string rue, string cp, string ville, string mail, DateTime dateCreation)
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

        public string Prenom
        {
            get
            {
                return this.prenom;
            }

            set
            {
                this.prenom = value;
            }
        }

        public DateTime DateNaissance
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
                this.ville = value;
            }
        }

        public string Mail
        {
            get
            {
                return this.mail;
            }

            set
            {
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
    }
}
