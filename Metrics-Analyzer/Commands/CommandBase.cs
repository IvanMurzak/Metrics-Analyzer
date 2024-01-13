using NLog;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompositeDisposable = System.Reactive.Disposables.CompositeDisposable;

namespace Metrics_Analyzer.Commands;

public class CommandBase : Command, IDisposable
{
    protected static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    protected CompositeDisposable? compositeDisposable = new CompositeDisposable();

    public CommandBase(string name, string? description = null) : base(name, description)
    {

    }

    public void Dispose()
    {
        compositeDisposable?.Dispose();
        compositeDisposable = null;
    }
}
