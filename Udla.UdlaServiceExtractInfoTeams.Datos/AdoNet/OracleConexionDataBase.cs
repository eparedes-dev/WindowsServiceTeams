//using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Udla.UdlaServiceExtractInfoTeams.Data.AdoNet
{
    /// <summary>
    /// Contiene los comandos para conectarse con una base de datos MS SQL Server
    /// </summary>
    /// 
    public class OracleConexionDataBase
    {

        //Test
        private static string BannerConnection = ConfigurationManager.ConnectionStrings["BannerConnection_ConnectionString"].ConnectionString;

        //Funcion que ejecuta un query sql y devuelve un dataset con los datos
        public DataSet ExecuteOracleQuery(string QuerySql)
        {
            DataSet DsReturnQuery = new DataSet();
  
            using (OracleConnection Banner = new OracleConnection(BannerConnection))
            {
                try
                {
                    OracleCommand SqlCmd = new OracleCommand(QuerySql, Banner);
                    SqlCmd.CommandType = System.Data.CommandType.Text;
                    OracleDataAdapter SqlDataAdapter = new OracleDataAdapter();
                    SqlDataAdapter.SelectCommand = SqlCmd;
                    SqlDataAdapter.Fill(DsReturnQuery);

                    return DsReturnQuery;
                }
                catch (Exception)
                {

                    return DsReturnQuery;
                }
               
            }
        }


     


    }
}
