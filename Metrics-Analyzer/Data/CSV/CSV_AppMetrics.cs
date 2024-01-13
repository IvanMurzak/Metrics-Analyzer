namespace Metrics_Analyzer.Data.CSV;

internal class CSV_AppMetrics
{
    public DateTime date            { private set; get; }
    public string   app_name        { private set; get; }
    public int      company_id      { private set; get; }
    public double   revenue         { private set; get; }
    public double   marketing_spend { private set; get; }

    public static List<CSV_AppMetrics> Parse(string filePath)
    {
        if (!filePath.ToLower().EndsWith(".csv"))
            filePath += ".csv";

        var csv = File.ReadAllText(filePath);
        var lines = csv.Split(CSVUtils.LineSeparator);

        return lines.Skip(1) // skip header
            .Select(line =>
            {
                var columns = line.Split(CSVUtils.ColumnSeparator);
                return new CSV_AppMetrics
                {
                    date            = CSVUtils.ParseDate   (columns[0]),
                    app_name        = CSVUtils.ParseString (columns[1]),
                    company_id      = CSVUtils.ParseInt    (columns[2]),
                    revenue         = CSVUtils.ParseDouble (columns[3]),
                    marketing_spend = CSVUtils.ParseDouble (columns[4])
                };
            })
            .ToList();
    }
}
