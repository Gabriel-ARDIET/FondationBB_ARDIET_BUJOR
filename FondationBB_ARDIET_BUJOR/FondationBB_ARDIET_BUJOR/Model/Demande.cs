using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
        Sénior

    }
    public class Demande : ICrud<Demande>
    {
        private int id;
        private DateTime dateDemande;
        private TrancheAge uneTrancheAge;
        private Race? uneRace;
        private int? idRace;
        private Personne unePersonne;
        private int? idPersonne;

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
            this.IdRace = uneRace.Id;
            this.IdPersonne = unePersonne.Id;
        }
        public Demande(int id, DateTime dateDemande, TrancheAge uneTrancheAge, int? idRace, int? idPersonne)
        {
            this.Id = id;
            this.DateDemande = dateDemande;
            this.UneTrancheAge = uneTrancheAge;
            this.IdRace = idRace;
            this.IdPersonne = idPersonne;
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

        public int? IdRace
        {
            get
            {
                return this.idRace;
            }

            set
            {
                this.idRace = value;
            }
        }

        public int? IdPersonne
        {
            get
            {
                return this.idPersonne;
            }

            set
            {
                this.idPersonne = value;
            }
        }

        public int Create()
        {
            string sql = "INSERT INTO demande (date_demande, tranche_age_demande, id_race, id_personne) " +
                         "VALUES (@date, @tranche, @idRace, @idPersonne) RETURNING id_demande;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@date", DateOnly.FromDateTime(this.DateDemande));
                cmd.Parameters.AddWithValue("@tranche", EnumConverter.ConvertTrancheAgeToString(this.UneTrancheAge));

                cmd.Parameters.AddWithValue("@idRace", this.UneRace != null ? this.UneRace.Id : DBNull.Value);
                cmd.Parameters.AddWithValue("@idPersonne", this.UnePersonne.Id);
                this.Id = DataAccess.ExecuteInsert(cmd);

                return this.Id > 0 ? 1 : 0;
            }
        }

        public int Delete()
        {
            string sql = "DELETE FROM demande WHERE id_demande = @id;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", this.Id);

                return DataAccess.ExecuteSet(cmd);
            }
        }

        public List<Demande> FindAll()
        {
            List<Demande> list = new List<Demande>();
            string sql = "SELECT * FROM demande;";

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(sql))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Demande d = new Demande();
                    d.Id = (int)dr["id_demande"];
                    d.DateDemande = new DateTime((DateOnly)dr["date_demande"], TimeOnly.MinValue);

                    d.UneTrancheAge = EnumConverter.ConvertStringToTrancheAge((string)dr["tranche_age_demande"]);

                    d.IdRace = (int)dr["id_race"];
                    d.IdPersonne = (int)dr["id_personne"];

                    list.Add(d);
                }
            }
            return list;
        }

        public List<Demande> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            string sql = "UPDATE demande SET date_demande = @date, tranche_age_demande = @tranche, " +
                         "id_race = @idRace, id_personne = @idPersonne " +
                         "WHERE id_demande = @id;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", this.Id);
                cmd.Parameters.AddWithValue("@date", DateOnly.FromDateTime(this.DateDemande));
                cmd.Parameters.AddWithValue("@tranche", EnumConverter.ConvertTrancheAgeToString(this.UneTrancheAge));
                cmd.Parameters.AddWithValue("@idRace", this.UneRace != null ? this.UneRace.Id : DBNull.Value);
                cmd.Parameters.AddWithValue("@idPersonne", this.UnePersonne.Id);

                return DataAccess.ExecuteSet(cmd);
            }
        }
    }
}
