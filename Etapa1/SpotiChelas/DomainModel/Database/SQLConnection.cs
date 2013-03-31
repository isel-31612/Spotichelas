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
        }

        public void disconnect()
        {
            con.Close();
        }

        public void executeCommand(string str, bool query=false)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = str;
            if (!query)
                cmd.ExecuteNonQuery();
            else
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    IDataRecord dr = reader;
                    Console.WriteLine(string.Format("{0}",dr[1]));
                }
            }

        }

        public void createTableFor(Type t)
        {
            string tableName = t.Name;
            var testTableCMD = con.CreateCommand();
            testTableCMD.CommandText = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='"+tableName+"'";
            if (testTableCMD.ExecuteReader().HasRows)
                return;//ja existe tabela
            MemberInfo[] fields = getAllDatabaseFields(t);
            string s = "CREATE TABLE " + tableName + "(";
            foreach (FieldInfo f in fields)
            {
                AppendMemberData(f, ref s); s += ",";
            }
            s += ")";
            var createTableCMD = con.CreateCommand();
            createTableCMD.CommandText = s;
            createTableCMD.ExecuteNonQuery();
        }

        private MemberInfo[] getAllDatabaseFields(Type t)
        {
            Queue<MemberInfo> result = new Queue<MemberInfo>();
            MemberInfo[] members = t.GetMembers();
            foreach (MemberInfo m in members)
            {
                if(m.GetCustomAttribute(typeof(DBField))!=null)
                    result.Enqueue(m);
            }
            return result.ToArray();
        }

        private void AppendMemberData<T>(T mi, ref string str) where T : MemberInfo
        {
            string fieldName = mi.Name; str += fieldName + " ";
            string fieldType;
            if (mi is FieldInfo)
            {
                FieldInfo fi = mi as FieldInfo;
                fieldType = fi.FieldType.Name; // used to define a type (INT/NVARCHAR/ETC)
            }
            else
            {
                MethodInfo mii = mi as MethodInfo;
                fieldType = mii.ReturnParameter.ParameterType.Name;
            }
            /*Naive <3*/ str += fieldType + " ";
            bool primaryKey = mi.GetCustomAttribute(typeof(DBPrimaryKey)) != null; if (primaryKey) str += " PRIMARY KEY";
            bool notNull = mi.GetCustomAttribute(typeof(DBNotNull)) != null; if (notNull) str += " NOT NULL";
            bool identity = mi.GetCustomAttribute(typeof(DBIdentity)) != null; if (identity) str += " IDENTITY";
            DBForeignKey foreignKey = mi.GetCustomAttribute(typeof(DBForeignKey)) as DBForeignKey;
            if (foreignKey != null)
            {
                string refTable = foreignKey.table;
                string refField = foreignKey.field;
                str += " REFERENCES "+ refTable +"("+ refField +")";
            }
        }


        public static void Main()
        {
            Console.WriteLine("----STARTED----");
            DatabaseConnection db = new DatabaseConnection();
            db.connect();
            Console.WriteLine("----Connected!...----");
            //db.executeCommand("CREATE TABLE Musicas( id INT PRIMARY KEY, name NVARCHAR(20), duration INT NOT NULL)",false);
            //Console.WriteLine("----Tables created!...----");
            db.executeCommand("SELECT OBJECT_ID('Musicas') IS NOT NULL",true);
            //db.executeCommand("INSERT INTO Musicas VALUES(1,'Lost in the Echo',200)");
            db.executeCommand("INSERT INTO Musicas VALUES(2,'Lies Misery Greed',220)");
            db.executeCommand("INSERT INTO Musicas VALUES(3,'Burn it Down',314)");
            db.executeCommand("SELECT * FROM Musicas",true);
            Console.WriteLine("----Disconnecting...----");
            db.disconnect();
            Console.WriteLine("----Waiting user input...----");
            Console.ReadKey();
            Console.WriteLine("----FINISHED----");
        }
    }

}
