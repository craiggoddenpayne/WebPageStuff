using System;
using System.Threading;
using System.Windows.Forms;

namespace WebManipulation
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var analytics = new WebPageTimingAnalytics();
            var timings = analytics.GetTimingsFor("http://www.swinton.co.uk");
            foreach (var timing in timings)
            {
                Console.WriteLine(timing.ResourceUrl + " took " + timing.TimeTaken + "ms and had a size of " + timing.Size + " bytes");
            }
            Console.ReadKey();
        }
    }
}
