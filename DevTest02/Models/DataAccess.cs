using System.Data.SQLite;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTest02.Models
{
    public class DataAccess
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        public DataAccess(string empPath)
        {
            sql_con = new SQLiteConnection($"Data Source={empPath};Version=3;New=False;Compress=True;");
        }

        public List<Employee> GetDataEmployees()
        {
            sql_con.Open();

            sql_cmd = sql_con.CreateCommand();
            string CommandText = "select * from employees order by bornDate";
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];            
            sql_con.Close();

            var listEmp = (from row in DT.AsEnumerable()
                         select new Employee
                         {                             
                             ID = Convert.ToInt32(row["id"]),
                             Name = row["name"].ToString(),
                             RFC = row["RFC"].ToString(),
                             LastName = row["lastName"].ToString(),
                             BornDate = DateTime.Parse(row["bornDate"].ToString()),
                         }).ToList();

            return listEmp;
        }

        private void ExecuteQuery(string txtQuery)
        {
            sql_con.Open();

            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;

            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }
    }
}