using System.Net.Http;
using Jabot.Slack;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Jabot.Startup))]

namespace Jabot
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(new HttpClient());
            builder.Services.AddScoped<ISlackService, SlackService>();
        }
    }
}