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
    public class Race : ICrud<Race>
    {
        private int id;
        private string libelle;
        private Taille uneTaille;
        private Espece uneEspece;
        private int idEspece;

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

        public Race(int id, string libelle, Taille uneTaille, int idEspece)
        {
            this.Id = id;
            this.Libelle = libelle;
            this.UneTaille = uneTaille;
            this.IdEspece = idEspece;
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

        public int IdEspece
        {
            get
            {
                return this.idEspece;
            }

            set
            {
                this.idEspece = value;
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

        public List<Race> FindAll()
        {
            List<Race> list = new List<Race>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from race;"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    list.Add(new Race(
                        (int)dr["id_race"],
                        (string)dr["libelle_race"],
                        EnumConverter.ConvertStringToTaille((string)dr["taille_race"]),
                        (int)dr["id_espece"]
                        ));
            }
            return list;
        }

        public List<Race> FindBySelection(string criteres)
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
