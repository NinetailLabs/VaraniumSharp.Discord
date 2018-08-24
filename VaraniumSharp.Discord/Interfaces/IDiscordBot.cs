using Discord.WebSocket;
using System.Reflection;
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
        /// Start the bot and install commands from the specified assembly if the commands have not been installed already
        /// </summary>
        /// <param name="assembly">Assembly where the commands are located. Pass null to use the executing Assembly</param>
        Task StartAsync(Assembly assembly);

        /// <summary>
        /// Stop the Discord client
        /// </summary>
        Task StopAsync();

        #endregion
    }
}