using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public static class EnumConverter
    {
        public static Sexe ConvertStringToSexe(string value)
        {
            Sexe sexe;
            if (value == "M")
                sexe = Sexe.Male;
            else
                sexe = Sexe.Femelle;
            return sexe;
        }
        public static string ConvertSexeToString(Sexe sexe)
        {
            string value;
            if (sexe == Sexe.Male)
                value = "M";
            else
                value = "F";
            return value;
        }
        public static Taille ConvertStringToTaille(string value)
        {
            Taille taille;
            if (value == "petit")
                taille = Taille.Petit;
            else if (value == "moyen")
                taille = Taille.Moyen;
            else
                taille = Taille.Grand;
            return taille;
        }
        public static string ConvertTailleToString(Taille taille)
        {
            string value;
            if (taille == Taille.Petit)
                value = "petit";
            else if (taille == Taille.Moyen)
                value = "moyen";
            else
                value = "grand";
            return value;
        }
        public static TrancheAge ConvertStringToTrancheAge(string value)
        {
            TrancheAge trancheAge;
            if (value == "Bébé")
                trancheAge = TrancheAge.Bébé;
            else if (value == "Jeune")
                trancheAge = TrancheAge.Jeune;
            else if (value == "Adulte")
                trancheAge = TrancheAge.Adulte;
            else
                trancheAge = TrancheAge.Sénior;
            return trancheAge;
        }
        public static string ConvertTrancheAgeToString(TrancheAge trancheAge)
        {
            string value;
            if (trancheAge == TrancheAge.Bébé)
                value = "Bébé";
            else if (trancheAge == TrancheAge.Jeune)
                value = "Jeune";
            else if (trancheAge == TrancheAge.Adulte)
                value = "Adulte";
            else
                value = "Sénior";
            return value;
        }
        public static Role ConvertStringToRole(string value)
        {
            Role role;
            if (value == "Responsable")
                role = Role.Responsable;
            else
                role = Role.Bénévole;
            return role;
        }
        public static string ConvertRoleToString(Role role)
        {
            string value;
            if (role == Role.Responsable)
                value = "Responsable";
            else
                value = "Bénévole";
            return value;
        }
    }
}
