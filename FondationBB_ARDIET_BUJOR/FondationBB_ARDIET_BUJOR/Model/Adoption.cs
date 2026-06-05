using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Adoption : ICrud<Adoption>
    {
        private int id;
        private decimal frais;
        private DateTime dateAdoption;
        private Employe createur;
        private Personne adoptant;
        private Animal unAnimal;
        private int idCreateur;
        private int idAdoptant;
        private int idAnimal;

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

        public Adoption(int id, decimal frais, DateTime dateAdoption, int idCreateur, int idAdoptant, int idAnimal)
        {
            this.Id = id;
            this.Frais = frais;
            this.DateAdoption = dateAdoption;
            this.IdCreateur = idCreateur;
            this.IdAdoptant = idAdoptant;
            this.IdAnimal = idAnimal;
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
                if (value >= 1000000)
                    throw new ArgumentOutOfRangeException("le tarif doit être inférieur à 1 000 000");
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Le tarif ne peut pas être négatif");
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

        public int IdCreateur
        {
            get
            {
                return this.idCreateur;
            }

            set
            {
                this.idCreateur = value;
            }
        }

        public int IdAdoptant
        {
            get
            {
                return this.idAdoptant;
            }

            set
            {
                this.idAdoptant = value;
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
            int nb = 0;

            string sql = "INSERT INTO adoption (frais_adoption, date_adoption, id_employe, id_personne, id_animal) " +
                         "VALUES (@frais, @date, @idEmploye, @idPersonne, @idAnimal) RETURNING id_adoption;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@frais", this.Frais);
                cmd.Parameters.AddWithValue("@date", DateOnly.FromDateTime(this.DateAdoption));
                cmd.Parameters.AddWithValue("@idEmploye", this.IdCreateur);
                cmd.Parameters.AddWithValue("@idPersonne", this.IdAdoptant);
                cmd.Parameters.AddWithValue("@idAnimal", this.IdAnimal);

                nb = DataAccess.ExecuteInsert(cmd);
            }
            this.Id = nb;
            return nb;
        }

        public int Delete()
        {
            string sql = "DELETE FROM adoption WHERE id_adoption = @id;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", this.Id);

                return DataAccess.ExecuteSet(cmd);
            }
        }

        public List<Adoption> FindAll()
        {
            List<Adoption> list = new List<Adoption>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from adoption;"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    list.Add(new Adoption(
                        (int)dr["id_adoption"],
                        (decimal)dr["frais_adoption"],
                        new DateTime((DateOnly)dr["date_adoption"], TimeOnly.MinValue),
                        (int)dr["id_employe"],
                        (int)dr["id_personne"],
                        (int)dr["id_animal"]
                        ));
            }
            return list;
        }

        public List<Adoption> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            string sql = "UPDATE adoption SET frais_adoption = @frais, date_adoption = @date, " +
                         "id_employe = @idEmploye, id_personne = @idPersonne, id_animal = @idAnimal " +
                         "WHERE id_adoption = @id;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", this.Id);
                cmd.Parameters.AddWithValue("@frais", this.Frais);
                cmd.Parameters.AddWithValue("@date", DateOnly.FromDateTime(this.DateAdoption));
                cmd.Parameters.AddWithValue("@idEmploye", this.IdCreateur);
                cmd.Parameters.AddWithValue("@idPersonne", this.IdAdoptant);
                cmd.Parameters.AddWithValue("@idAnimal", this.IdAnimal);

                return DataAccess.ExecuteSet(cmd);
            }
        }
    }
}
