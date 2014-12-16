Rabbit [![Build status](https://ci.appveyor.com/api/projects/status/6fxlb8bkqp18cg3c/branch/master)](https://ci.appveyor.com/project/Decagon/rabbit/branch/master)
======

Rabbit is still in beta and has some, well, issues https://github.com/Decagon/Rabbit/issues/23

Rabbit allows [Everybody Edits](http://everybodyedits.com) developers to seamlessly integrate (ArmorGames*, Kongregate, MouseBreaker, Facebook, and standard authentication) into one login interface. 

Want to use Rabbit in your project? It's simple!

```csharp
Connection conn = new RabbitAuth().LogOn(EmailOrTokenOrUserName, RoomID, Password);
```

Then initialize the connection with `conn.Send("init")` and `conn.Send("init2")`.

The [PlayerIOClient (v3.0.10)](https://gamesnet.yahoo.com/download/) is a dependency. 

Rabbit is available on NuGet under the [EE-Rabbit](http://www.nuget.org/packages/EE-Rabbit/) package.


##Credits
Rabbit icon (on NuGet) courtesy of [https://flic.kr/p/cVkan9](https://flic.kr/p/cVkan9).
