using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Udla.UdlaServiceExtractInfoTeams.Data.Enums;

namespace Udla.UdlaServiceExtractInfoTeams.Data.AdoNet
{
    /// <summary>
    /// Permite la conexión con la base de datos
    /// </summary>
    public class DataBaseConection
    {

        #region  Private Properties

        private static string SDSTeamsSincronizationDB_ConnectionString = ConfigurationManager.ConnectionStrings["SDSTeamsSincronizationDBConnectionString"].ConnectionString;
     
        #endregion  Private Properties

        #region SQL SERVER

        /// <summary>
        /// Ejecuta una consulta SQL (Select) sobre la base de datos SQL Server
        /// </summary>
        /// <param name="query">La consulta a ejecutar (Select)</param>
        /// <returns>El DataSet con los datos recibidos</returns>
        public static DataSet SQLServer_Query(string query)
        {
            EnumDataBase enumDataBase = EnumDataBase.SDSTeamsSincronizationDB;
            SqlConnection sqlConnection = ConstruirConexionDB(enumDataBase);

            try
            {
                sqlConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                dataAdapter.SelectCommand = new SqlCommand(query, sqlConnection);
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static object SQLServer_ExecuteScalar(string query, EnumDataBase enumDataBase)
        {
            SqlConnection sqlConnection = ConstruirConexionDB(enumDataBase);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                return sqlCommand.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }


        /// <summary>
        /// Ejecuta una sentencia (Insert, Update, Delete) SQL sobre la base de datos SQL Server.
        /// </summary>
        /// <param name="strSentence">La sentencia a ejecutar</param>
        /// <returns>El resultado de que si se ejecuto o no la sentencia</returns>
        public static bool SQLServer_Execute(string strSentence, EnumDataBase enumDataBase)
        {
            using (SqlConnection sqlConnection = ConstruirConexionDB(enumDataBase))
            {
                sqlConnection.Open();
                using (SqlTransaction transaction = sqlConnection.BeginTransaction())
                {
                    //SqlConnection sqlConnection = ConstruirConexionDB(enumDataBase);
                    try
                    {
                        SqlCommand sqlCommand = sqlConnection.CreateCommand();
                        sqlCommand.Transaction = transaction;
                        sqlCommand.CommandText = strSentence;
                        sqlCommand.ExecuteNonQuery();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception )
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }


        public static bool SQLServer_ExecuteCommandList(List<SqlCommand> listaSqlCommand, EnumDataBase enumDataBase)
        {
            try
            {
                int count = 0;

                using (SqlConnection sqlConnection = ConstruirConexionDB(enumDataBase))
                {
                    sqlConnection.Open();

                    using (SqlTransaction transaction = sqlConnection.BeginTransaction())
                    {
                        try
                        {
                            foreach (var cmd in listaSqlCommand)
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Connection = sqlConnection;
                                cmd.Transaction = transaction;

                                cmd.ExecuteNonQuery();

                                count++;
                            }
                            transaction.Commit();
                        }
                        catch (Exception )
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                    sqlConnection.Close();
                }

                return true;
            }
            catch (Exception )
            {
                throw;
            }

        }

        public static void SQLServer_ExecuteBulkCopy(DataSet ds)//DataTable destinationTable, string destinationTableName)
        {
            EnumDataBase enumDataBase = EnumDataBase.SDSTeamsSincronizationDB;
            //int countStart = destinationTable.Rows.Count;
            int countStart = 0;

            using (SqlConnection connection = ConstruirConexionDB(enumDataBase))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (DataTable table in ds.Tables)
                        {
                            string destinationTableName = table.TableName;
                            countStart = table.Rows.Count;
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.CheckConstraints, transaction))
                            {
                                bulkCopy.BatchSize = 2000;
                                bulkCopy.BulkCopyTimeout = 180;
                                bulkCopy.DestinationTableName = destinationTableName;
                                bulkCopy.NotifyAfter = countStart;
                                bulkCopy.WriteToServer(table);
                            }
                        }


                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                }
            }

        }

        public static DataTable ExcuteSP(string query, string fecha1, string fecha2)
        {
            EnumDataBase enumDataBase = EnumDataBase.SDSTeamsSincronizationDB;
            SqlConnection sqlConnection = ConstruirConexionDB(enumDataBase);
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
                    DateTime fechaInicio = DateTime.ParseExact(fecha1, "yyyy-MM-dd", cultureinfo);
                    DateTime fechaFin = DateTime.ParseExact(fecha2, "yyyy-MM-dd", cultureinfo);

                    cmd.Parameters.AddWithValue("@BlockDateStart", fecha1);
                    cmd.Parameters.AddWithValue("@BlockDateEnd", fecha2);

                    sqlConnection.Open();
                    var rdr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(rdr);

                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Actualizar cuenta de soporte 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="updateSentence"></param>
        /// <returns></returns>
        public bool BatchUpdateSincronizationState(DataSet ds, string updateSentence)
        {
            EnumDataBase enumDataBase = EnumDataBase.SDSTeamsSincronizationDB;
            int batchSize = 100;
            bool resultado = false;

            using (SqlConnection connection = ConstruirConexionDB(enumDataBase))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Create a SqlDataAdapter.
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.InsertCommand = new SqlCommand(updateSentence, connection);

                        // Parámetros
                        adapter.InsertCommand.Parameters.Add("@SectionSupportAccountId", SqlDbType.Int, 2, "SectionSupportAccountId");
                        adapter.InsertCommand.Parameters.Add("@SectionId", SqlDbType.Int, 2, "SectionId");
                        adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        adapter.InsertCommand.Transaction = transaction;

                        // Set the batch size.
                        adapter.UpdateBatchSize = batchSize;
                        //adapter.ContinueUpdateOnError = true;

                        // Execute the update.
                        adapter.Update(ds.Tables[0]);

                        transaction.Commit();
                        resultado = true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();                     
                        resultado = false;
                        throw;
                    }
                }
            }

            return resultado;
        }

        public bool BatchUpdateGraphIdSection(DataSet ds, string updateSentence)
        {
            EnumDataBase enumDataBase = EnumDataBase.SDSTeamsSincronizationDB;
            int batchSize = 100;
            bool resultado = false;

            using (SqlConnection connection = ConstruirConexionDB(enumDataBase))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Create a SqlDataAdapter.
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.InsertCommand = new SqlCommand(updateSentence, connection);

                        // Parámetros
                        adapter.InsertCommand.Parameters.Add("@GraphId", SqlDbType.VarChar, 250, "GraphId");
                        adapter.InsertCommand.Parameters.Add("@SectionId", SqlDbType.Int, 2, "SectionId");
                        adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        adapter.InsertCommand.Transaction = transaction;

                        // Set the batch size.
                        adapter.UpdateBatchSize = batchSize;
                        //adapter.ContinueUpdateOnError = true;

                        // Execute the update.
                        adapter.Update(ds.Tables[0]);

                        transaction.Commit();
                        resultado = true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        resultado = false;
                        throw;
                    }
                }
            }

            return resultado;
        }
        public bool BatchUpdateSectionIsArchivedSection(DataSet ds, string updateSentence)
        {
            EnumDataBase enumDataBase = EnumDataBase.SDSTeamsSincronizationDB;
            int batchSize = 100;
            bool resultado = false;

            using (SqlConnection connection = ConstruirConexionDB(enumDataBase))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Create a SqlDataAdapter.
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.InsertCommand = new SqlCommand(updateSentence, connection);

                        // Parámetros
                        adapter.InsertCommand.Parameters.Add("@SectionIsArchived", SqlDbType.Bit, 1, "SectionIsArchived");
                        adapter.InsertCommand.Parameters.Add("@MarkedForArchiving", SqlDbType.Bit, 1, "MarkedForArchiving");
                        adapter.InsertCommand.Parameters.Add("@SectionId", SqlDbType.Int, 2, "SectionId");
                        adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        adapter.InsertCommand.Transaction = transaction;

                        // Set the batch size.
                        adapter.UpdateBatchSize = batchSize;
                        //adapter.ContinueUpdateOnError = true;

                        // Execute the update.
                        adapter.Update(ds.Tables[0]);

                        transaction.Commit();
                        resultado = true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        resultado = false;
                        throw;
                    }
                }
            }

            return resultado;
        }

        public bool BatchUpdateSectionIsUnArchivedSection(DataSet ds, string updateSentence)
        {
            EnumDataBase enumDataBase = EnumDataBase.SDSTeamsSincronizationDB;
            int batchSize = 100;
            bool resultado = false;

            using (SqlConnection connection = ConstruirConexionDB(enumDataBase))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Create a SqlDataAdapter.
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.InsertCommand = new SqlCommand(updateSentence, connection);

                        // Parámetros
                        adapter.InsertCommand.Parameters.Add("@SectionIsArchived", SqlDbType.Bit, 1, "SectionIsArchived");
                        adapter.InsertCommand.Parameters.Add("@MarkedForUnArchiving", SqlDbType.Bit, 1, "MarkedForUnArchiving");
                        adapter.InsertCommand.Parameters.Add("@UnArchivedDate", SqlDbType.DateTime, 8, "UnArchivedDate");
                        adapter.InsertCommand.Parameters.Add("@SectionId", SqlDbType.Int, 2, "SectionId");
                        adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        adapter.InsertCommand.Transaction = transaction;

                        // Set the batch size.
                        adapter.UpdateBatchSize = batchSize;
                        //adapter.ContinueUpdateOnError = true;

                        // Execute the update.
                        adapter.Update(ds.Tables[0]);

                        transaction.Commit();
                        resultado = true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        resultado = false;
                        throw;
                    }
                }
            }

            return resultado;
        }

        private static SqlConnection ConstruirConexionDB(EnumDataBase enumDataBase)
        {
            SqlConnection sqlConnection = null;

            if (enumDataBase == EnumDataBase.SDSTeamsSincronizationDB)
                sqlConnection = new SqlConnection(SDSTeamsSincronizationDB_ConnectionString);


            return sqlConnection;
        }



        #endregion SQL SERVER

    }
}
