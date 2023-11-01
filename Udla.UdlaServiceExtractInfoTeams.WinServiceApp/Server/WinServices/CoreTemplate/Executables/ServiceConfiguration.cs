
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;
using Udla.UdlaServiceExtractInfoTeams.WinServiceLib.Server.WinServices.CoreTemplate.Engines;

namespace Udla.UdlaServiceExtractInfoTeams.WinServiceApp.Server.WinServices.CoreTemplate.Executables
{
    /// <summary>
    /// This class customizes the operation of the WinService and 
    /// WinServiceInstaller.  If this "winservice framework" needs to be used on 
    /// another project, then this class should be modified.
    /// </summary>
    internal static class ServiceConfiguration
    {

        #region public static readonly
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly string ServiceDisplayName = "Udla SDS Teams Sincronization WinService";
        public static readonly string ServiceName = "UdlaSDSTeamsSincronizationWinService";

        #endregion


        #region public static functions
        public static object CreateEngine()
        {
            return new WinServiceEngine();
        }


        public static void StartEngine(
            object engine)
        {

            //((WinServiceEngine)engine).Start();
            WinServiceEngine x = (WinServiceEngine)engine;
            x.Start();
        }


        public static void StopEngine(
            object engine)
        {
            ((WinServiceEngine)engine).Stop();
        }

        #endregion

    }
}
