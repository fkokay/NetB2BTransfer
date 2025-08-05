using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Data
{
    public static class DataReader
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

        public static string GetExecuteScalar(string connectionString, string query, ref string errorMessage)
        {
            using (var connect = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(query, connect) { CommandTimeout = 9999999 })
                {
                    connect.Open();
                    var returnValue = cmd.ExecuteScalar() + "";
                    return !string.IsNullOrEmpty(returnValue) ? returnValue : string.Empty;
                }
            }
        }

        public static int GetExecuteScalarToInt(string connectionString, string query, ref string errorMessage)
        {
            using (var connect = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(query, connect) { CommandTimeout = 9999999 })
                {
                    connect.Open();
                    object value = cmd.ExecuteScalar();
                    return int.TryParse(value?.ToString(), out int result) ? result : 0;
                }
            }
        }

        public static int ExecuteNonQuery(string connectionString, string query, ref string errorMessage)
        {
            using (var connect = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(query, connect) { CommandTimeout = 9999999 })
                {
                    connect.Open();
                    object value = cmd.ExecuteNonQuery();
                    return int.TryParse(value?.ToString(), out int result) ? result : 0;
                }
            }
        }

        public static async Task<DataTable> GetDbDataTable(string connectionString, string query)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand sorgu = new SqlCommand(query, connection))
                {
                    sorgu.CommandTimeout = 500;

                    using (SqlDataReader reader = await sorgu.ExecuteReaderAsync())
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            return dataTable;
        }

    }
}
