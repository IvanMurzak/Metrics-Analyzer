using Metrics_Analyzer.Data.CSV;
using Metrics_Analyzer.Processors;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Metrics_Analyzer.Data.Utils;

static internal class DataParser
{
    static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public static List<CompanyData> Parse(string companiesFilePath, string metricsFilePath)
    {
        var csvCompanies = CSV_Company.Parse(companiesFilePath);
        var csvMetrics = CSV_AppMetrics.Parse(metricsFilePath);

        if (csvCompanies.Count == 0)
            throw new Exception("No comanies found. Abort.");

        if (csvMetrics.Count == 0)
            throw new Exception("No metrices found. Abort.");

        var companies = csvCompanies.Select(company =>
        {
            return new CompanyData
            (
                id:          company.company_id,
                name:        company.company_name,
                countryCode: company.country_code,
                apps:        new()
            );
        }).ToList();

        foreach (var metric in csvMetrics)
        {
            var company = companies.FirstOrDefault(x => x.Id == metric.company_id);
            if (company == null)
            {
                _logger.Warn($"Company with id '{metric.company_id}' exists in metrics file but doesn't exist in companies file.");
                continue;
            }

            var app = company.apps.GetValueOrDefault(metric.app_name);
            if (app == null)
                company.apps[metric.app_name] = app = new AppData(company.Id, company.Name, metric.app_name, new());

            app.Timestamps.Add(new AppTimestampData
            (
                date:           metric.date,
                revenue:        metric.revenue,
                marketingSpend: metric.marketing_spend
            ));
        }

        return companies;
    }
    public static string ToCSV(List<AppProcessor.CompanyResult> input)
    {
        return CSV_AppProcessResult.ToCSV(input
            .SelectMany(company => company.apps)
            .Select(app =>
            {
                return new CSV_AppProcessResult
                (
                    company_id:   app.companyId,
                    company_name: app.companyName,
                    app_name:     app.name,
                    risk_score:   app.riskScore,
                    risk_rating:  app.riskRatingTitle
                );
            })
            .ToList());
    }
}
