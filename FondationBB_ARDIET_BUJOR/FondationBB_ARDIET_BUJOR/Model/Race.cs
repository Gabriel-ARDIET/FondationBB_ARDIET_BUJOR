using System;
using System.Collections.Generic;
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
    }
}
