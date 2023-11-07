//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Azure.Identity;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.AzureAppConfiguration;
//using Microsoft.Extensions.Logging;

//namespace AppConfig1
//{
//    public class timer1
//    {
//        private readonly ILogger _logger;
//        private readonly IConfiguration _configuration;
//        private readonly IConfigurationRefresher _configurationRefresher;

//        public timer1(ILoggerFactory loggerFactory, IConfiguration configuration, IConfigurationRefresherProvider refresherProvider)
//        {
//            _logger = loggerFactory.CreateLogger<timer1>();
//            _configuration = configuration;
//            _configurationRefresher = refresherProvider.Refreshers.First();
//        }

//        [Function("timer1")]
//        public async Task Run([TimerTrigger("* */1 * * * *", RunOnStartup = true)] TimerInfo myTimer)
//        {
//            string result = _configuration["TestApp:Settings:Message"];

//            var refreshClass = new RefreshClass();

//            await refreshClass.Init("test");

//            await _configurationRefresher.TryRefreshAsync();

//            var cred = new DefaultAzureCredential();

//            _logger.LogInformation($"C# Timer trigger with Configuration: {result}");
//        }
//    }
//}
