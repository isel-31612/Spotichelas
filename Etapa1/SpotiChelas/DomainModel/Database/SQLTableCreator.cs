using SpotiChelas.DomainModel.Data.DBAttributes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace SpotiChelas.DomainModel.Database
{
    class SQLTableCreator
    {
        private SqlConnection con;

        public SQLTableCreator(SqlConnection con)
        {
            this.con = con;
        }

        public static bool ExistsTableNamed(SqlTransaction tran,SqlConnection con, string str)
        {
            var existsCMD = con.CreateCommand();
            existsCMD.CommandText = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='" + str + "'";
            existsCMD.Transaction = tran;
            SqlDataReader reader = existsCMD.ExecuteReader();
            bool exists = reader.HasRows;
            reader.Close();
            return exists;
        }

        public void createTable(SqlTransaction tran,Type t)
        {
            string tableName = t.Name;
            MemberInfo[] fields = getAllDatabaseFields(t);
            StringBuilder str = new StringBuilder("CREATE TABLE "); str.Append(tableName).Append("(");
            for (int i = 0; i < fields.Length-1;i++ )
            {
                AppendMemberData(fields[i], ref str);
                str.Append(",");
            }
            AppendMemberData(fields[fields.Length - 1], ref str);
            str.Append(")");
            var createTableCMD = new SqlCommand(str.ToString(),con);
            createTableCMD.Transaction = tran;
            createTableCMD.ExecuteNonQuery();
        }

        private MemberInfo[] getAllDatabaseFields(Type t)
        {
            Queue<MemberInfo> result = new Queue<MemberInfo>();
            MemberInfo[] members = t.GetMembers();
            foreach (MemberInfo m in members)
            {
                if (m.GetCustomAttribute(typeof(DBField)) != null)
                    result.Enqueue(m);
            }
            return result.ToArray();
        }

        private void AppendMemberData<T>(T mi, ref StringBuilder str) where T : MemberInfo
        {
            string fieldName = mi.Name; str.Append(fieldName).Append(" ");
            Type fieldType;
            string fieldTypeName;
            SqlTypeBinder binder = SqlTypeBinder.get();
            if (mi is FieldInfo)
            {
                FieldInfo fi = mi as FieldInfo;
                fieldType = fi.FieldType; // used to define a type (INT/NVARCHAR/ETC)
            }
            else
            {
                MethodInfo mii = mi as MethodInfo;
                fieldType = mii.ReturnParameter.ParameterType;
            }
            fieldTypeName = binder.getTypeFor(fieldType);
            str.Append(fieldTypeName).Append(" ");
            bool primaryKey = mi.GetCustomAttribute(typeof(DBPrimaryKey)) != null; if (primaryKey) str.Append(" PRIMARY KEY");
            bool notNull = mi.GetCustomAttribute(typeof(DBNotNull)) != null; if (notNull) str.Append(" NOT NULL");
            bool identity = mi.GetCustomAttribute(typeof(DBIdentity)) != null; if (identity) str.Append(" IDENTITY");
            DBForeignKey foreignKey = mi.GetCustomAttribute(typeof(DBForeignKey)) as DBForeignKey;
            if (foreignKey != null)
            {
                string refTable = foreignKey.table;
                string refField = foreignKey.field;
                str.Append(" REFERENCES ").Append(refTable).Append("(").Append(refField).Append(")");
            }
        }
    }
}
