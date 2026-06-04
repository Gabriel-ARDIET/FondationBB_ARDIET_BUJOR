using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Statut
    {
        private int id;
        private string libelle;

        public Statut() { }

        public Statut(int id, string libelle)
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
        /// Récupère tous les statuts de la base de données
        /// </summary>
        public List<Statut> FindAll()
        {
            List<Statut> liste = new List<Statut>();
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT id_statut, libelle_statut FROM statut ORDER BY libelle_statut"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    liste.Add(new Statut(
                        Convert.ToInt32(row["id_statut"]),
                        row["libelle_statut"].ToString()
                    ));
                }
            }
            return liste;
        }

        // Permet un affichage propre par défaut si aucun DisplayMemberPath n'est défini
        public override string ToString() => Libelle;
    }
}