using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGPT
{
    public static class DatabaseUtilities
    {
        private static string _connectionString = "YourConnectionStringHere"; // Replace with your connection string

        /// <summary>
        /// Sets the connection string for the database utilities.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Executes a non-query SQL command asynchronously.
        /// </summary>
        /// <param name="commandText">The SQL command text.</param>
        /// <returns>The number of affected rows.</returns>
        public static async Task<int> ExecuteNonQueryAsync(string commandText)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(commandText, connection))
            {
                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Executes a SQL query and returns the result as a DataTable.
        /// </summary>
        /// <param name="queryText">The SQL query text.</param>
        /// <returns>The result as a DataTable.</returns>
        public static async Task<DataTable> ExecuteQueryAsync(string queryText)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(queryText, connection))
            {
                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    DataTable result = new DataTable();
                    result.Load(reader);
                    return result;
                }
            }
        }

        /// <summary>
        /// Executes a SQL scalar command and returns the result.
        /// </summary>
        /// <param name="commandText">The SQL command text.</param>
        /// <returns>The scalar result.</returns>
        public static async Task<object> ExecuteScalarAsync(string commandText)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(commandText, connection))
            {
                await connection.OpenAsync();
                return await command.ExecuteScalarAsync();
            }
        }

        // Add more utility methods as needed, such as:
        // - Insert, Update, Delete helpers
        // - Transaction handling
        // - Stored procedure execution
        // - Connection health checks
        // - Etc.
    }
}
