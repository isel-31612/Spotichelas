using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using Entities;
using Entities.DBAttributes;

namespace DataAccess
{
    class SQLTableCreator
    {
        private SqlConnection con;

        public SQLTableCreator(SqlConnection con)
        {
            this.con = con;
        }

        public static bool ExistsTableNamed(SqlCommand cmd, string str)
        {
            cmd.CommandText = new StringBuilder("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='").Append(str).Append("'").ToString();
            SqlDataReader reader = cmd.ExecuteReader();
            bool exists = reader.HasRows;
            reader.Close();
            return exists;
        }

        public void createTable(SqlCommand cmd, Type t)
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
            cmd.CommandText= str.ToString();
            cmd.ExecuteNonQuery();
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
