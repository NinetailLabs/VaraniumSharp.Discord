using Discord.WebSocket;
using System.Threading.Tasks;

namespace VaraniumSharp.Discord.Interfaces
{
    /// <summary>
    /// Basic <see cref="DiscordSocketClient"/> wrapper that sets up all basic logic needed to create and operate a Discord bot
    /// </summary>
    public interface IDiscordBot
    {
        #region Public Methods

        /// <summary>
        /// Start the Discord client
        /// </summary>
        Task StartAsync();

        /// <summary>
        /// Stop the Discord client
        /// </summary>
        Task StopAsync();

        #endregion
    }
}