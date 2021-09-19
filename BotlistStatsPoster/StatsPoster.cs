using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace BotlistStatsPoster
{
    public class StatsPoster
    {
        private ulong _clientId;
        private TokenConfiguration _tokenConfiguration;

        /// <summary>Create an API client which you can use to post your guild count to bot listing websites</summary>
        /// <param name="clientId">Your bot's client id</param>
        /// <param name="tokenConfiguration">The API tokens for the bot listing websites</param>
        public StatsPoster(ulong clientId, TokenConfiguration tokenConfiguration)
        {
            _clientId = clientId;
            _tokenConfiguration = tokenConfiguration;
        }

        /// <summary>Sends a POST request to all bot listing sites that you have specified a token for to update your guild count
        /// <para>To avoid hitting ratelimits, do not call this method more than once every few minutes.</para>
        /// </summary>
        public async Task PostGuildCountAsync(int guildCount)
        {
            var tasks = new List<Task>();

            if (!string.IsNullOrEmpty(_tokenConfiguration.Topgg)) tasks.Add(PostToTopggAsync(guildCount));
            if (!string.IsNullOrEmpty(_tokenConfiguration.DiscordBots)) tasks.Add(PostToDiscordBotsAsync(guildCount));
            if (!string.IsNullOrEmpty(_tokenConfiguration.Discords)) tasks.Add(PostToBotsForDiscordAsync(guildCount));
            if (!string.IsNullOrEmpty(_tokenConfiguration.BotsOnDiscord)) tasks.Add(PostToBotsOnDiscordAsync(guildCount));
            if (!string.IsNullOrEmpty(_tokenConfiguration.DiscordBoats)) tasks.Add(PostToDiscordBoatsAsync(guildCount));
            if (!string.IsNullOrEmpty(_tokenConfiguration.DiscordBotList)) tasks.Add(PostToDiscordBotListAsync(guildCount));
            if (!string.IsNullOrEmpty(_tokenConfiguration.BotlistSpace)) tasks.Add(PostToBotlistSpaceAsync(guildCount));
            if(!string.IsNullOrEmpty(_tokenConfiguration.DiscordExtremeList)) tasks.Add(PostToDiscordExtremeListAsync(guildCount));
            if(!string.IsNullOrEmpty(_tokenConfiguration.VoidBots)) tasks.Add(PostToVoidBotsAsync(guildCount));
            
            await Task.WhenAll(tasks);
        }

        private async Task PostToTopggAsync(int guildCount)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenConfiguration.Topgg);
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("server_count", guildCount.ToString())
            });

            var result = await client.PostAsync($"https://top.gg/api/bots/{_clientId}/stats", content);
            result.EnsureSuccessStatusCode();
        }

        private async Task PostToDiscordBotsAsync(int guildCount)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_tokenConfiguration.DiscordBots);
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("guildCount", guildCount.ToString())
            });

            var result = await client.PostAsync($"https://discord.bots.gg/api/v1/bots/{_clientId}/stats", content);
            result.EnsureSuccessStatusCode();
        }

        private async Task PostToBotsForDiscordAsync(int guildCount)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_tokenConfiguration.Discords);
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("server_count", guildCount.ToString())
            });
            
            var result = await client.PostAsync($"https://discords.com/bots/api/bot/{_clientId}", content);
            result.EnsureSuccessStatusCode();
        }

        private async Task PostToBotsOnDiscordAsync(int guildCount)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_tokenConfiguration.BotsOnDiscord);
            var content = new StringContent($"{{ \"guildCount\": {guildCount} }}", Encoding.UTF8, "application/json");

            var result = await client.PostAsync($"https://bots.ondiscord.xyz/bot-api/bots/{_clientId}/guilds", content);
            result.EnsureSuccessStatusCode();
        }

        private async Task PostToDiscordBoatsAsync(int guildCount)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_tokenConfiguration.DiscordBoats);
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("server_count", guildCount.ToString())
            });

            var result = await client.PostAsync($"https://discord.boats/api/bot/{_clientId}", content);
            result.EnsureSuccessStatusCode();
        }

        private async Task PostToDiscordBotListAsync(int guildCount)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_tokenConfiguration.DiscordBotList);
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("guilds", guildCount.ToString())
            });

            var result = await client.PostAsync($"https://discordbotlist.com/api/v1/bots/{_clientId}/stats", content);
            result.EnsureSuccessStatusCode();
        }

        private async Task PostToBotlistSpaceAsync(int guildCount)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_tokenConfiguration.BotlistSpace);
            var content = new StringContent($"{{ \"serverCount\": {guildCount} }}", Encoding.UTF8, "application/json");

            var result = await client.PostAsync($"https://api.discordlist.space/v2/bots/{_clientId}", content);
            result.EnsureSuccessStatusCode();
        }

        private async Task PostToDiscordExtremeListAsync(int guildCount)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_tokenConfiguration.DiscordExtremeList);
            var content = new StringContent($"{{ \"guildCount\": {guildCount} }}", Encoding.UTF8, "application/json");

            var result = await client.PostAsync($"https://api.discordextremelist.xyz/v2/bot/{_clientId}/stats", content);
            result.EnsureSuccessStatusCode();
        }
        
        private async Task PostToVoidBotsAsync(int guildCount)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_tokenConfiguration.VoidBots);
            var content = new StringContent($"{{ \"server_count\": {guildCount} }}", Encoding.UTF8, "application/json");

            var result = await client.PostAsync($"https://api.voidbots.net/bot/stats/{_clientId}", content);
            result.EnsureSuccessStatusCode();
        }
    }
}
