using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Statut
    {
        private int id;
        private string libelle;

        public Statut()
        {
        }

        public Statut(int id, string libelle)
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
    }
}
