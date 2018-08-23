using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace VaraniumSharp.Discord.Tests
{
    public class LoggerFixture : ILogger
    {
        public List<(LogLevel level, string message)> LoggedMessages { get; } = new List<(LogLevel level, string message)>();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter.Invoke(state, exception);
            LoggedMessages.Add(new ValueTuple<LogLevel, string>(logLevel, message));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}