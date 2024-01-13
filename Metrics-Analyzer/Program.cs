using System;
using System.CommandLine;
using System.CommandLine.Parsing;
using Spectre.Console;
using NLog;
using Metrics_Analyzer.Console;
using Metrics_Analyzer.Commands;
using Metrics_Analyzer.Console.Extensions;

class Program
{
    public static readonly string EnvironmentPath = Directory.GetCurrentDirectory() + "\\data";
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    [STAThread]
    static void Main(string[] args)
    {
        MAConsole.MALogo();

        Initialize();
        _logger.Info("Use [cyan]-h[/] or [cyan]--help[/] command to see all available commands");

        var rootCommand = BuildCommands();

        if (args.Length == 0) // console mode
        {
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
                if (commandLine.ToLower().StartsWith("eve-master.exe"))
                    commandLine = commandLine.Substring(14);
                if (commandLine.ToLower().StartsWith("eve-master"))
                    commandLine = commandLine.Substring(10);

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
    static void Initialize()
    {
        AnsiConsole.Status().Start("Initializing...", ctx =>
        {
            TimeTracker.Do("Initialized", () =>
            {
                // JsonUtilityEx.Init();

                // Application.EnableVisualStyles();
                // Application.SetCompatibleTextRenderingDefault(false);
            }).Print();
        });
    }
    static RootCommand BuildCommands()
    {
        return (RootCommand)new RootCommand("Metrics Analyzer tool.")
            .FactoryAdd(new CommandTest("Process local .csv files"));
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
    public static string GetPath(string path)
    {
        if (path[0] == '.') path = path.Substring(1);
        if (path[0] == '/' || path[0] == '\\')
            return $"{EnvironmentPath}{path}";
        else
            return $"{EnvironmentPath}\\{path}";
    }
}