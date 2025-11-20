using MixRubber2.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MixRubber2
{
    public static class DBHelper
    {
        static public DataTable GetData(string commandText)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=.\;Initial Catalog=MixRubber;trusted_connection=true;encrypt=false;Integrated Security=True"))
                {
                    DbDataAdapter adapter = new SqlDataAdapter(commandText, connection);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка запроса в БД");
                //Console.WriteLine(ex.Message);
            }

            return dt;
        }
    }
}
