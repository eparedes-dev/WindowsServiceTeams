using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udla.UdlaServiceExtractInfoTeams.Business.ApiGraph;
using Udla.UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines.Configuration;

namespace Udla.UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines.Processors.ExtractUsers
{
     class Processor : BaseProcessor
    {
        #region log declaration

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        #region public override properties

        public override string Name { get { return ServiceConfiguration.ProcesadorExtractUsers; } }

        #endregion


        #region protected override functions

        protected override void ExecuteInternal(OperationContext operationContext)
        {
            string mensajeParaLoguear = string.Empty;
            try
            {

                mensajeParaLoguear = "--- Procesador para extraer información de usuarios ---";
                this.Escribir_Log_Mail_Notificacion(operationContext, true, mensajeParaLoguear);
                ApiGraphBO apiGraphBO = new ApiGraphBO();
                apiGraphBO.GetUsersTenantInfo();

            }
            catch (Exception ex)
            {
                this.Escribir_Log_Mail_Notificacion(operationContext, true, $"Ocurrió un error en la ejecución de este procesador: {this.Name}. Exception: {ex}");
                log.Error($"Ocurrió un error en la ejecución de este procesador: {this.Name}", ex);
            }
            finally
            {

            }

        }

        private void Escribir_Log_Mail_Notificacion(OperationContext operationContext, bool escribirEnMail, string mensaje)
        {
            if (escribirEnMail)
            {
                operationContext.ContenidoMail_Servicio.AppendLine(mensaje);
            }
            log.InfoFormat(mensaje);
        }

        private void Escribir_Mail_Notificacion(OperationContext operationContext, string mensaje)
        {
            operationContext.ContenidoMail_Servicio.AppendLine(mensaje);

        }

        #endregion protected override functions
    }
}
