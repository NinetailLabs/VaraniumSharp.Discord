# VaraniumSharp.Discord

[![Build status](https://ci.appveyor.com/api/projects/status/tmbabteki5t2jypu/branch/master?svg=true)](https://ci.appveyor.com/project/DeadlyEmbrace/varaniumsharp-discord/branch/master)
[![NuGet](https://img.shields.io/nuget/v/VaraniumSharp.Discord.svg)](https://www.nuget.org/packages/VaraniumSharp/)
[![Coverage Status](https://coveralls.io/repos/github/NinetailLabs/VaraniumSharp.Discord/badge.svg?branch=master)](https://coveralls.io/github/NinetailLabs/VaraniumSharp.Discord?branch=master)

VaraniumSharp.Discord is a VaraniumSharp add-on that wraps around [Discord.Net](https://www.nuget.org/packages/Discord.Net) that implements all logic required to manage a basic Discord socket bot.
This leaves only the creation of commands to the implementer without the bot management boilerplate code having to be rewritten.

## Requirements
- Dependency Injection container that implements IServiceProvider as it is used by the bot to resolve commands
- Implementer must set the `TokenType` and `Token` properties in the `DiscordBotConfig` class
- [Optional] Implement custom `IDiscordBotConfig` that loads config values from custom provider

## Logging
The framework uses the [VaraniumSharp](https://www.nuget.org/packages/VaraniumSharp/) `StaticLogger` for logging log messages from Discord. The `StaticLogger` 
uses [Microsoft.Extensions.Logging.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Abstractions/) for logging so any logger that has a provider 
for it (like [Serilog.Extensions.Logging](https://www.nuget.org/packages/Serilog.Extensions.Logging/)) can be used to get the log messages.