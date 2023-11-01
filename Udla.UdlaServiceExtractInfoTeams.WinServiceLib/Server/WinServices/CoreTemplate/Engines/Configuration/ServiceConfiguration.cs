using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace Udla.UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines.Configuration
{
    internal class ServiceConfiguration
    {
        #region public static readonly

        /// <summary>
        /// Nombre del Procesador que actualiza los estados de las Facturas y Notas de Crédito desde EBilling a BX. 
        /// Es el mismo nombre que tiene en el archivo de XML de configuración
        /// </summary>
        public static readonly string ProcesadorTest = "ProcesadorTest";
        public static readonly string ProcesadorExtractUsers = "ProcesadorExtractUsers";

        #endregion

        #region public properties

        public CultureInfo ServiceCultureInfo { get; private set; }
        public string CrmServiceUrl { get; private set; }
        public string CrmServiceUserName { get; private set; }
        public string CrmServicePassword { get; private set; }
        public string CrmServiceDomainName { get; private set; }

        /// <summary>
        /// Indica si el procesador está habilitado
        /// </summary>
        public IDictionary<string, bool> ProcessorIsEnabledByProcessorName { get; private set; }

        /// <summary>
        /// Contiene los Scripst y sentencias a ejecutar en la base de datos BX
        /// </summary>


        #endregion

        #region constructors
        public ServiceConfiguration(
            string configurationDirectoryPath,
            string configurationFilePath)
        {
            try
            {

                // 1.- Inicializo las clases que contiene  los scripts



                // 2.- Verifico si existe el archivo de configuración

                // 3.- Obtengo la información del configuración
                var generalConfiguration = ReadGeneralConfigurations(configurationFilePath);

                if (generalConfiguration != null && generalConfiguration.ProcessorConfiguration.Count > 0)
                {

                    // Obtengo el estado de los procesadores
                    IDictionary<string, bool> processorIsEnabledByProcessorName = new Dictionary<string, bool>();
                    foreach (var item in generalConfiguration.ProcessorConfiguration)
                    {
                        processorIsEnabledByProcessorName.Add(item.ProcessorName, item.IsEnabled);
                    }

                    this.ProcessorIsEnabledByProcessorName = processorIsEnabledByProcessorName;

                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format(
                    "Could not parse service configuration file (configurationDirectoryPath={0}, configurationFilePath={1}).",
                    configurationDirectoryPath,
                    configurationFilePath),
                    exception);
            }
        }
        #endregion

        #region private functions

        private GeneralConfiguration ReadGeneralConfigurations(string configurationFilePath)
        {
            try
            {
                var contenidoArchivo = File.ReadAllText(configurationFilePath);

                var generalConfiguration = new GeneralConfiguration();
                using (TextReader reader = new StringReader(contenidoArchivo))
                {
                    var serializer = new XmlSerializer(generalConfiguration.GetType());
                    generalConfiguration = (GeneralConfiguration)serializer.Deserialize(reader);
                }

                return generalConfiguration;
            }
            catch (Exception e)
            {
                throw new Exception("Error al cargar las configuraciones de los Procesadores.: " + e.Message);
            }

        }

        #endregion private functions

    }
}
