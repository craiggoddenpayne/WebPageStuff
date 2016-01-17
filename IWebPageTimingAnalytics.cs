using System.Collections.Generic;

namespace WebManipulation
{
    public interface IWebPageTimingAnalytics
    {
        IEnumerable<ResourceTimingResult> GetTimingsFor(string url);
    }
}