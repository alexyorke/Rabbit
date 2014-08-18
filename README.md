Rabbit [![Build status](https://ci.appveyor.com/api/projects/status/6fxlb8bkqp18cg3c/branch/master)](https://ci.appveyor.com/project/Decagon/rabbit/branch/master)
======

Supporting ArmorGames, Kongregate, MouseBreaker, Facebook, username and email authentication mechanisms Rabbit allows [Everybody Edits](http://everybodyedits.com) developers to seamlessly integrate many forms of authentication in one interface. Rabbit detects the type of input and authenticates users to the appropriate service.

To use Rabbit, type:

```csharp
Connection conn = new RabbitAuth().LogOn(EmailOrTokenOrUserName, RoomID, Password);
```

The [PlayerIOClient (v3.0.10)](https://gamesnet.yahoo.com/download/) is also a dependency (it is not bundled within the Rabbit dll). Then initialize the connection with `conn.Send("init")` and `conn.Send("init2")`.

Rabbit is available on NuGet under the [EE-Rabbit](http://www.nuget.org/packages/EE-Rabbit/) package.

Current version: v0.7.5


Rabbit icon (on NuGet) by [https://flic.kr/p/cVkan9](https://flic.kr/p/cVkan9).
