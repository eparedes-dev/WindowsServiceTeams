using log4net;
using log4net.Config;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using Udla.UdlaServiceExtractInfoTeams.WinServiceApp.Server.WinServices.CoreTemplate.Executables;

namespace Udla.UdlaServiceExtractInfoTeams.WinServiceApp.Server.WinServices.CoreTemplate.Executables
{
    partial class WinService : ServiceBase
    {

        #region log declaration

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        #region private members

        private object _engine;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        #endregion


        #region Constructors

        public WinService()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository);
            InitializeComponent();
            this.ServiceName = ServiceConfiguration.ServiceName;
        }

        #endregion Constructors


        #region public static functions
        /// <summary>
        /// The main entry point for the process
        /// </summary>
        public static void Main()
        {
           
            log.InfoFormat("Main begin (ServiceName={0})", ServiceConfiguration.ServiceName);
            try
            {
                // Run WinService
                ServiceBase.Run(new WinService());
            }
            catch (Exception exception)
            {
                log.Error(
                    string.Format("Main failed (ServiceName={0})", ServiceConfiguration.ServiceName),
                    exception);
                throw;
            }
            finally
            {
                log.InfoFormat("Main end (ServiceName={0})", ServiceConfiguration.ServiceName);
            }
        }
        #endregion

        #region private functions

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // Set private members
            components = new Container();
        }

        #endregion


        #region protected overriden functions
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            // Dispose components
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        protected override void OnStart(string[] args)
        {
            log.InfoFormat("OnStart begin (ServiceName={0})", ServiceConfiguration.ServiceName);
            try
            {
                // Create engine
                _engine = ServiceConfiguration.CreateEngine();
                // Start engine
                ServiceConfiguration.StartEngine(_engine);
            }
            catch (Exception exception)
            {
                log.Error(
                    string.Format("OnStart failed (ServiceName={0})", ServiceConfiguration.ServiceName),
                    exception);
                throw;
            }
            finally
            {
                log.InfoFormat("OnStart end (ServiceName={0})", ServiceConfiguration.ServiceName);
            }
        }

        protected override void OnStop()
        {
            log.InfoFormat("OnStop begin (ServiceName={0})", ServiceConfiguration.ServiceName);
            try
            {
                if (_engine != null)
                {
                    // Stop engine
                    ServiceConfiguration.StopEngine(_engine);
                    _engine = null;
                }
            }
            catch (Exception exception)
            {
                log.Error(
                    string.Format("OnStop failed (ServiceName={0})", ServiceConfiguration.ServiceName),
                    exception);
                throw;
            }
            finally
            {
                log.InfoFormat("OnStop end (ServiceName={0})", ServiceConfiguration.ServiceName);
            }
        }

        #endregion

    }
}
