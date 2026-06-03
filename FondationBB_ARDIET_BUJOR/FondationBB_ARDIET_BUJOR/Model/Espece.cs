using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Espece
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
        public static List<Espece> FindAll()
        {
            List<Espece> liste = new List<Espece>();
            string req = "SELECT id_espece, libelle_espece FROM espece";
            NpgsqlCommand cmd = new NpgsqlCommand(req);

            DataTable dt = DataAccess.ExecuteSelect(cmd);
            foreach (DataRow row in dt.Rows)
            {
                liste.Add(new Espece
                {
                    Id = Convert.ToInt32(row["id_espece"]),
                    Libelle = row["libelle_espece"].ToString()
                });
            }
            return liste;
        }
    }
}
