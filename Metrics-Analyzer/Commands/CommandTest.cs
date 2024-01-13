using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics_Analyzer.Commands
{
    public class CommandTest : CommandBase
    {
        public CommandTest(string? description = null) : base("test", description)
        {
        }
    }
}
