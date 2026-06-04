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
        private ObservableCollection<Employe> lesEmployes;
        private ObservableCollection<Etat> lesEtats;
        private ObservableCollection<Statut> lesStatuts;
        private ObservableCollection<Race> lesRaces;
        private ObservableCollection<Espece> lesEspeces;
        private ObservableCollection<Soin> lesSoins;
        private ObservableCollection<Recoit> lesSoinsReçus;
        private ObservableCollection<Animal_Comportement> lesComportementsDesAnimaux;
        private ObservableCollection<Comportement> lesComportements;

        public Data()
        {
            LesPersonnes = new ObservableCollection<Personne>();
            LesAnimaux = new ObservableCollection<Animal>();
            LesAdoptions = new ObservableCollection<Adoption>();
            LesEmployes = new ObservableCollection<Employe>();
            LesEtats = new ObservableCollection<Etat>();
            LesStatuts = new ObservableCollection<Statut>();
            LesRaces = new ObservableCollection<Race>();
            LesEspeces = new ObservableCollection<Espece>();
            LesSoinsReçus = new ObservableCollection<Recoit>();
            LesSoins = new ObservableCollection<Soin>();
            LesComportementsDesAnimaux = new ObservableCollection<Animal_Comportement>();
            LesComportements = new ObservableCollection<Comportement>();
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
        public ObservableCollection<Animal> LesAnimaux
        {
            get
            {
                return this.lesAnimaux;
            }
            set
            {
                this.lesAnimaux = value;
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

        public ObservableCollection<Employe> LesEmployes
        {
            get
            {
                return this.lesEmployes;
            }

            set
            {
                this.lesEmployes = value;
            }
        }

        public ObservableCollection<Etat> LesEtats
        {
            get
            {
                return this.lesEtats;
            }

            set
            {
                this.lesEtats = value;
            }
        }

        public ObservableCollection<Statut> LesStatuts
        {
            get
            {
                return this.lesStatuts;
            }

            set
            {
                this.lesStatuts = value;
            }
        }

        public ObservableCollection<Race> LesRaces
        {
            get
            {
                return this.lesRaces;
            }

            set
            {
                this.lesRaces = value;
            }
        }

        public ObservableCollection<Espece> LesEspeces
        {
            get
            {
                return this.lesEspeces;
            }

            set
            {
                this.lesEspeces = value;
            }
        }

        public ObservableCollection<Recoit> LesSoinsReçus
        {
            get
            {
                return this.lesSoinsReçus;
            }

            set
            {
                this.lesSoinsReçus = value;
            }
        }

        public ObservableCollection<Soin> LesSoins
        {
            get
            {
                return this.lesSoins;
            }

            set
            {
                this.lesSoins = value;
            }
        }

        public ObservableCollection<Animal_Comportement> LesComportementsDesAnimaux
        {
            get
            {
                return this.lesComportementsDesAnimaux;
            }

            set
            {
                this.lesComportementsDesAnimaux = value;
            }
        }

        public ObservableCollection<Comportement> LesComportements
        {
            get
            {
                return this.lesComportements;
            }

            set
            {
                this.lesComportements = value;
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
            ChargerAdoptions();
            ChargerAnimaux();
            foreach (Adoption a in LesAdoptions)
            {
                a.UnAnimal = LesAnimaux.FirstOrDefault(animal => animal.Id == a.IdAnimal);
                a.Adoptant = LesPersonnes.FirstOrDefault(personne => personne.Id == a.IdAdoptant);
                a.Createur = LesEmployes.FirstOrDefault(employe => employe.Id == a.IdCreateur);
            }
        }
        public void ChargerAnimaux()
        {
            if (LesAnimaux.Count != 0)
                return;
            LesAnimaux = new ObservableCollection<Animal>(new Animal().FindAll());
            ChargerEmployes();
            ChargerRaces();
            ChargerComportementsDesAnimaux();
            ChargerSoinsReçus();
            ChargerStatuts();
            ChargerEtats();
            foreach (Animal a in LesAnimaux)
            {
                a.EmployeCreateur = LesEmployes.FirstOrDefault(employe => employe.Id == a.IdCreateur);
                a.UneRace = LesRaces.FirstOrDefault(race => race.Id == a.IdRace);
                a.UnStatut = LesStatuts.FirstOrDefault(statut => statut.Id == a.IdStatut);
                a.UnEtat = LesEtats.FirstOrDefault(etat => etat.Id == a.IdEtat);
            }
        }
        public void ChargerEmployes()
        {
            if (LesEmployes.Count != 0)
                return;
            LesEmployes = new ObservableCollection<Employe>(new Employe().FindAll());
        }
        public void ChargerEtats()
        {
            if (LesEtats.Count != 0)
                return;
            LesEtats = new ObservableCollection<Etat>(new Etat().FindAll());
        }
        public void ChargerStatuts()
        {
            if (LesStatuts.Count != 0)
                return;
            LesStatuts = new ObservableCollection<Statut>(new Statut().FindAll());
        }
        public void ChargerRaces()
        {
            if (LesRaces.Count != 0)
                return;
            LesRaces = new ObservableCollection<Race>(new Race().FindAll());
            ChargerEspeces();
            foreach (Race r in LesRaces)
            {
                r.UneEspece = LesEspeces.FirstOrDefault(espece => espece.Id == r.IdEspece);
            }
        }
        public void ChargerEspeces()
        {
            if (LesEspeces.Count != 0)
                return;
            LesEspeces = new ObservableCollection<Espece>(new Espece().FindAll());
        }
        public void ChargerSoinsReçus()
        {
            if (LesSoinsReçus.Count != 0)
                return;
            LesSoinsReçus = new ObservableCollection<Recoit>(new Recoit().FindAll());
            foreach (Recoit r in LesSoinsReçus)
            {
                r.UnSoin = LesSoins.FirstOrDefault(soin => soin.Id == r.IdSoin);
                r.UnAnimal = LesAnimaux.FirstOrDefault(animal => animal.Id == r.IdAnimal);
            }
        }
        public void ChargerSoins()
        {
            if (LesSoins.Count != 0)
                return;
            LesSoins = new ObservableCollection<Soin>(new Soin().FindAll());
            ChargerAnimaux();
            ChargerSoins();
            foreach (Recoit r in LesSoinsReçus)
            {
                r.UnSoin = LesSoins.FirstOrDefault(soin => soin.Id == r.IdSoin);
                r.UnAnimal = LesAnimaux.FirstOrDefault(animal => animal.Id == r.IdAnimal);
            }
        }
        public void ChargerComportementsDesAnimaux()
        {
            if (LesComportementsDesAnimaux.Count != 0)
                return;
            LesComportementsDesAnimaux = new ObservableCollection<Animal_Comportement>(new Animal_Comportement().FindAll());
            ChargerAnimaux();
            ChargerComportements();
            foreach (Animal_Comportement ac in LesComportementsDesAnimaux)
            {
                ac.UnAnimal = LesAnimaux.FirstOrDefault(animal => animal.Id == ac.IdAnimal);
                ac.UnComportement = LesComportements.FirstOrDefault(comportement => comportement.Id == ac.IdComportement);
            }
        }
        public void ChargerComportements()
        {
            if (LesComportements.Count != 0)
                return;
            LesComportements = new ObservableCollection<Comportement>(new Comportement().FindAll());
        }
    }
}
