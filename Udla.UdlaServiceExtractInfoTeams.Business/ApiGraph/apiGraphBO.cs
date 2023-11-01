using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Kiota.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udla.UdlaServiceExtractInfoTeams.Business.ApiGraph
{
    public class ApiGraphBO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string applicationId = ConfigurationManager.AppSettings["Setting.Graph.ApplicationIdUDLA"].ToString();
        private static readonly string applicationSecret = ConfigurationManager.AppSettings["Setting.Graph.ApplicationSecretUDLA"].ToString();
        private static readonly string tenantId = ConfigurationManager.AppSettings["Setting.Graph.TenantIdUDLA"].ToString();
        private static readonly string scopeGraphApi = ConfigurationManager.AppSettings["Setting.Graph.ScopeGraphApiUDLA"].ToString();
        private static GraphServiceClient _graphServiceClient;

        //Metodo de conexion con el API
        private static GraphServiceClient GetAuthenticatedGraphClient()
        {
            var credencial = new ClientSecretCredential(tenantId, applicationId, applicationSecret);
            _graphServiceClient = new GraphServiceClient(credencial, new[] { scopeGraphApi });
            return _graphServiceClient;
        }

        public void GetUsersTenantInfo()
        {
            try
            {
                var graphClient = GetAuthenticatedGraphClient();
                var result = graphClient.Users["erickariel.paredes@udla.edu.ec"].GetAsync().Result;

                var users = graphClient.Users.
                var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 3 };
            }
            catch (Exception ex)
            {
                log.Error("Error en el método: GetUsersTenantInfo", ex);
            }



        }
    }
}
