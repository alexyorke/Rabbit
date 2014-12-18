Rabbit [![Build status](https://ci.appveyor.com/api/projects/status/6fxlb8bkqp18cg3c/branch/master)](https://ci.appveyor.com/project/Decagon/rabbit/branch/master)
======

Rabbit allows [Everybody Edits](http://everybodyedits.com) developers to seamlessly integrate all supported types (Armor Games, Kongregate, Mousebreaker, Facebook, and standard authentication) into one login interface. 

Want to use Rabbit in your project? It's simple!

```csharp
Connection conn = new RabbitAuth().LogOn(EmailOrTokenOrUserName, RoomID, Password);
```
*Note: the password is not required for some authentication types and can be ommitted.*

Initialize the connection with `conn.Send("init")` and `conn.Send("init2")` (as normal).

The [PlayerIOClient (v3.0.14)](https://gamesnet.yahoo.com/download/) is a dependency. 

###How does Rabbit work?

Rabbit works by identifying common patterns in the ids and passwords, which are commonly associated with authenticating with a specific service. These patterns can be made into rules, which then can be easily followed and therefore, automatically redirects the request to the service. Finally, the user is authenticated using a unified connection handler.

###Bugs

If you find a bug in Rabbit, feel free to let me know in the GitHub issue tracker or, if you don't have an account, by decagongithub@gmail.com

###NuGet
Rabbit is available on NuGet: [EE-Rabbit](http://www.nuget.org/packages/EE-Rabbit/).


###Credits
Rabbit icon (on NuGet) courtesy of [https://flic.kr/p/cVkan9](https://flic.kr/p/cVkan9).

[Yonom](https://github.com/Yonom), author of [Cupcake](https://github.com/Yonom/CupCake), significantly [helped](https://github.com/Decagon/Rabbit/commits/master?author=Yonom)!
