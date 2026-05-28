using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Recoit
    {
        private Soin unSoin;
        private int idSoin;
        private Animal unAnimal;
        private int idAnimal;
        private DateTime dateSoin;
        private DateTime? dateRappel;

        public Recoit()
        {
        }

        public Recoit(Soin unSoin, Animal unAnimal, DateTime dateSoin, DateTime? dateRappel)
        {
            this.UnSoin = unSoin;
            this.UnAnimal = unAnimal;
            this.DateSoin = dateSoin;
            this.DateRappel = dateRappel;
        }

        public Recoit(int idSoin, int idAnimal, DateTime dateSoin, DateTime? dateRappel)
        {
            this.IdSoin = idSoin;
            this.IdAnimal = idAnimal;
            this.DateSoin = dateSoin;
            this.DateRappel = dateRappel;
        }

        public Recoit(Soin unSoin, Animal unAnimal, DateTime dateSoin)
        {
            this.dateRappel = dateRappel;
            this.UnSoin = unSoin;
            this.UnAnimal = unAnimal;
            this.DateSoin = dateSoin;
        }

        public Recoit(int idSoin, int idAnimal, DateTime dateSoin)
        {
            this.IdSoin = idSoin;
            this.IdAnimal = idAnimal;
            this.DateSoin = dateSoin;
        }

        public Soin UnSoin
        {
            get
            {
                return this.unSoin;
            }

            set
            {
                this.unSoin = value;
            }
        }

        public int IdSoin
        {
            get
            {
                return this.idSoin;
            }

            set
            {
                this.idSoin = value;
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

        public DateTime DateSoin
        {
            get
            {
                return this.dateSoin;
            }

            set
            {
                this.dateSoin = value;
            }
        }

        public DateTime? DateRappel
        {
            get
            {
                return this.dateRappel;
            }

            set
            {
                this.dateRappel = value;
            }
        }
    }
}
