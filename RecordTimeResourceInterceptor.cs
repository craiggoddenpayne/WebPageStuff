using System;
using System.IO;
using System.Net;
using Awesomium.Core;

namespace WebManipulation
{
   
    public class RecordTimeResourceInterceptor : IResourceInterceptor
    {
        public Action<ResourceTimingResult> RecordTimeOfResourceOccured;
        
        public bool OnFilterNavigation(NavigationRequest request)
        {
            return false;
        }

        public ResourceResponse OnRequest(ResourceRequest request)
        {
            var webRequest = HttpWebRequest.CreateHttp(request.Url);
            webRequest = request.ToWebRequest(webRequest);
            Stream stream = null;
            var startTime = DateTime.Now;
            try
            {
                var response = webRequest.GetResponse();
                stream = response.GetResponseStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            var streamData = stream.ReadFully();
            var endTime = DateTime.Now;

            var timeTaken = (endTime - startTime).TotalMilliseconds;
            var filename = Guid.NewGuid().ToString();
            File.WriteAllBytes(filename, streamData);

            RecordTimeOfResourceOccured.Invoke(new ResourceTimingResult
            {
                ResourceUrl = request.Url.ToString(),
                TimeTaken = timeTaken,
                Size = streamData.Length,
            });
            return ResourceResponse.Create(filename);
        }
    }
}