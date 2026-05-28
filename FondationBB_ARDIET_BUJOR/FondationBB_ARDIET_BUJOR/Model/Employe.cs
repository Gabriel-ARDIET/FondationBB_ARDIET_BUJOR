using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public enum Role
    {
        Responsable,
        Bénévole
    }
    public class Employe
    {
        private int id;
        private string nom;
        private string prenom;
        private string login;
        private Role unRole;

        public Employe()
        {
        }

        public Employe(int id, string nom, string prenom, string login, Role unRole)
        {
            this.Id = id;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Login = login;
            this.UnRole = unRole;
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

        public string Login
        {
            get
            {
                return this.login;
            }

            set
            {
                if (value.Length > 50)
                    throw new ArgumentOutOfRangeException("Le login doit faire moins de 50 caractères");
                this.login = value;
            }
        }

        public Role UnRole
        {
            get
            {
                return this.unRole;
            }

            set
            {
                this.unRole = value;
            }
        }
    }
}
