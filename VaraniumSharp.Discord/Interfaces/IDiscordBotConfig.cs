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
        /// Collection of character prefixes the bot looks for in messages (Set to \0 to ignore)
        /// </summary>
        char AcceptedCharPrefix { get; }

        /// <summary>
        /// Collection of string prefixes the bot looks for in messages
        /// </summary>
        string AcceptedStringPrefix { get; }

        /// <summary>
        /// Token for the Discord API
        /// </summary>
        string Token { get; }

        /// <summary>
        /// Type of token to use for login
        /// </summary>
        TokenType TokenType { get; }

        #endregion
    }
}