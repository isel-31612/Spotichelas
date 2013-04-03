using SpotiChelas.DomainModel.Data;
using SpotiChelas.DomainModel.Data.DBAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Database
{
    public class DatabaseConnection
    {
        private SqlConnection con = null;
        private SqlTransaction tran = null;

        public DatabaseConnection()
        {
            con = new SqlConnection();
            con.ConnectionString = "Initial Catalog=SpotiChelasDB;Data Source=(localdb)\\Projects";
        }

        public DatabaseConnection(string connectString)
        {
            con = new SqlConnection();
            con.ConnectionString = connectString;
        }

        public void connect()
        {
            con.Open();
            tran = con.BeginTransaction();
        }

        public void disconnect()
        {
            if (tran != null)
                tran.Rollback();
            con.Close();
        }

        public void AddStuff(Track t)
        {
            var cmd = con.CreateCommand();
            cmd.Transaction = tran;
            SQLTableCreator tc = new SQLTableCreator(con);
            if (!SQLTableCreator.ExistsTableNamed(cmd, t.GetType().Name))
                tc.createTable(cmd,typeof(Track));
            cmd.CommandText = "INSERT INTO "+t.GetType().Name+" (getId,_name,_duration) VALUES("+t.getId()+",'" + t.Name + "'," + t.Duration + ")";
            cmd.ExecuteNonQuery();
        }

        public void GetStuff(Type t)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM " + t.Name;
            cmd.Transaction = tran;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                IDataRecord dr = reader;
                Console.WriteLine(string.Format("{0}, {1}, {2}", dr[0],dr[1],dr[2]));
            }
            reader.Close();
        }

        public void Execute() {

            SqlCommand cmd = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.TABLES",con);
            var reader = cmd.ExecuteReader();
            Console.WriteLine(reader.VisibleFieldCount);
            while (reader.Read())
            {
                IDataRecord dr = reader;
                for (int i = 0; i < reader.VisibleFieldCount;i++)
                {
                    Console.WriteLine(dr[i]);
                }
            }
            reader.Close();
        }

        public static void Main()
        {
            Console.WriteLine("----STARTED----");
            DatabaseConnection db = new DatabaseConnection();
            db.connect();
            Console.WriteLine("----Connected!...----");
            //db.Execute();
            db.AddStuff(new Track("Breaking the Habit",300));
            db.GetStuff(typeof(Track));
            Console.WriteLine("----Disconnecting...----");
            db.disconnect();
            Console.WriteLine("----Waiting user input...----");
            Console.ReadKey();
            Console.WriteLine("----FINISHED----");
        }
    }

}
