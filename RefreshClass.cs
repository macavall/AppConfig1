using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;

namespace AppConfig1
{

    public class RefreshClass
    {
        private readonly IServiceProvider _serviceProvider;

        public async Task Init(string input)
        {
            await RefreshConfiguration();
        }

        public async Task RefreshConfiguration()
        {
            await _serviceProvider.GetService<IConfigurationRefresherProvider>().Refreshers.First().RefreshAsync(default(CancellationToken));
        }
    }
}
