﻿using System;

namespace Udla.UdlaServiceExtractInfoTeams.WinFormDemo.ConfiguracionXML
{
    [Serializable]
    public class ProcessorConfigurationDemo
    {
        /// <summary>
        /// Indica el nombre del Procesador.
        /// </summary>
        public string ProcessorName { get; set; }

        /// <summary>
        /// Indica si el procesador está habilitado
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Contiene el Query para obtener la Info Tributaria
        /// </summary>
        public string InfoTributariaQuery { get; set; }

        /// <summary>
        /// Contiene el Query para obtner los detalles
        /// </summary>
        public string DetalleQuery { get; set; }
    }
}
