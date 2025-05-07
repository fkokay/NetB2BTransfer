using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Data
{
    public class DataReader
    {
        public static List<T> ReadData<T>(string connectionString, string queryString, ref string errorMessage)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
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
