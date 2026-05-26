using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public enum TrancheAge
    {
        Bébé,
        Jeune,
        Adulte,
        Vieux

    }
    public class Demande
    {
        private int id;
        private DateTime dateDemande;
        private TrancheAge uneTrancheAge;
        private Race? uneRace;
        private Personne unePersonne;

        public Demande()
        {
        }

        public Demande(int id, DateTime dateDemande, TrancheAge uneTrancheAge, Race uneRace, Personne unePersonne)
        {
            this.Id = id;
            this.DateDemande = dateDemande;
            this.UneTrancheAge = uneTrancheAge;
            this.UneRace = uneRace;
            this.UnePersonne = unePersonne;
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

        public DateTime DateDemande
        {
            get
            {
                return this.dateDemande;
            }

            set
            {
                this.dateDemande = value;
            }
        }

        public TrancheAge UneTrancheAge
        {
            get
            {
                return this.uneTrancheAge;
            }

            set
            {
                this.uneTrancheAge = value;
            }
        }

        public Race UneRace
        {
            get
            {
                return this.uneRace;
            }

            set
            {
                this.uneRace = value;
            }
        }

        public Personne UnePersonne
        {
            get
            {
                return this.unePersonne;
            }

            set
            {
                this.unePersonne = value;
            }
        }
    }
}
