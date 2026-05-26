using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Adoption
    {
        private int id;
        private decimal frais;
        private DateTime dateAdoption;
        private Employe createur;
        private Personne adoptant;
        private Animal unAnimal;

        public Adoption()
        {
        }

        public Adoption(int id, decimal frais, DateTime dateAdoption, Employe createur, Personne adoptant, Animal unAnimal)
        {
            this.Id = id;
            this.Frais = frais;
            this.DateAdoption = dateAdoption;
            this.Createur = createur;
            this.Adoptant = adoptant;
            this.UnAnimal = unAnimal;
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

        public decimal Frais
        {
            get
            {
                return this.frais;
            }

            set
            {
                this.frais = value;
            }
        }

        public DateTime DateAdoption
        {
            get
            {
                return this.dateAdoption;
            }

            set
            {
                this.dateAdoption = value;
            }
        }

        public Employe Createur
        {
            get
            {
                return this.createur;
            }

            set
            {
                this.createur = value;
            }
        }

        public Personne Adoptant
        {
            get
            {
                return this.adoptant;
            }

            set
            {
                this.adoptant = value;
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
    }
}
