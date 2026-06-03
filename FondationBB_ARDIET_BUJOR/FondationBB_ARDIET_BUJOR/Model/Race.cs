using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public enum Taille
    {
        Grand,
        Moyen,
        Petit
    }
    public class Race
    {
        private int id;
        private string libelle;
        private Taille uneTaille;
        private Espece uneEspece;

        public Race()
        {
        }

        public Race(int id, string libelle, Taille uneTaille, Espece uneEspece)
        {
            this.Id = id;
            this.Libelle = libelle;
            this.UneTaille = uneTaille;
            this.UneEspece = uneEspece;
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

        public Taille UneTaille
        {
            get
            {
                return this.uneTaille;
            }

            set
            {
                this.uneTaille = value;
            }
        }

        public Espece UneEspece
        {
            get
            {
                return this.uneEspece;
            }

            set
            {
                this.uneEspece = value;
            }
        }
        public List<Race> FindAll()
        {
            List<Race> lesRaces = new List<Race>();

            string requete = "SELECT id_race, libelle_race, taille_race, id_espece FROM race ORDER BY libelle_race ASC";

            // Création de la commande Npgsql
            NpgsqlCommand cmd = new NpgsqlCommand(requete);

            // Appel à votre classe DataAccess statique
            DataTable dt = DataAccess.ExecuteSelect(cmd);

            // On parcourt les lignes du DataTable
            foreach (DataRow row in dt.Rows)
            {
                Race r = new Race();
                r.Id = Convert.ToInt32(row["id_race"]);
                r.Libelle = row["libelle_race"].ToString();

                // Conversion de la taille
                if (Enum.TryParse(row["taille_race"].ToString(), true, out Taille t))
                    r.UneTaille = t;

                // Note : Ici vous ne récupérez que l'ID de l'espèce. 
                // Si vous avez besoin de l'objet complet, il faudra faire un "new Espece { Id = ... }"
                r.UneEspece = new Espece { Id = Convert.ToInt32(row["id_espece"]) };

                lesRaces.Add(r);
            }

            return lesRaces;
        }
    }
}
