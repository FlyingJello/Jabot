using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Jabot.Slack
{
    public class SlackService : ISlackService
    {
        private const string PostMessageUrl = "https://slack.com/api/chat.postMessage";

        private readonly string _botToken;

        public SlackService()
        {
            _botToken = Environment.GetEnvironmentVariable("BOT_TOKEN");
        }

        public async Task BroadcastMessage(string channel, string message)
        {
            var payload = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("token", _botToken),
                new KeyValuePair<string, string>("channel", channel),
                new KeyValuePair<string, string>("text", message)
            };

            using var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, PostMessageUrl) { Content = new FormUrlEncodedContent(payload) };
            await httpClient.SendAsync(request);
        }
    }
}
