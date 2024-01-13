using System.Collections.Generic;

namespace Metrics_Analyzer.Data.Runtime
{
    internal class AppData
    {
        public int CompanyId { private set; get; }
        public string CompanyName { private set; get; }
        public string Name { private set; get; }
        public List<AppTimestampData> Timestamps { private set; get; }

        public AppData(int companyId, string companyName, string name, List<AppTimestampData> timestamps)
        {
            CompanyId   = companyId;
            CompanyName = companyName;
            Name        = name;
            Timestamps  = timestamps;
        }
    }
}