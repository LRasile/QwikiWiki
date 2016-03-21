using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Qwikiwiki.DataLayer
{
    
    public sealed class SQLHelper
    {
        private SQLHelper()
        {
        }

        /// <summary>
        /// Execute Non Query for SQL Command
        /// </summary>
        /// <param name="sqlComm">SqlCommand with full parameters</param>
        /// <returns>Number of integer greater than zero if successfull, otherwise 0</returns>
        /// <remarks></remarks>
        public static int ExecuteNonQuery(ref SqlCommand sqlComm)
        {
            Int32 result = -1;
            SqlConnection sqlConnection = GetSqlConnection();
            try
            {
                sqlConnection.Open();
                sqlComm.Connection = sqlConnection;
                result = sqlComm.ExecuteNonQuery();

            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return result;
        }


        ///' <summary>
        ///' Get List of T type from sqlCommand
        ///' </summary>
        ///' <typeparam name="T">Type of tables that sql query on database</typeparam>
        ///' <param name="sqlComm">SqlCommand with full parameters</param>
        ///' <returns>List of Object whose type is T</returns>
        ///' <remarks>This method call when retrive records from sql query</remarks>
        public static List<T> GetEntityList<T>(ref SqlCommand sqlComm) where T : class, new()
        {
            List<T> result = new List<T>();
            SqlConnection SqlConnection = GetSqlConnection();
            SqlDataReader sqlReader = default(SqlDataReader);
            try
            {
                SqlConnection.Open();
                sqlComm.Connection = SqlConnection;
                sqlReader = sqlComm.ExecuteReader();
                while (sqlReader.Read())
                {
                    T item = ReflectionHelper.ReflectType<T>(sqlReader);
                    result.Add(item);
                }
                sqlReader.Close();

            }
            finally
            {
                SqlConnection.Close();
                SqlConnection.Dispose();
            }

            return result;
        }


        /// <summary>
        /// Get instance of T type from sqlCommand
        /// </summary>
        /// <typeparam name="T">Type of tables that sql query on database</typeparam>
        /// <param name="sqlComm">SqlCommand with full parameters</param>
        /// <returns>List of Object whose type is T</returns>
        /// <remarks>This method call when retrive records from sql query</remarks>
        public static T GetEntity<T>(ref SqlCommand sqlComm) where T : class, new()
        {
            T result = null;
            SqlConnection sqlConnection = GetSqlConnection();
            SqlDataReader sqlReader = default(SqlDataReader);
            try
            {
                sqlConnection.Open();
                sqlComm.Connection = sqlConnection;
                sqlReader = sqlComm.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    result = ReflectionHelper.ReflectType<T>(sqlReader);
                }
                sqlReader.Close();

            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return result;
        }

        public static T GetScalar<T>(ref SqlCommand sqlComm)
        {
            T result;
            SqlConnection SqlConnection = GetSqlConnection();
            try
            {
                SqlConnection.Open();
                sqlComm.Connection = SqlConnection;
                result = (T)sqlComm.ExecuteScalar();
            }
            finally
            {
                SqlConnection.Close();
                SqlConnection.Dispose();
            }
            return result;
        }


        private static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(GetSqlConnectionString());
        }

        /// <summary>
        /// Get Connection String from config
        /// </summary>
        /// <returns>Connection string to connect to sql server 2005 using provider sql client</returns>
        /// <remarks></remarks>
        private static string GetSqlConnectionString()
        {
            string result = string.Empty;
            ConnectionStringSettings connectionStringSetting = ConfigurationManager.ConnectionStrings["strConn"];
            if (connectionStringSetting != null)
            {
                result = connectionStringSetting.ConnectionString;
            }
            return result;
        }
    }

}
