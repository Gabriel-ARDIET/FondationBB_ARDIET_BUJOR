using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
    public class Employe : ICrud<Employe>
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

        public int Create()
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public List<Employe> FindAll()
        {
            List<Employe> lesEmployes = new List<Employe>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from boxs;"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesEmployes.Add(new Employe((int)dr["id_employe"], (string)dr["nom_employe"], (string)dr["prenom_employe"],(string)dr["login_employe"], Role.Responsable));
            }
            return lesEmployes;
        }

        public List<Employe> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            throw new NotImplementedException();
        }
    }
}
