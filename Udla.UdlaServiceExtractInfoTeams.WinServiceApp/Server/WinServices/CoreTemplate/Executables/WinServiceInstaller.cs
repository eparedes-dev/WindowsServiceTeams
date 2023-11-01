﻿using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using Udla.UdlaServiceExtractInfoTeams.WinServiceApp.Server.WinServices.CoreTemplate.Executables;

namespace Udla.UdlaServiceExtractInfoTeams.WinServiceApp.Server.WinServices.CoreTemplate.Executables
{
    /// <summary>
    /// Custom installer for WinService.  Do not change the contents of this file.
    /// If you require customization, modify the ServiceConfiguration class 
    /// instead.
    /// </summary>
    [RunInstaller(true)]
    public class WinServiceInstaller : Installer
    {
        #region log declaration

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        #region private members
        private ServiceProcessInstaller _serviceProcessInstaller;
        private ServiceInstaller _serviceInstaller;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        #endregion


        #region constructors
        public WinServiceInstaller()
        {
            // This call is required by the Designer.
            InitializeComponent();

            // Initialize installers.
            InitializeInstallers();
        }
        #endregion


        #region private functions
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }


        private void InitializeInstallers()
        {
            // Set private members
            _serviceProcessInstaller = new ServiceProcessInstaller();
            _serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            _serviceProcessInstaller.Password = null;
            _serviceProcessInstaller.Username = null;


            _serviceInstaller = new ServiceInstaller();
            _serviceInstaller.DisplayName = ServiceConfiguration.ServiceDisplayName;
            _serviceInstaller.ServiceName = ServiceConfiguration.ServiceName;
            _serviceInstaller.Description = "Servicio Version 1.0 para sincronización de TEAMS";

            

            Installer[] installers = new Installer[2];
            installers[0] = _serviceProcessInstaller;
            installers[1] = _serviceInstaller;
            base.Installers.AddRange(
                installers);
        }

        #endregion


        #region protected override functions
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
        #endregion

    }
}
