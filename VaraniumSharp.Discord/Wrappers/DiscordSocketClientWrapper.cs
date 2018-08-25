using Discord;
using Discord.WebSocket;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using VaraniumSharp.Attributes;
using VaraniumSharp.Discord.Interfaces;

namespace VaraniumSharp.Discord.Wrappers
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    [AutomaticContainerRegistration(typeof(IDiscordSocketClientWrapper))]
    public class DiscordSocketClientWrapper : IDiscordSocketClientWrapper
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DiscordSocketClientWrapper(IDiscordBotConfig botConfig)
        {
            _botConfig = botConfig;
            DiscordSocketClient = new DiscordSocketClient();
            DiscordSocketClient.Log += DiscordSocketClientOnLog;
            DiscordSocketClient.MessageReceived += DiscordSocketClientOnMessageReceived;
        }

        #endregion

        #region Events

        /// <inheritdoc />
        public event EventHandler<LogMessage> Log;

        /// <inheritdoc />
        public event EventHandler<ISocketMessageWrapper> MessageReceived;

        #endregion

        #region Properties

        /// <inheritdoc />
        public SocketSelfUser CurrentUser => DiscordSocketClient.CurrentUser;

        /// <inheritdoc />
        public DiscordSocketClient DiscordSocketClient { get; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public async Task LoginAsync()
        {
            await DiscordSocketClient.LoginAsync(_botConfig.TokenType, _botConfig.Token);
        }

        /// <inheritdoc />
        public async Task StartAsync()
        {
            await DiscordSocketClient.StartAsync();
        }

        /// <inheritdoc />
        public async Task StopAsync()
        {
            await DiscordSocketClient.StopAsync();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Occurs when Discord sent a log message
        /// </summary>
        /// <param name="arg">The message to log</param>
        private Task DiscordSocketClientOnLog(LogMessage arg)
        {
            Log?.Invoke(this, arg);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Occurs when a message was received from Discord
        /// </summary>
        /// <param name="arg">Received message</param>
        private Task DiscordSocketClientOnMessageReceived(SocketMessage arg)
        {
            MessageReceived?.Invoke(this, new SocketMessageWrapper(_botConfig) { SocketMessage = arg });
            return Task.CompletedTask;
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