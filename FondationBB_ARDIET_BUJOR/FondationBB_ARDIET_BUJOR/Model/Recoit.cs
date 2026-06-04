using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Recoit : ICrud<Recoit>
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
            this.dateRappel = null; // Initialisé à null car absent des paramètres
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

        public int Create()
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

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public List<Recoit> FindAll()
        {
            {
                List<Recoit> list = new List<Recoit>();
                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from recoit;"))
                {
                    DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                    foreach (DataRow dr in dt.Rows)
                        list.Add(new Recoit(
                            (int)dr["id_soin"],
                            (int)dr["id_animal"],
                            new DateTime((DateOnly)dr["date_soin"], TimeOnly.MinValue),
                            dr["date_rappel"] == DBNull.Value ? null : new DateTime((DateOnly)dr["date_rappel"], TimeOnly.MinValue)
                            ));
                }
                return list;
            }
        }

        public List<Recoit> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }
    }
}
