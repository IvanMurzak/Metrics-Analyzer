using NLog;
using System;
using System.CommandLine;

namespace Metrics_Analyzer.Commands;

public class CommandBase : Command, IDisposable
{
    protected static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public CommandBase(string name, string? description = null) : base(name, description)
    {

    }

    public void Dispose()
    {
    }
}
