using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public enum Sexe
    {
        Male,
        Femelle
    }
    public class Animal
    {
        private int id;
        private string nom;
        private DateTime dateNaissance;
        private string icad;
        private Sexe unSexe;
        private string annotation;
        private DateTime dateArrivee;
        private double poids;
        private Race uneRace;
        private Employe employeCreateur;
        private Statut unStatut;
        private Etat unEtat;
        private List<Comportement> comportements;
        private List<Soin> soinReçus;

        public Animal()
        {
            this.Comportements = new List<Comportement>();
            this.SoinReçus = new List<Soin>();
        }

        public Animal(int id, string nom, DateTime dateNaissance, string icad, Sexe unSexe, string annotation, DateTime dateArrivee, double poids, Race uneRace, Employe employeCreateur,
            Statut unStatut, Etat unEtat, List<Comportement> comportements, List<Soin> soinReçus)
        {
            this.Id = id;
            this.Nom = nom;
            this.DateNaissance = dateNaissance;
            this.Icad = icad;
            this.UnSexe = unSexe;
            this.Annotation = annotation;
            this.DateArrivee = dateArrivee;
            this.Poids = poids;
            this.UneRace = uneRace;
            this.EmployeCreateur = employeCreateur;
            this.UnStatut = unStatut;
            this.UnEtat = unEtat;
            this.Comportements = comportements;
            this.SoinReçus = soinReçus;
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

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public DateTime DateNaissance
        {
            get
            {
                return this.dateNaissance;
            }

            set
            {
                this.dateNaissance = value;
            }
        }

        public string Icad
        {
            get
            {
                return this.icad;
            }

            set
            {
                this.icad = value;
            }
        }

        public Sexe UnSexe
        {
            get
            {
                return this.unSexe;
            }

            set
            {
                this.unSexe = value;
            }
        }

        public string Annotation
        {
            get
            {
                return this.annotation;
            }

            set
            {
                this.annotation = value;
            }
        }

        public DateTime DateArrivee
        {
            get
            {
                return this.dateArrivee;
            }

            set
            {
                this.dateArrivee = value;
            }
        }

        public double Poids
        {
            get
            {
                return this.poids;
            }

            set
            {
                this.poids = value;
            }
        }

        public Employe EmployeCreateur
        {
            get
            {
                return this.employeCreateur;
            }

            set
            {
                this.employeCreateur = value;
            }
        }

        public Statut UnStatut
        {
            get
            {
                return this.unStatut;
            }

            set
            {
                this.unStatut = value;
            }
        }

        public Etat UnEtat
        {
            get
            {
                return this.unEtat;
            }

            set
            {
                this.unEtat = value;
            }
        }

        public List<Comportement> Comportements
        {
            get
            {
                return this.comportements;
            }

            set
            {
                this.comportements = value;
            }
        }

        public List<Soin> SoinReçus
        {
            get
            {
                return this.soinReçus;
            }

            set
            {
                this.soinReçus = value;
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
    }
}
