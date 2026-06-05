using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FondationBB_ARDIET_BUJOR.Model
{
    public class DataAccess
    {
        private static string connectionString;
        private static NpgsqlConnection connection;




        static DataAccess()
        {
        }

        public static bool TryConnect(string login, string mdp)
        {
            connectionString = "Host=srv-peda-new;Port=5433;Username="+login+";Password="+mdp+";Database=ardiet_bujor_s201;Options='-c search_path=ardiet_bujor_s201'";
            try
            {
                connection = new NpgsqlConnection(connectionString);
                connection = GetConnection();
                return true;
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb à la connexion  \n");
                return false;
            }
        }

        public static NpgsqlConnection GetConnection()
        {

            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)

                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    LogError.Log(ex, "Pb à la connexion  \n");
                    throw;
                }

            return connection;
        }


        public static DataTable ExecuteSelect(NpgsqlCommand cmd)
        {
            DataTable dataTable = new DataTable();
            try
            {
                cmd.Connection = GetConnection();
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de executeSelect \n" + cmd.CommandText);
                throw;
            }

            return dataTable;
        }

        public static int ExecuteInsert(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();
                nb = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de executeInsert \n" + cmd.CommandText);
                throw;
            }
            return nb;

        }



        public static int ExecuteSet(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();
                nb = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de executeSet \n" + cmd.CommandText);
                throw;
            }
            return nb;

        }

        public static string ExecuteSelectOneValue(NpgsqlCommand cmd)
        {
            object res = null;
            try
            {
                cmd.Connection = GetConnection();
                res = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de ExecuteSelectOneValue \n" + cmd.CommandText);
                throw;
            }
            return res.ToString();

        }

        public static void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
