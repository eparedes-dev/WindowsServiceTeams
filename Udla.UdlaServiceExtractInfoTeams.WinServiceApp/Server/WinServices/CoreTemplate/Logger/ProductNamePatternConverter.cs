using log4net.Core;
using log4net.Layout.Pattern;
using System.IO;
using Udla.UdlaServiceExtractInfoTeams.WinServiceApp.Server.WinServices.CoreTemplate.Executables;


namespace Udla.UdlaServiceExtractInfoTeams.WinServiceApp.Server.WinServices.CoreTemplate.Logger
{
    public sealed class ProductNamePatternConverter : PatternLayoutConverter
	{
        #region protected override functions
		protected override void Convert(
			TextWriter writer,
			LoggingEvent loggingEvent)
		{
			writer.Write(ServiceConfiguration.ServiceName);
		}
		#endregion

	}
}