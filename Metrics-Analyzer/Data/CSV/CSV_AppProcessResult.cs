using System.Text;

namespace Metrics_Analyzer.Data.CSV;

internal class CSV_AppProcessResult
{
    public int    company_id   { private set; get; }
    public string company_name { private set; get; }
    public string app_name     { private set; get; }
    public double risk_score   { private set; get; }
    public string risk_rating  { private set; get; }

    public CSV_AppProcessResult(int company_id, string company_name, string app_name, double risk_score, string risk_rating)
    {
        this.company_id = company_id;
        this.company_name = company_name;
        this.app_name = app_name;
        this.risk_score = risk_score;
        this.risk_rating = risk_rating;
    }

    public static List<CSV_AppProcessResult> Parse(string filePath)
    {
        if (!filePath.ToLower().EndsWith(".csv"))
            filePath += ".csv";

        var csv = File.ReadAllText(filePath);
        var lines = csv.Split(CSVUtils.LineSeparator);

        return lines.Skip(1) // skip header
            .Select(line =>
            {
                var columns = line.Split(CSVUtils.ColumnSeparator);
                return new CSV_AppProcessResult
                (
                    company_id:   CSVUtils.ParseInt(columns[0]),
                    company_name: CSVUtils.ParseString(columns[1]),
                    app_name:     CSVUtils.ParseString(columns[2]),
                    risk_score:   CSVUtils.ParseDouble(columns[3]),
                    risk_rating:  CSVUtils.ParseString(columns[4])
                );
            })
            .ToList();
    }

    public static string ToCSV(List<CSV_AppProcessResult> items)
    {
        var strBuilder = new StringBuilder();
        strBuilder.Append($"company_id{CSVUtils.ColumnSeparator}company_name{CSVUtils.ColumnSeparator}app_name{CSVUtils.ColumnSeparator}risk_score{CSVUtils.ColumnSeparator}risk_rating{CSVUtils.LineSeparator}");
        
        foreach (var item in items)
        {
            strBuilder.Append(item.company_id);
            strBuilder.Append(CSVUtils.ColumnSeparator);

            strBuilder.Append(item.company_name);
            strBuilder.Append(CSVUtils.ColumnSeparator);

            strBuilder.Append(item.app_name);
            strBuilder.Append(CSVUtils.ColumnSeparator);

            strBuilder.Append(item.risk_score);
            strBuilder.Append(CSVUtils.ColumnSeparator);

            strBuilder.Append(item.risk_rating);
            strBuilder.Append(CSVUtils.ColumnSeparator);

            strBuilder.Append(CSVUtils.LineSeparator);
        }
        return strBuilder.ToString();
    }
}
