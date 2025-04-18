using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.Core.Data
{
    public static class DataQuery
    {
        public static string GetExecuteScalar(string query, ref string errorMessage)
        {
            using (var connect = new SqlConnection("Data Source=(local);Initial Catalog=FEYZAN2025;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=sapass;Trust Server Certificate=True;"))
            {
                using (var cmd = new SqlCommand(query, connect) { CommandTimeout = 9999999 })
                {
                    connect.Open();
                    var returnValue = cmd.ExecuteScalar() + "";
                    return !string.IsNullOrEmpty(returnValue) ? returnValue : string.Empty;
                }
            }
        }

        public static bool ExecCommandNoReturn(string cmdText, ref string errorMessage)
        {
            using (var connect = new SqlConnection("Data Source=(local);Initial Catalog=FEYZAN2025;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=sapass;Trust Server Certificate=True;"))
            {
                var cmd = new SqlCommand(cmdText, connect);
                try
                {
                    connect.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    errorMessage += ex.Message;
                }
                return true;
            }
        }
    }
}
