using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Udla.UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines.Configuration
{
    [Serializable]
    public class GeneralConfiguration
    {

        [XmlArray("Processors")]
        [XmlArrayItem("ProcessorConfiguration")]
        public List<ProcessorConfiguration> ProcessorConfiguration = new List<ProcessorConfiguration>();


        public void AddConfigurarion(ProcessorConfiguration processorConfiguration)
        {
            ProcessorConfiguration.Add(processorConfiguration);
        }

    }
}
