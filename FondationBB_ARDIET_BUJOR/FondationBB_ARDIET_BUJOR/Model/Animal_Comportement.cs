using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Animal_Comportement : ICrud<Animal_Comportement>
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
            this.IdComportement = unComportement.Id;
            this.UnAnimal = unAnimal;
            this.IdAnimal = unAnimal.Id;
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

        public int Create()
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public List<Animal_Comportement> FindAll()
        {
            {
                List<Animal_Comportement> list = new List<Animal_Comportement>();
                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from animal_comportement;"))
                {
                    DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                    foreach (DataRow dr in dt.Rows)
                        list.Add(new Animal_Comportement(
                            (int)dr["id_comportement"],
                            (int)dr["id_animal"]
                            ));
                }
                return list;
            }
        }

        public List<Animal_Comportement> FindBySelection(string criteres)
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
