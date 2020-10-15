using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using Codecov.Coverage.Report;
using Codecov.Url;
using Serilog;

namespace Codecov.Upload
{
    internal class CodecovUploader : Upload
    {
#pragma warning disable DF0025 // Marks undisposed objects assinged to a field, originated from method invocation.
        private static HttpClient _client = CreateHttpClient();
#pragma warning restore DF0025 // Marks undisposed objects assinged to a field, originated from method invocation.

        public CodecovUploader(IUrl url, IReport report)
            : base(url, report)
        {
        }

        public static void Cleanup()
        {
            _client?.Dispose();
            _client = null;
        }

        protected virtual void ConfigureContent(HttpContent content)
        {
            content.Headers.ContentEncoding.Clear();
            content.Headers.ContentEncoding.Add("gzip");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-gzip");
        }

        protected virtual void ConfigureRequest(HttpRequestMessage request)
        {
        }

        protected virtual HttpResponseMessage CreateResponse(HttpRequestMessage request)
        {
            ConfigureRequest(request);

            if (request.Content is object)
            {
                request.Content.Dispose();
            }

            request.Content = new ByteArrayContent(GetReportBytes());

            ConfigureContent(request.Content);

            var response = _client.SendAsync(request).Result;
            return response;
        }

        protected override string Post()
        {
            Log.Verbose("Trying to upload using HttpClient");
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), Url.GetUrl))
            {
                request.Headers.TryAddWithoutValidation("X-Reduced-Redundancy", "false");
                request.Headers.TryAddWithoutValidation("X-Content-Type", "application/x-gzip");

                Log.Information("Pinging Codecov");
                var response = _client.SendAsync(request).Result;
                if (!response.IsSuccessStatusCode)
                {
                    ReportFailure(response);

                    return string.Empty;
                }

                return response.Content.ReadAsStringAsync().Result;
            }
        }

        protected override bool Put(Uri url)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("PUT"), url))
            {
                Log.Information("Uploading");
                using (var response = CreateResponse(request))
                {
                    var success = response.IsSuccessStatusCode;

                    if (!success)
                    {
                        ReportFailure(response);
                    }

                    return success;
                }
            }
        }

        protected void ReportFailure(HttpResponseMessage message)
        {
            Log.Warning("Unable to upload coverage report to Codecov. Server returned: ({StatusCode}) {ReasonPhrase}", (int)message.StatusCode, message.ReasonPhrase);
            Log.Warning($"Unable to upload coverage report to Codecov. Server returned: ({(int)message.StatusCode}) {message.ReasonPhrase}");

            if (string.Equals(message.Content.Headers.ContentType.MediaType, "text/plain", StringComparison.OrdinalIgnoreCase))
            {
                Log.Warning(message.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Log.Warning("Unknown reason. Possible reason being invalid parameters.");
            }
        }

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd($"codecove-exe/{Assembly.GetExecutingAssembly().GetName().Version}");
            return client;
        }

        private byte[] GetReportBytes()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var stream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    var content = Encoding.UTF8.GetBytes(Report.Reporter);
                    stream.Write(content, 0, content.Length);
                }

                return memoryStream.ToArray();
            }
        }
    }
}
