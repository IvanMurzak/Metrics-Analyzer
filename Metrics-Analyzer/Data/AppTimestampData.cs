namespace Metrics_Analyzer.Data;

internal class AppTimestampData
{
    public DateTime Date { private get; set; }
    public decimal Revenue { private set; get; }
    public decimal MarketingSpend { private set; get; }

    public AppTimestampData(DateTime date, decimal revenue, decimal marketingSpend)
    {
        Date = date;
        Revenue = revenue;
        MarketingSpend = marketingSpend;
    }
}
