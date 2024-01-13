using System.Collections.Generic;

namespace Metrics_Analyzer.Processors;

static partial class AppProcessor
{
    const double Min = double.MinValue;

    // TODO: create json config file and add it as resource file to the app

    const double RiskScore_Payback_Coefficient = 0.7;
    const double RiskScore_LTVtoCAC_Coefficient = 0.3;

    static readonly KeyValuePair<double, double>[] RiskScore_Payback_Value =
    {
        new KeyValuePair<double, double>(Min, 100),
        new KeyValuePair<double, double>(7,    80),
        new KeyValuePair<double, double>(14,   60),
        new KeyValuePair<double, double>(21,   30),
        new KeyValuePair<double, double>(28,   10)
    };
    static readonly KeyValuePair<double, double>[] RiskScore_LTVtoCAC_Value =
    {
        new KeyValuePair<double, double>(Min,  10),
        new KeyValuePair<double, double>(1.5,  30),
        new KeyValuePair<double, double>(2.0,  60),
        new KeyValuePair<double, double>(2.5,  80),
        new KeyValuePair<double, double>(3.0, 100)
    };
    static readonly KeyValuePair<double, double>[] RiskRating_RiskScore_Value =
    {
        new KeyValuePair<double, double>(Min, 6),
        new KeyValuePair<double, double>(15,  5),
        new KeyValuePair<double, double>(25,  4),
        new KeyValuePair<double, double>(45,  3),
        new KeyValuePair<double, double>(65,  2),
        new KeyValuePair<double, double>(85,  1)
    };
    static Dictionary<double, string> RiskRatingTitle_Value = new()
    {
        { 1, "Undoubted" },
        { 2, "Low" },
        { 3, "Moderate" },
        { 4, "Cautionary" },
        { 5, "Unsatisfactory" },
        { 6, "Unacceptable" }
    };

    private static double ParseRange(double value, KeyValuePair<double, double>[] range)
    {
        for (var i = range.Length - 1; i >= 0; i--)
        {
            if (value >= range[i].Key)
                return range[i].Value;
        }
        return 0;
    }
}
