using Discord;

namespace VaraniumSharp.Discord.Interfaces
{
    /// <summary>
    /// Configuration for DiscordBot
    /// </summary>
    public interface IDiscordBotConfig
    {
        #region Properties

        /// <summary>
        /// Token for the Discord API
        /// </summary>
        string Token { get; set; }

        /// <summary>
        /// Type of token to use for login
        /// </summary>
        TokenType TokenType { get; set; }

        #endregion
    }
}