namespace Metrics_Analyzer.Data.CSV
{
    internal class CSV_DataCompany
    {
        public int    company_id   { private set; get; }
        public string company_name { private set; get; }
        public string country_code { private set; get; }

        public static List<CSV_DataCompany> Parse(string filePath)
        {
            if (!filePath.ToLower().EndsWith(".csv"))
                filePath += ".csv";

            var csv = File.ReadAllText(filePath);
            var lines = csv.Split(CSVUtils.LineSeparator);

            return lines.Skip(1) // skip header
                .Select(line =>
                {
                    var columns = line.Split(CSVUtils.ColumnSeparator);
                    return new CSV_DataCompany
                    {
                        company_id   = CSVUtils.ParseInt(columns[0]),
                        company_name = CSVUtils.ParseString(columns[1]),
                        country_code = CSVUtils.ParseString(columns[2])
                    };
                })
                .ToList();
        }
    }
}
