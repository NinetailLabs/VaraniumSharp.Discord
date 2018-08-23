using Discord.Commands;
using Discord.WebSocket;

namespace VaraniumSharp.Discord.Interfaces
{
    /// <summary>
    /// Wrapper for <see cref="SocketMessage"/>
    /// </summary>
    public interface ISocketMessageWrapper
    {
        /// <summary>
        /// Socket Message
        /// </summary>
        SocketMessage SocketMessage { get; set; }

        /// <summary>
        /// Return a <see cref="SocketCommandContext"/> for the SocketMessage
        /// </summary>
        /// <param name="discordSocketClient">DiscordClient for which the context should be returned</param>
        /// <param name="argPos">Argument position</param>
        /// <returns>The context unless the message is not a <see cref="SocketUserMessage"/> or it has a user mention or '!' prefix in which case null</returns>
        SocketCommandContext GetCommandContextForUserMessage(IDiscordSocketClientWrapper discordSocketClient, out int argPos);
    }
}