using Discord.Commands;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace VaraniumSharp.Discord.Interfaces
{
    /// <summary>
    /// Wrapper for <see cref="CommandService"/>
    /// </summary>
    public interface ICommandServiceWrapper
    {
        #region Properties

        /// <summary>
        /// Wrapped instance
        /// </summary>
        CommandService CommandService { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add modules containing the command
        /// </summary>
        /// <param name="assembly">Assembly where command should be retrieved from</param>
        Task AddModulesAsync(Assembly assembly);

        /// <summary>
        /// Executes a command
        /// </summary>
        /// <param name="context">Command context</param>
        /// <param name="argPos">Argument position</param>
        /// <param name="services">Service container</param>
        /// <param name="multiMatchHandling">How multiple matches should be handled</param>
        /// <returns>Result of the execution</returns>
        Task<IResult> ExecuteAsync(ICommandContext context, int argPos, IServiceProvider services = null,
            MultiMatchHandling multiMatchHandling = MultiMatchHandling.Exception);

        #endregion
    }
}