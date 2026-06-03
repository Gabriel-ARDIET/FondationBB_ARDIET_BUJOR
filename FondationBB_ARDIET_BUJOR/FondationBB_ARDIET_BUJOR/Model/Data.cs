using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Data
    {
        private ObservableCollection<Personne> lesPersonnes;
        private ObservableCollection<Animal> lesAnimaux;
        private ObservableCollection<Adoption> lesAdoptions;

        public Data()
        {
            LesPersonnes = new ObservableCollection<Personne>();
        }

        public ObservableCollection<Personne> LesPersonnes
        {
            get
            {
                return this.lesPersonnes;
            }

            set
            {
                this.lesPersonnes = value;
            }
        }

        public ObservableCollection<Adoption> LesAdoptions
        {
            get
            {
                return this.lesAdoptions;
            }

            set
            {
                this.lesAdoptions = value;
            }
        }

        public void ChargerPersonnes()
        {
            if (LesPersonnes.Count != 0)
                return;
            LesPersonnes = new ObservableCollection<Personne>(new Personne().FindAll());
        }
        public void ChargerAdoptions()
        {
            if (LesAdoptions.Count != 0)
                return;
            LesAdoptions = new ObservableCollection<Adoption>(new Adoption().FindAll());
        }
    }
}
