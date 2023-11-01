using log4net.Core;
using log4net.Layout.Pattern;
using System.IO;

namespace Udla.UdlaServiceExtractInfoTeams.WinFormDemo.Logger
{
    public sealed class ProductNamePatternConverter : PatternLayoutConverter
    {


        #region protected override functions
        protected override void Convert(
            TextWriter writer,
            LoggingEvent loggingEvent)
        {
            writer.Write("SDS Teams Sincronization Service");
        }
        #endregion


    }
}
