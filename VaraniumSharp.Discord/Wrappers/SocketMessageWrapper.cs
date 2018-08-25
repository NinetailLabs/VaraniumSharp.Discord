using Discord.Commands;
using Discord.WebSocket;
using System.Diagnostics.CodeAnalysis;
using VaraniumSharp.Discord.Interfaces;

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
                || (!string.IsNullOrEmpty(_botConfig.AcceptedStringPrefix) && !msg.HasStringPrefix(_botConfig.AcceptedStringPrefix, ref argPos))
                || !msg.HasMentionPrefix(discordSocketClient.CurrentUser, ref argPos))
            {
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

        #endregion
    }
}