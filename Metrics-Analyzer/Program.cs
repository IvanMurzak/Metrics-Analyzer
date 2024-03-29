﻿using System;
using System.CommandLine;
using System.CommandLine.Parsing;
using Spectre.Console;
using NLog;
using Metrics_Analyzer.Console;
using Metrics_Analyzer.Commands;
using Metrics_Analyzer.Console.Extensions;
using System.Threading.Tasks;

class Program
{
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    [STAThread]
    static void Main(string[] args)
    {
        MAConsole.MALogo();

        var rootCommand = BuildCommands();

        if (args.Length == 0) // console mode
        {
            _logger.Info("Use [cyan]-h[/] or [cyan]--help[/] command to see all available commands");
            while (true)
            {
                AnsiConsole.Markup("[red] > [/]");

                var commandLine = Console.ReadLine();
                if (string.IsNullOrEmpty(commandLine))
                {
                    AnsiConsole.MarkupLine("[red] Wrong command, abort. [/]");
                    continue;
                }

                if (commandLine.StartsWith(".\\"))
                    commandLine = commandLine.Substring(2);
                if (commandLine.StartsWith("./"))
                    commandLine = commandLine.Substring(2);

                Task.Run(() => InvokeCommand
                (
                    () => rootCommand.InvokeAsync(commandLine),
                    exit: false
                )).Wait();
            }
        }
        else // single command execution mode
        {
            Task.Run(() => InvokeCommand
            (
                () => rootCommand.InvokeAsync(args),
                exit: true
            )).Wait();
        }
    }
    static RootCommand BuildCommands()
    {
        return (RootCommand)new RootCommand("Metrics Analyzer tool.")
            .FactoryAdd(new CommandAnalyze("Process local .csv files"));
    }
    static async Task InvokeCommand(Func<Task<int>> invoke, bool exit = true)
    {
        try
        {
            var result = await invoke();
            if (result == 0)
            {
                MAConsole.OperationCompleted("Finished successfully");
            }
            else
            {
                _logger.Fatal($"finished with error result = {result}", true);
            }
            if (exit) Environment.Exit(result);
        }
        catch (Exception e)
        {
            _logger.Fatal(e);
            if (exit) Environment.Exit(1);
        }
    }
}