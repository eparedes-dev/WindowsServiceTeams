using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Udla.UdlaServiceExtractInfoTeams.WinFormDemo.ConfiguracionXML;
using Udla.UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines;

namespace Udla.UdlaServiceExtractInfoTeams.WinFormStart
{
    public partial class FrmActivarServicioWindows : Form
    {
        #region log declaration
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        public FrmActivarServicioWindows()
        {
            InitializeComponent();
        }

        private void btnIniciarLog_Click(object sender, EventArgs e)
        {
            // Set up a simple configuration that logs on the console.
            //BasicConfigurator.Configure();

            var engine = new WinServiceEngine();
            WinServiceEngine x = (WinServiceEngine)engine;
            x.Start();

            btnIniciarLog.Enabled = false;
        }

        private void btnCrearArchivoConfiguracion_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessorConfigurationDemo processorConfigurationDemo = new ProcessorConfigurationDemo();
                processorConfigurationDemo.ProcessorName = "Procesador Prueba";
                processorConfigurationDemo.IsEnabled = true;
                processorConfigurationDemo.InfoTributariaQuery = "Select * from InfoTributaria";
                processorConfigurationDemo.DetalleQuery = "Select * from Detalles";


                var xml = new XmlSerializerNamespaces();
                xml.Add(string.Empty, string.Empty);

                // Serialize
                Type[] personTypes = { typeof(ProcessorConfigurationDemo) };
                var serializer = new XmlSerializer(processorConfigurationDemo.GetType());
                var stringWriter = new StringWriter();
                var xmlWriter = XmlWriter.Create(stringWriter);
                serializer.Serialize(xmlWriter, processorConfigurationDemo, xml);

                var path = Path.GetDirectoryName(Application.ExecutablePath) + "\\ProcessorConfigurationFile.xml";

                var result = stringWriter.GetStringBuilder().ToString();

                File.WriteAllText(path, result, Encoding.UTF8);

                MessageBox.Show("Archivo XML Creado en: " + path);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnArchivoConfiguracionVarios_Click(object sender, EventArgs e)
        {

            try
            {
                ConfigurationDemo configurationDemo = new ConfigurationDemo();

                ProcessorConfigurationDemo processorConfigurationDemoFacturas = new ProcessorConfigurationDemo();
                processorConfigurationDemoFacturas.ProcessorName = "ProcesadorFacturasBX";
                processorConfigurationDemoFacturas.IsEnabled = true;
                processorConfigurationDemoFacturas.InfoTributariaQuery = "Select * from InfoTributaria";
                processorConfigurationDemoFacturas.DetalleQuery = "Select * from Detalles";

                ProcessorConfigurationDemo processorConfigurationDemoNotasCredito = new ProcessorConfigurationDemo();
                processorConfigurationDemoNotasCredito.ProcessorName = "ProcesadorNotasCreditoBX";
                processorConfigurationDemoNotasCredito.IsEnabled = true;
                processorConfigurationDemoNotasCredito.InfoTributariaQuery = "Select * from InfoTributaria";
                processorConfigurationDemoNotasCredito.DetalleQuery = "Select * from Detalles";


                configurationDemo.AddConfigurarion(processorConfigurationDemoFacturas);
                configurationDemo.AddConfigurarion(processorConfigurationDemoNotasCredito);


                var xml = new XmlSerializerNamespaces();
                xml.Add(string.Empty, string.Empty);

                // Serialize
                Type[] personTypes = { typeof(ProcessorConfigurationDemo) };
                XmlSerializer serializer = new XmlSerializer(typeof(ConfigurationDemo), personTypes);
                var stringWriter = new StringWriter();
                var xmlWriter = XmlWriter.Create(stringWriter);
                serializer.Serialize(xmlWriter, configurationDemo, xml);

                var path = Path.GetDirectoryName(Application.ExecutablePath) + "\\ProcessorConfigurationFile.xml";

                var result = stringWriter.GetStringBuilder().ToString();

                File.WriteAllText(path, result, Encoding.UTF8);

                MessageBox.Show("Archivo XML Creado en: " + path);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FrmActivarServicioWindows_Load(object sender, EventArgs e)
        {

        }
    }
}
