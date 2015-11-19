# RabbitIO: Smart PlayerIO authentication

Rabbit is a library written in C#, and allows PlayerIO integration of Armor Games, Kongregate, Mousebreaker, Facebook, email, Yahoo, and standard authentication into one login interface. Give one username and password UI to your users, and Rabbit will know what service to authenticate them into.

## Usage

After you have installed [`RabbitIO` from NuGet](https://www.nuget.org/packages/RabbitIO/), including Rabbit is easy:

```csharp
Connection conn = new RabbitAuth().LogOn(gameId, email, passwordOrToken);
```

If you prefer to skip the authentication check, you can pass in the AuthenticationType manually (for example Kongregate):

```csharp
Connection conn = new RabbitAuth(Rabbit.Auth.AuthenticationType = Rabbit.Auth.AuthenticationType.Kongregate).LogOn(gameId, email, token);
```

###Everybody Edits

Use `EERabbitAuth()` to authenticate with Everybody Edits:


```csharp
Connection conn = new EERabbitAuth().LogOn(EmailOrTokenOrUserName, RoomID, Password);
```

## Project Goals

 * Well maintained
 * Well documented
 * Well tested
 * Maintain backwards compatibility*


## Project Maturity

Rabbit is a new library, and is used in CupCake, Terra, and Skylight, which together power the majority of the bots on Everybody Edits. If you want to use Rabbit in your project, use the stable version 0.9.3.3 or wait until 1.0 is released. The public API will freeze within 1.0.

### Supported Features

 * Support for Armor Games, Kongregate, Mousebreaker, Facebook, and standard authentication
 * Support for PlayerIO `useSecureApiRequests`
 * Smart error detection and recovery
 * Fast: queries are categorized in milliseconds
 * Many localizations
 * Compatibility with Terra


## Getting Started

Over on the wiki, have a look at our [Getting Started](https://github.com/Decagon/Rabbit/wiki/Getting-Started) guide. Send questions to decagongithub@gmail.com, and report any issues to the [GitHub Issue Tracker](https://github.com/Decagon/Rabbit/issues)


## Documentation & Examples

Rabbit has [documentation](https://github.com/Decagon/Rabbit/wiki/) available on GitHub.

For more samples, have a look at [Rabbit.Tests](https://github.com/Decagon/Rabbit/tree/master/Rabbit.Tests)

## Requirements

Rabbit requires .NET framework 3.5 or later.


## Credits

[Yonom](https://github.com/Yonom), author of [Cupcake](https://github.com/Yonom/CupCake), significantly [helped](https://github.com/Decagon/Rabbit/commits/master?author=Yonom)!


## Continuous Integration

[![Build status](https://ci.appveyor.com/api/projects/status/6fxlb8bkqp18cg3c/branch/master)](https://ci.appveyor.com/project/Decagon/rabbit/branch/master)


## License

Copyright (C) 2014-2015 under the MIT license.

