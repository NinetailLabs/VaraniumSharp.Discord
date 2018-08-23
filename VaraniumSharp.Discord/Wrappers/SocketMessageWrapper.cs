using Discord.Commands;
using Discord.WebSocket;
using System.Diagnostics.CodeAnalysis;
using VaraniumSharp.Discord.Interfaces;

namespace VaraniumSharp.Discord.Wrappers
{
    
    [ExcludeFromCodeCoverage]
    public class SocketMessageWrapper : ISocketMessageWrapper
    {
        /// <inheritdoc />
        public SocketMessage SocketMessage { get; set; }

        /// <inheritdoc />
        public SocketCommandContext GetCommandContextForUserMessage(IDiscordSocketClientWrapper discordSocketClient, out int argPos)
        {
            argPos = 0;
            if (!(SocketMessage is SocketUserMessage msg))
            {
                return null;
            }

            if (!msg.HasCharPrefix('!', ref argPos)
                || msg.HasMentionPrefix(discordSocketClient.CurrentUser, ref argPos))
            {
                return null;
            }

            return new SocketCommandContext(discordSocketClient.DiscordSocketClient, msg);
        }
    }
}