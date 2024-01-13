using System;

namespace Metrics_Analyzer.Data.Runtime
{
    internal class AppTimestampData
    {
        public DateTime Date { private set; get; }
        public double Revenue { private set; get; }
        public double MarketingSpend { private set; get; }

        public AppTimestampData(DateTime date, double revenue, double marketingSpend)
        {
            Date = date;
            Revenue = revenue;
            MarketingSpend = marketingSpend;
        }
    }
}