using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Udla.UdlaServiceExtractInfoTeams.WinFormStart
{
    static class Program
    {

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            //XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            XmlConfigurator.Configure(logRepository);
            //log.Info("Hello logging world!");
            //log.Error("Error!");
            //log.Warn("Warn!");

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmActivarServicioWindows());


            //var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            ////XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            //XmlConfigurator.Configure(logRepository);

            //Console.WriteLine("Hello world!");

            //// Log some things
            //log.Info("Hello logging world!");
            //log.Error("Error!");
            //log.Warn("Warn!");

            //Console.ReadLine();

        }
    }
}
