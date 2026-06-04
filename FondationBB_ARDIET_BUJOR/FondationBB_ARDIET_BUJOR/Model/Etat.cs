using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Etat : ICrud<Etat>
    {
        private int id;
        private string libelle;

        public Etat() { }

        public Etat(int id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

        public int Id
        {
            get => this.id;
            set => this.id = value;
        }

        public string Libelle
        {
            get => this.libelle;
            set
            {
                if (value.Length > 30)
                    throw new ArgumentOutOfRangeException("Le libellé doit faire moins de 30 caractères");
                this.libelle = value;
            }
        }

        /// <summary>
        /// Récupère tous les états de la base de données
        /// </summary>
        public List<Etat> FindAll()
        {
            List<Etat> liste = new List<Etat>();
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT id_etat, libelle_etat FROM etat ORDER BY libelle_etat"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    liste.Add(new Etat(
                        Convert.ToInt32(row["id_etat"]),
                        row["libelle_etat"].ToString()
                    ));
                }
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

        public List<Etat> FindAll()
        {
            {
                List<Etat> list = new List<Etat>();
                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from etat;"))
                {
                    DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                    foreach (DataRow dr in dt.Rows)
                        list.Add(new Etat(
                            (int)dr["id_etat"],
                            (string)dr["libelle_etat"]
                            ));
                }
                return list;
            }
        }

        public List<Etat> FindBySelection(string criteres)
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

        public override string ToString() => Libelle;
    }
}