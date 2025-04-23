using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.Core.Data
{
    public class DataReader
    {
        public static List<T> ReadData<T>(string queryString, ref string errorMessage)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source=(local);Initial Catalog=ERBABLTD2022;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=sapass;Trust Server Certificate=True;"))
                {
                    return connection.Query<T>(queryString, commandTimeout: 9999999).ToList();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

    }
}
