using System;

namespace Udla.UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines.Configuration
{
    [Serializable]
    public class ProcessorConfiguration
    {
        /// <summary>
        /// Indica el nombre del Procesador.
        /// </summary>
        public string ProcessorName { get; set; }

        /// <summary>
        /// Indica si el procesador está habilitado
        /// </summary>
        public bool IsEnabled { get; set; }

    }
}
