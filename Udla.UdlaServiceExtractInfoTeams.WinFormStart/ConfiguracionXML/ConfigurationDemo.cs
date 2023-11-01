using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Udla.UdlaServiceExtractInfoTeams.WinFormDemo.ConfiguracionXML
{
    [Serializable]
    [XmlRoot("GeneralConfiguration")]
    //[XmlInclude(typeof(ProcessorConfigurationDemo))] // include type class Person
    public class ConfigurationDemo
    {
        public ConfigurationDemo()
        { }


        [XmlArray("Processors")]
        [XmlArrayItem("ProcessorConfiguration")]
        public List<ProcessorConfigurationDemo> ProcessorConfigurationDemo = new List<ProcessorConfigurationDemo>();


        public void AddConfigurarion(ProcessorConfigurationDemo processorConfigurationDemo)
        {
            ProcessorConfigurationDemo.Add(processorConfigurationDemo);
        }

    }
}
