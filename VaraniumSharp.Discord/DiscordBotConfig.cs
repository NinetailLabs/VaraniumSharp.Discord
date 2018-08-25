using System.Diagnostics.CodeAnalysis;
using Discord;
using VaraniumSharp.Attributes;
using VaraniumSharp.Discord.Interfaces;
using VaraniumSharp.Enumerations;

namespace VaraniumSharp.Discord
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    [AutomaticContainerRegistration(typeof(IDiscordBotConfig), ServiceReuse.Singleton)]
    public class DiscordBotConfig : IDiscordBotConfig
    {
        /// <summary>
        /// Set the configuration values
        /// </summary>
        /// <param name="token">Token value to use</param>
        /// <param name="tokenType">Type of token</param>
        public void ConfigureBot(string token, TokenType tokenType)
        {
            Token = token;
            TokenType = tokenType;
        }

        #region Properties

        /// <inheritdoc />
        public string Token { get; private set; }

        /// <inheritdoc />
        public TokenType TokenType { get; private set; }

        #endregion
    }
}