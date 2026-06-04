using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Espece : ICrud<Espece>
    {
        private int id;
        private string libelle;

        public Espece()
        {
        }

        public Espece(int id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
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

        public string Libelle
        {
            get
            {
                return this.libelle;
            }

            set
            {
                if (value.Length > 30)
                    throw new ArgumentOutOfRangeException("Le libéllé doit faire moins de 30 caractères");
                this.libelle = value;
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

        public List<Espece> FindAll()
        {
            List<Espece> list = new List<Espece>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from espece;"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    list.Add(new Espece(
                        (int)dr["id_espece"],
                        (string)dr["libelle_espece"]
                        ));
            }
            return list;
        }

        public List<Espece> FindBySelection(string criteres)
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
