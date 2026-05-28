using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Animal_Comportement
    {
        private Comportement unComportement;
        private Animal unAnimal;
        private int idComportement;
        private int idAnimal;

        public Animal_Comportement()
        {
        }

        public Animal_Comportement(Comportement unComportement, Animal unAnimal)
        {
            this.UnComportement = unComportement;
            this.UnAnimal = unAnimal;
        }

        public Animal_Comportement(int idComportement, int idAnimal)
        {
            this.IdComportement = idComportement;
            this.IdAnimal = idAnimal;
        }

        public Comportement UnComportement
        {
            get
            {
                return this.unComportement;
            }

            set
            {
                this.unComportement = value;
            }
        }

        public Animal UnAnimal
        {
            get
            {
                return this.unAnimal;
            }

            set
            {
                this.unAnimal = value;
            }
        }

        public int IdComportement
        {
            get
            {
                return this.idComportement;
            }

            set
            {
                this.idComportement = value;
            }
        }

        public int IdAnimal
        {
            get
            {
                return this.idAnimal;
            }

            set
            {
                this.idAnimal = value;
            }
        }
    }
}
