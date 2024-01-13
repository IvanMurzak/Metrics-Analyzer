namespace Metrics_Analyzer.Data;

internal class AppTimestampData
{
    public DateTime Date { private get; set; }
    public double Revenue { private set; get; }
    public double MarketingSpend { private set; get; }

    public AppTimestampData(DateTime date, double revenue, double marketingSpend)
    {
        Date = date;
        Revenue = revenue;
        MarketingSpend = marketingSpend;
    }
}
