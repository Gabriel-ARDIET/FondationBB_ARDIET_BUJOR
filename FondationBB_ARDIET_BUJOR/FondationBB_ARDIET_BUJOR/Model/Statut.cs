using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class Statut : ICrud<Statut>
    {
        private int id;
        private string libelle;

        public Statut() { }

        public Statut(int id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

        public int Id
        {
            get => this.id;
            set => this.id = value;
        }

        public string Libelle
        {
            get => this.libelle;
            set
            {
                if (value.Length > 30)
                    throw new ArgumentOutOfRangeException("Le libellé doit faire moins de 30 caractères");
                this.libelle = value;
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

        public List<Statut> FindAll()
        {
            {
                List<Statut> list = new List<Statut>();
                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from statut;"))
                {
                    DataTable dt = DataAccess.ExecuteSelect(cmdSelect);
                    foreach (DataRow dr in dt.Rows)
                        list.Add(new Statut(
                            (int)dr["id_statut"],
                            (string)dr["libelle_statut"]
                            ));
                }
                return list;
            }
        }

        public List<Statut> FindBySelection(string criteres)
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
        public override string ToString() => Libelle;
    }
}