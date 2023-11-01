namespace Udla.UdlaServiceExtractInfoTeams.Util.Extentions
{
    public static class GeneralExtentions
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
