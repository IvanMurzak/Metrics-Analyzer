using System.Collections.Generic;
using System.Linq;

namespace Metrics_Analyzer.Data.Runtime;

internal class CompanyData
{
    public int Id { private set; get; }
    public string Name { private set; get; }
    public string CountryCode { private set; get; }
    public Dictionary<string, AppData> apps { private set; get; }

    public CompanyData(int id, string name, string countryCode, List<AppData> apps)
    {
        Id = id;
        Name = name;
        CountryCode = countryCode;
        this.apps = apps.ToDictionary(x => x.Name);
    }
}
