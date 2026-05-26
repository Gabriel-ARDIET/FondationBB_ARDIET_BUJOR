using System;
using System.Collections.Generic;
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
                this.libelle = value;
            }
        }
    }
}
