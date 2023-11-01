using log4net.Core;
using log4net.Layout.Pattern;
using System.IO;

namespace Udla.UdlaServiceExtractInfoTeams.WinServiceApp.Server.WinServices.CoreTemplate.Logger
{
    public sealed class LogCounterPatternConverter : PatternLayoutConverter
	{

		#region private static members
		private static int LogCounter = 0;
		#endregion

		#region protected override functions
		protected override void Convert(
			TextWriter writer,
			LoggingEvent loggingEvent)
		{
			writer.Write(LogCounter.ToString());
			LogCounter++;
		}
		#endregion

	}
}