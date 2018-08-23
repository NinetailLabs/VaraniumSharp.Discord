using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using VaraniumSharp.Discord.Interfaces;
using VaraniumSharp.Discord.Wrappers;

namespace VaraniumSharp.Discord.Tests
{
    public class DiscordBotTests
    {
        [Test]
        public async Task StartingTheBotInvokesAllRequiredMethods()
        {
            // arrange
            var fixture = new DiscordBotFixture();

            var sut = fixture.GetInstance();
            
            // act
            await sut.StartAsync();

            // assert
            fixture.CommandServiceWrapperMock.Verify(t => t.AddModulesAsync(It.IsAny<Assembly>()), Times.Once());
            fixture.DiscordSocketClientWrapperMock.Verify(t => t.LoginAsync(), Times.Once);
            fixture.DiscordSocketClientWrapperMock.Verify(t => t.StartAsync(), Times.Once);
        }

        [Test]
        public async Task StartingTheBotMoreThanOnceWillOnlyInvokeModuleSetupOnce()
        {
            // arrange
            var fixture = new DiscordBotFixture();

            var sut = fixture.GetInstance();

            // act
            await sut.StartAsync();
            await sut.StartAsync();

            // assert
            fixture.CommandServiceWrapperMock.Verify(t => t.AddModulesAsync(It.IsAny<Assembly>()), Times.Once());
        }

        [Test]
        public async Task StoppingTheBotInvokesTheStopMethodOnTheClient()
        {
            // arrange
            var fixture = new DiscordBotFixture();

            var sut = fixture.GetInstance();

            // act
            await sut.StopAsync();

            // assert
            fixture.DiscordSocketClientWrapperMock.Verify(t => t.StopAsync(), Times.Once);
        }

        [Test]
        public void MessagesOtherThanSocketUserMessageIsNotHandled()
        {
            // arrange
            var fixture = new DiscordBotFixture();
            var messageDummy = new SocketMessageWrapper();

            var _ = fixture.GetInstance();

            // act
            fixture.DiscordSocketClientWrapperMock.Raise(x => x.MessageReceived += null, null, messageDummy);

            // assert
            fixture.CommandServiceWrapperMock.Verify(t => t.ExecuteAsync(It.IsAny<ICommandContext>(), It.IsAny<int>(), null, MultiMatchHandling.Exception), Times.Never);
        }
        
        [TestCase(LogSeverity.Critical, LogLevel.Critical)]
        [TestCase(LogSeverity.Error, LogLevel.Error)]
        [TestCase(LogSeverity.Warning, LogLevel.Warning)]
        [TestCase(LogSeverity.Info, LogLevel.Information)]
        [TestCase(LogSeverity.Verbose, LogLevel.Trace)]
        [TestCase(LogSeverity.Debug, LogLevel.Debug)]
        public void DiscordMessagesAreCorrectlyLogger(LogSeverity severity, LogLevel expectedLevel)
        {
            // arrange
            const string logMessage = "Test log";
            var fixture = new DiscordBotFixture();
            var logMessageDummy = new LogMessage(severity, "", logMessage);
            var logProviderDummy = new Mock<ILoggerProvider>();
            var loggerDummy = new LoggerFixture();

            logProviderDummy
                .Setup(t => t.CreateLogger(It.IsAny<string>()))
                .Returns(loggerDummy);
            
            Logging.StaticLogger.LoggerFactory.AddProvider(logProviderDummy.Object);

            var _ = fixture.GetInstance();

            // act
            fixture.DiscordSocketClientWrapperMock.Raise(x => x.Log += null, null, logMessageDummy);

            // assert
            loggerDummy.LoggedMessages.Count.Should().Be(1);
            loggerDummy.LoggedMessages.First().message.Should().Be(logMessage);
            loggerDummy.LoggedMessages.First().level.Should().Be(expectedLevel);
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local", Justification = "Test Fixture - Unit tests require access to Mocks")]
        private class DiscordBotFixture
        {
            #region Properties

            public ICommandServiceWrapper CommandServiceWrapper => CommandServiceWrapperMock.Object;
            public Mock<ICommandServiceWrapper> CommandServiceWrapperMock { get; } = new Mock<ICommandServiceWrapper>();

            public IDiscordSocketClientWrapper DiscordSocketClientWrapper => DiscordSocketClientWrapperMock.Object;
            public Mock<IDiscordSocketClientWrapper> DiscordSocketClientWrapperMock { get; } = new Mock<IDiscordSocketClientWrapper>();
            public IServiceProvider ServiceProvider => ServiceProviderMock.Object;
            public Mock<IServiceProvider> ServiceProviderMock { get; } = new Mock<IServiceProvider>();

            #endregion

            #region Public Methods

            public DiscordBot GetInstance()
            {
                return new DiscordBot(DiscordSocketClientWrapper, CommandServiceWrapper, ServiceProvider);
            }

            #endregion
        }
    }
}