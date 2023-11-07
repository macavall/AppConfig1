using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AppConfig1
{
    public class http1
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationRefresherProvider _configurationRefresher;

        public http1(ILoggerFactory loggerFactory, IConfiguration configuration, IConfigurationRefresherProvider configurationRefresher)
        {
            _logger = loggerFactory.CreateLogger<http1>();
            _configuration = configuration;
            _configurationRefresher = configurationRefresher;
        }

        [Function("http1")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            string result = _configuration["TestApp:Settings:Message"] ;

            var refreshClass = new RefreshClass();

            await refreshClass.Init("test");

            string functionHostVersion = Environment.GetEnvironmentVariable("FUNCTIONS_EXTENSION_VERSION");

            _logger.LogInformation($"Host Version: {functionHostVersion}");

            response.WriteString(functionHostVersion != null ? functionHostVersion : "No Host Version Found");

            return response;
        }
    }
}
