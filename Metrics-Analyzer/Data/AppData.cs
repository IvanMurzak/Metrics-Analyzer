namespace Metrics_Analyzer.Data;

public class AppData
{
    public string Name { private set; get; }
    public List<AppTimestampData> Timestamps { private set; get; }

    public AppData(string name, List<AppTimestampData> timestamps)
    {
        Name = name;
        Timestamps = timestamps;
    }
}
