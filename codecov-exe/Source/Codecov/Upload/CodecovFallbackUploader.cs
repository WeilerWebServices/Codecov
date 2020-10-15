using System;
using System.Net.Http;
using Codecov.Coverage.Report;
using Codecov.Url;
using Serilog;

namespace Codecov.Upload
{
    internal sealed class CodecovFallbackUploader : CodecovUploader
    {
        public CodecovFallbackUploader(IUrl url, IReport report)
            : base(url, report)
        {
        }

        protected override void ConfigureContent(HttpContent content)
        {
            content.Headers.ContentEncoding.Clear();
            content.Headers.ContentEncoding.Add("gzip");
        }

        protected override void ConfigureRequest(HttpRequestMessage request)
        {
            request.Headers.Accept.Clear();
            request.Headers.Accept.ParseAdd("text/plain");
            request.Headers.Add("X-Content-Encoding", "gzip");
        }

        protected override string Post()
            => Url.GetFallbackUrl.ToString();

        protected override bool Put(Uri url)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), url))
            {
                Log.Information("Uploading to Codecov");
                using (var response = CreateResponse(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        Log.Information("View reports at: {GetReportUrl}", GetReportUrl(content));
                    }
                    else
                    {
                        ReportFailure(response);
                    }

                    return response.IsSuccessStatusCode;
                }
            }
        }

        private static string GetReportUrl(string content)
        {
            // The report url is expected to be the second line in the content response
            var splitResponse = content.Split('\n');
            return splitResponse[splitResponse.Length > 1 ? 1 : 0];
        }
    }
}
