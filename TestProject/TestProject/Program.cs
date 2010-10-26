using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = ConfigurationManager.ConnectionStrings["TempConnString"].ConnectionString;
            DbResult result = new DbResult();
            int tempInt = 0;
            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "select TableID, SomeValue1, SomeValue2 from someTable";
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Int32.TryParse(reader["TableID"].ToString(), out tempInt);
                            result.TableID = tempInt;
                            result.SomeValue1 = reader["SomeValue1"].ToString();
                            result.SomeValue2 = reader["SomeValue2"].ToString();
                        }
                        reader.Close();
                    }
                }
                conn.Close();
            }
        }
    }
}
