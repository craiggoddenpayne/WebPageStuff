using System;
using System.Collections.Generic;
using System.Threading;
using Awesomium.Core;

namespace WebManipulation
{

    public class WebPageTimingAnalytics : IWebPageTimingAnalytics
    {
        public IEnumerable<ResourceTimingResult> GetTimingsFor(string url)
        {




            var results = new List<ResourceTimingResult>();
            using (WebView webView = WebCore.CreateWebView(1024, 768))
            {
                bool finishedLoading = false;
                WebCore.ResourceInterceptor = new RecordTimeResourceInterceptor
                {
                    RecordTimeOfResourceOccured = (result) =>
                    {
                        results.Add(result);
                    }
                };

                webView.Source = new Uri(url);
                webView.DocumentReady += (sender, e) =>
                {
                    WebCore.Shutdown();
                };
                WebCore.Run();
                return results;
            }
        }
    }
}