using Metrics_Analyzer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics_Analyzer.Processors
{
    static class AppProcessor
    {
        public static List<CompanyResult> Process(IEnumerable<CompanyData> companies)
        {
            return companies.Select(company =>
            {
                return new CompanyResult
                {
                    id = company.Id,
                    name = company.Name,
                    apps = company.apps.Values.Select(app =>
                    {
                        var appResult = new AppResult()
                        {
                            publishDate = app.Timestamps[0].Date,
                            name = app.Name
                        };
                        foreach (var timestamp in app.Timestamps)
                        {
                            appResult.LTV += timestamp.Revenue;
                            appResult.CAC += timestamp.MarketingSpend;

                            if (appResult.firstPayback == null && appResult.LTV >= appResult.CAC)
                            {
                                appResult.firstPayback = timestamp.Date;
                            }
                        }
                        return appResult;
                    }).ToList()
                };
            }).ToList();
        }

        public class CompanyResult
        {
            public int id;
            public string name;
            public List<AppResult> apps;
        }
        public class AppResult
        {
            public string name;

            public double LTV;
            public double CAC;
            public double riskRating;
            public double riskScore;

            public DateTime publishDate;
            public DateTime? firstPayback;

            public int PaybackDays => firstPayback == null
                ? -1
                : (firstPayback.Value - publishDate).Days;
            public double LTVtoCAC => LTV / CAC;
        }
    }
}
