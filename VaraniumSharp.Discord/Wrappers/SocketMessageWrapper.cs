using Discord.Commands;
using Discord.WebSocket;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using VaraniumSharp.Discord.Interfaces;
using VaraniumSharp.Logging;

namespace VaraniumSharp.Discord.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class SocketMessageWrapper : ISocketMessageWrapper
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SocketMessageWrapper(IDiscordBotConfig botConfig)
        {
            _botConfig = botConfig;
            _logger = StaticLogger.GetLogger<SocketMessageWrapper>();
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public SocketMessage SocketMessage { get; set; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public SocketCommandContext GetCommandContextForUserMessage(IDiscordSocketClientWrapper discordSocketClient, out int argPos)
        {
            argPos = 0;
            if (!(SocketMessage is SocketUserMessage msg))
            {
                return null;
            }

            if ((_botConfig.AcceptedCharPrefix != '\0' && !msg.HasCharPrefix(_botConfig.AcceptedCharPrefix, ref argPos))
               || msg.HasMentionPrefix(discordSocketClient.CurrentUser, ref argPos))
            {
                _logger.LogDebug("Skipping {message}", msg.Content);
                return null;
            }

            return new SocketCommandContext(discordSocketClient.DiscordSocketClient, msg);
        }

        #endregion

        #region Variables

        /// <summary>
        /// BotConfig instance
        /// </summary>
        private readonly IDiscordBotConfig _botConfig;

        private readonly ILogger _logger;

        #endregion
    }
}