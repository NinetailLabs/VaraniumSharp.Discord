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
        #region Properties

        /// <inheritdoc />
        public string Token { get; set; }

        /// <inheritdoc />
        public TokenType TokenType { get; set; }

        #endregion
    }
}