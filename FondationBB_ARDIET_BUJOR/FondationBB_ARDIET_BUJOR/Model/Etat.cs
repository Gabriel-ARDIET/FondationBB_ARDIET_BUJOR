using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Etat
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
            return liste;
        }

        public override string ToString() => Libelle;
    }
}