using System;
using System.Text;

namespace Udla.UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines.Configuration
{
    internal class OperationContext
    {

        #region public properties
        public DateTime TimeStamp { get; private set; }
        public ServiceSettings ServiceSettings { get; private set; }
        public ServiceConfiguration ServiceConfiguration { get; private set; }

        /// <summary>
        /// Propiedad para ir almacenando el contenido que se quiere enviar en un mail. Este contenido se recoge del procesador
        /// que envía información desde Banner a AD
        /// </summary>
        public StringBuilder ContenidoMail_Servicio { get; set; }

        #endregion

        #region constructors
        public OperationContext()
        {
            // 1. Set the time stamp.
            this.TimeStamp = DateTime.Now;

            // 2. Read the service settings.
            this.ServiceSettings = new ServiceSettings();

            // 3. Read the service configuration.
            this.ServiceConfiguration = new ServiceConfiguration(this.ServiceSettings.ConfigurationDirectoryPath, this.ServiceSettings.ConfigurationFilePath);


            // 4. Inicializo el contenedor de la información a enviar en el mail.
            this.ContenidoMail_Servicio = new StringBuilder();

        }

        #endregion



    }
}
