using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Comportement
    {
        private int id;
        private string libelle;

        public Comportement()
        {
        }

        public Comportement(int id, string libelle) 
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
