using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using VaraniumSharp.Discord.Wrappers;

namespace VaraniumSharp.Discord.Interfaces
{
    /// <summary>
    /// Wrapper for <see cref="DiscordSocketClient"/> making it possible to create interfaces for testing
    /// </summary>
    public interface IDiscordSocketClientWrapper
    {
        #region Events

        /// <summary>
        /// Fired when Discord has sent a log message
        /// </summary>
        event EventHandler<LogMessage> Log;

        /// <summary>
        /// Fired when a message was received from Discord
        /// </summary>
        event EventHandler<ISocketMessageWrapper> MessageReceived;

        #endregion

        #region Properties

        /// <summary>
        /// The current user
        /// </summary>
        SocketSelfUser CurrentUser { get; }

        /// <summary>
        /// DiscordSocketClient instance
        /// </summary>
        DiscordSocketClient DiscordSocketClient { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Logs the bot into Discord
        /// <remarks>
        /// While the <see cref="DiscordSocketClientWrapper.DiscordSocketClient"/> provides parameters the wrapper uses the settings in <see cref="IDiscordBotConfig"/> to handle login
        /// </remarks>
        /// </summary>
        Task LoginAsync();

        /// <summary>
        /// Start the bot
        /// </summary>
        Task StartAsync();

        /// <summary>
        /// Stop the bot
        /// </summary>
        Task StopAsync();

        #endregion
    }
}