using Metrics_Analyzer.Console.Extensions;
using Metrics_Analyzer.Data.CSV;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics_Analyzer.Commands
{
    public class CommandAnalyze : CommandBase
    {
        public CommandAnalyze(string? description = null) : base("analyze", description)
        {
            var argumentCompaniesFile = new Argument<string>("companies", "File name of companies (without extension).");
            var argumentMetricsFile = new Argument<string>("metrics", "File name of metrics (without extension).");

            this.FactoryAdd(argumentCompaniesFile)
                .FactoryAdd(argumentMetricsFile)
                .FactorySetHandler(context =>
                {
                    _logger.Info("[cyan]Analyze[/] command execution started");

                    var fileNameCompanies = context.ParseResult.GetValueForArgument(argumentCompaniesFile);
                    var fileNameMetrics = context.ParseResult.GetValueForArgument(argumentMetricsFile);

                    try
                    {
                        var companies = CSV_DataCompany.Parse(fileNameCompanies);
                        var metrics = CSV_DataAppMetrics.Parse(fileNameMetrics);

                        if (companies.Count == 0)
                        {
                            _logger.Error($"No comapnies found. Abort.");
                            context.ExitCode = 1;
                            return;
                        }
                        if (metrics.Count == 0)
                        {
                            _logger.Error($"No metrices found. Abort.");
                            context.ExitCode = 1;
                            return;
                        }



                    }
                    catch (Exception e)
                    {
                        _logger.Error(e.Message);
                        context.ExitCode = 1;
                    }
                });
        }
    }
}
