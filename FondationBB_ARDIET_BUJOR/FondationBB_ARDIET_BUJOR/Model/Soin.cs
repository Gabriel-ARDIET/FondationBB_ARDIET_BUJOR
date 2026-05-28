using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Soin
    {
        private int id;
        private string libelle;
        private decimal tarif;
        private int frequence;

        public Soin()
        {
        }

        public Soin(int id, string libelle, decimal tarif, int frequence)
        {
            this.Id = id;
            this.Libelle = libelle;
            this.Tarif = tarif;
            this.Frequence = frequence;
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
                if (value.Length > 50)
                    throw new ArgumentOutOfRangeException("Le libéllé doit faire moins de 50 caractères");
                this.libelle = value;
            }
        }

        public decimal Tarif
        {
            get
            {
                return this.tarif;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("le tarif ne peut pas être négatif");
                if (value >= 1000000)
                    throw new ArgumentOutOfRangeException("le tarif doit être inférieur à 1 000 000");
                this.tarif = value;
            }
        }

        public int Frequence
        {
            get
            {
                return this.frequence;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("la fréquence ne peut pas être négatif");
                this.frequence = value;
            }
        }
    }
}
