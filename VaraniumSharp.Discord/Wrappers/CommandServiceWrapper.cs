using Discord.Commands;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using VaraniumSharp.Attributes;
using VaraniumSharp.Discord.Interfaces;

namespace VaraniumSharp.Discord.Wrappers
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    [AutomaticContainerRegistration(typeof(ICommandServiceWrapper))]
    public class CommandServiceWrapper : ICommandServiceWrapper
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CommandServiceWrapper()
        {
            CommandService = new CommandService();
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public CommandService CommandService { get; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public async Task AddModulesAsync(Assembly assembly)
        {
            await CommandService.AddModulesAsync(assembly);
        }

        /// <inheritdoc />
        public async Task<IResult> ExecuteAsync(ICommandContext context, int argPos, IServiceProvider services = null,
            MultiMatchHandling multiMatchHandling = MultiMatchHandling.Exception)
        {
            return await CommandService.ExecuteAsync(context, argPos, services, multiMatchHandling);
        }

        #endregion
    }
}