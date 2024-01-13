using Metrics_Analyzer.Data.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Metrics_Analyzer.Processors;

static partial class AppProcessor
{
    public static List<CompanyResult> Process(IEnumerable<CompanyData> companies)
    {
        return companies.Select(company =>
        {
            return new CompanyResult
            {
                id = company.Id,
                name = company.Name,
                apps = company.apps.Values
                    .Select(app => app.ProcessApp(company))
                    .ToList()
            };
        }).ToList();
    }

    static AppResult ProcessApp(this AppData appData, CompanyData company)
    {
        var appResult = new AppResult()
        {
            companyId   = company.Id,
            companyName = company.Name,
            name        = appData.Name,
            publishDate = appData.Timestamps[0].Date
        };
        foreach (var timestamp in appData.Timestamps)
        {
            if (appResult.CAC + timestamp.MarketingSpend <= 0)
                continue; // ignoring timestamp while no marketing spend

            appResult.LTV += timestamp.Revenue;
            appResult.CAC += timestamp.MarketingSpend;

            if (appResult.firstPayback == null && appResult.LTV >= appResult.CAC)
            {
                appResult.firstPayback = timestamp.Date;
            }
        }

        var paybackValue = appResult.firstPayback == null
            ? ParseRange(double.MaxValue, RiskScore_Payback_Value)
            : ParseRange(appResult.PaybackDays, RiskScore_Payback_Value);

        var LTVtoCAC_Value = ParseRange(appResult.LTVtoCAC, RiskScore_LTVtoCAC_Value);

        appResult.riskScore = paybackValue * RiskScore_Payback_Coefficient + LTVtoCAC_Value * RiskScore_LTVtoCAC_Coefficient;
        appResult.riskRating = ParseRange(appResult.riskScore, RiskRating_RiskScore_Value);
        appResult.riskRatingTitle = RiskRatingTitle_Value.GetValueOrDefault(appResult.riskRating) ?? "Unknown";

        return appResult;
    }

    public class CompanyResult
    {
        public int id;
        public string name;
        public List<AppResult> apps;
    }
    public class AppResult
    {
        public int companyId;
        public string companyName;
        public string name;

        public double LTV;
        public double CAC;

        public DateTime publishDate;
        public DateTime? firstPayback;

        public double riskScore;
        public double riskRating;
        public string riskRatingTitle;

        public double LTVtoCAC => LTV / CAC;

        public int PaybackDays => firstPayback == null
            ? -1
            : (firstPayback.Value - publishDate).Days;
    }
}
