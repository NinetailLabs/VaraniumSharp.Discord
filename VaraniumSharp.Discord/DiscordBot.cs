using Discord;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using VaraniumSharp.Attributes;
using VaraniumSharp.Discord.Interfaces;

namespace VaraniumSharp.Discord
{
    /// <inheritdoc />
    [AutomaticContainerRegistration(typeof(IDiscordBot))]
    public class DiscordBot : IDiscordBot
    {
        #region Constructor

        /// <summary>
        /// DI Constructor
        /// </summary>
        public DiscordBot(IDiscordSocketClientWrapper discordSocketClient, ICommandServiceWrapper commandService,
            IServiceProvider serviceProvider)
        {
            _discordSocketClient = discordSocketClient;
            _commandService = commandService;
            _serviceProvider = serviceProvider;

            _discordSocketClient.Log += DiscordSocketClientOnLog;
            _discordSocketClient.MessageReceived += DiscordSocketClientOnMessageReceived;
            _logger = Logging.StaticLogger.GetLogger<DiscordBot>();
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public async Task StartAsync()
        {
            await StartAsync(null);
        }

        /// <inheritdoc />
        public async Task StartAsync(Assembly assembly)
        {
            _logger.LogDebug("DiscordBot starting");
            await InstallModulesAsync(assembly ?? Assembly.GetExecutingAssembly());
            await _discordSocketClient.LoginAsync();
            await _discordSocketClient.StartAsync();
        }

        /// <inheritdoc />
        public async Task StopAsync()
        {
            await _discordSocketClient.StopAsync();
            _logger.LogDebug("Client stopped");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Occurs when <see cref="_discordSocketClient"/> sends a log event
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="logMessage">The message to log</param>
        private void DiscordSocketClientOnLog(object sender, LogMessage logMessage)
        {
            switch (logMessage.Severity)
            {
                case LogSeverity.Critical:
                    _logger.LogCritical(logMessage.Exception, logMessage.Message);
                    break;

                case LogSeverity.Error:
                    _logger.LogError(logMessage.Exception, logMessage.Message);
                    break;

                case LogSeverity.Warning:
                    _logger.LogWarning(logMessage.Exception, logMessage.Message);
                    break;

                case LogSeverity.Info:
                    _logger.LogInformation(logMessage.Exception, logMessage.Message);
                    break;

                case LogSeverity.Verbose:
                    _logger.LogTrace(logMessage.Message);
                    break;

                case LogSeverity.Debug:
                    _logger.LogDebug(logMessage.Message);
                    break;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="_discordSocketClient"/> receives a message
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="socketMessage">Message received from the socket</param>
        private async void DiscordSocketClientOnMessageReceived(object sender, ISocketMessageWrapper socketMessage)
        {
            var context = socketMessage.GetCommandContextForUserMessage(_discordSocketClient, out var argPos);
            if (context == null)
            {
                return;
            }

            var result = await _commandService.ExecuteAsync(context, argPos, _serviceProvider);
            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        /// <summary>
        /// Install modules for <see cref="_commandService"/>
        /// </summary>
        /// <param name="assemblyContainingCommands">Assembly where bot commands are located</param>
        private async Task InstallModulesAsync(Assembly assemblyContainingCommands)
        {
            try
            {
                await _moduleLock.WaitAsync();
                if (_modulesRegistered)
                {
                    return;
                }

                _logger.LogDebug("Registering command from {Assembly}", assemblyContainingCommands);
                await _commandService.AddModulesAsync(assemblyContainingCommands, _serviceProvider);
                _modulesRegistered = true;
            }
            finally
            {
                _moduleLock.Release();
            }
        }

        #endregion

        #region Variables

        /// <summary>
        /// CommandService instance
        /// </summary>
        private readonly ICommandServiceWrapper _commandService;

        /// <summary>
        /// DiscordSocketClient instance
        /// </summary>
        private readonly IDiscordSocketClientWrapper _discordSocketClient;

        /// <summary>
        /// Logger instance
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Semaphore used to lock module installation method to prevent multiple executions
        /// </summary>
        private readonly SemaphoreSlim _moduleLock = new SemaphoreSlim(1);

        /// <summary>
        /// ServiceProvider instance
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Indicate if modules are already registered with the <see cref="_commandService"/>
        /// </summary>
        private bool _modulesRegistered;

        #endregion
    }
}