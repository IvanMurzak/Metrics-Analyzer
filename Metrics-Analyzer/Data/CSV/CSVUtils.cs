namespace Metrics_Analyzer.Data.CSV
{
    static internal class CSVUtils
    {
        public const char LineSeparator = '\n';
        public const char ColumnSeparator = ',';

        public static string ParseString(string str) => str ?? String.Empty;
        public static DateTime ParseDate(string str)
        {
            if (DateTime.TryParse(str, out var result))
                return result;
            return default(DateTime);
        }
        public static int ParseInt(string str)
        {
            if (int.TryParse(str, out var result))
                return result;
            return 0;
        }
        public static decimal ParseDecimal(string str)
        {
            if (decimal.TryParse(str, out var result))
                return result;
            return 0;
        }
        public static double ParseDouble(string str)
        {
            if (double.TryParse(str, out var result))
                return result;
            return 0;
        }
    }
}
