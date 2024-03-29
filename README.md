A .NET library to post a Discord bot's server count to various bot listing websites

https://www.nuget.org/packages/BotlistStatsPoster/

## Supported Bot Listing Websites

 * [top.gg](https://top.gg/)
 * [discord.bots.gg](https://discord.bots.gg/)
 * [discords.com](https://discords.com/)
 * [bots.ondiscord.xyz](https://bots.ondiscord.xyz/)
 * [discord.boats](https://discord.boats/)
 * [discordbotlist.com](https://discordbotlist.com/)
 * [discordlist.space](https://discordlist.space/)
 * [discordextremelist.xyz](https://discordextremelist.xyz/)
 * [voidbots.net](https://voidbots.net/)
 
## Usage

```csharp
// Create a StatsPoster with your bot's client id and tokens for bot listing sites
StatsPoster statsPoster = new StatsPoster(clientId, new TokenConfiguration
{
    Topgg = "token_here",
    DiscordBots = "token_here",
    ...
});

// Post the guild count to all bot listing sites which have a token set
await statsPoster.PostGuildCountAsync(guildCount);
```
