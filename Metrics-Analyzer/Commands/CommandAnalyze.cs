using Metrics_Analyzer.Console;
using Metrics_Analyzer.Console.Extensions;
using Metrics_Analyzer.Data.CSV;
using Metrics_Analyzer.Data.Utils;
using Metrics_Analyzer.Processors;
using Newtonsoft.Json.Linq;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                        var companies = TimeTracker.Do("Loading CSV files and parsing", 
                                () => DataParser.Parse(fileNameCompanies, fileNameMetrics))
                            .Print().Result;

                        var result = TimeTracker.Do("Processing data",
                                () => AppProcessor.Process(companies))
                            .Print().Result;

                        foreach (var companyResult in result)
                        {
                            var tree = new Tree($"{companyResult.name.ToString()} [gray]ID=[[{companyResult.id.ToString()}]][/]")
                            {
                                Style = new Style(Color.Yellow)
                            };
                            //tree.Style = new Style(Color.Yellow);

                            var treeApps = tree.AddNode($"[gold3_1]Owned apps ({companyResult.apps.Count}):[/]");
                            foreach (var app in companyResult.apps)
                            {
                                var treeApp = treeApps.AddNode($"[white]{app.name.PadRight(17)}[/] [gray]Published at {app.publishDate.ToShortDateString()}[/]");

                                treeApp.AddNode($"");
                                treeApp.AddNode($"[skyblue2]LTV[/] --------- [green]{app.LTV}[/]");
                                treeApp.AddNode($"[skyblue2]CAC[/] --------- [red]{app.CAC}[/]");
                                treeApp.AddNode($"[skyblue2]LTV:CAC[/] ----- [yellow]{app.LTVtoCAC}[/]");

                                if (app.firstPayback == null)
                                {
                                    treeApp.AddNode($"[skyblue2]Payback[/] ----- [red]No payback yet[/]");
                                }
                                else
                                {
                                    treeApp.AddNode($"[skyblue2]Payback[/] ----- [green]{app.PaybackDays} days[/]. [gray]Happened at {app.firstPayback.Value.ToShortDateString()}[/]");
                                }

                                treeApp.AddNode($"[skyblue2]Risk rating[/] - [green]{app.riskRating}[/]");
                                treeApp.AddNode($"[skyblue2]Risk score[/] -- [green]{app.riskScore}[/]");
                            }
                            AnsiConsole.Write(tree);
                            AnsiConsole.WriteLine();
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
