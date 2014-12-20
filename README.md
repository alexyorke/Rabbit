Rabbit [![Build status](https://ci.appveyor.com/api/projects/status/6fxlb8bkqp18cg3c/branch/master)](https://ci.appveyor.com/project/Decagon/rabbit/branch/master) [![Issue Stats](http://www.issuestats.com/github/decagon/rabbit/badge/pr)](http://www.issuestats.com/github/decagon/rabbit) [![Issue Stats](http://www.issuestats.com/github/decagon/rabbit/badge/issue)](http://www.issuestats.com/github/decagon/rabbit)
======

Rabbit allows PlayerIO developers to seamlessly integrate all supported types (Armor Games, Kongregate, Mousebreaker, Facebook, email, Yahoo*, and standard authentication) into one login interface. 

Want to use Rabbit in your project? It's simple!

```csharp
Connection conn = new RabbitAuth().LogOn(gameId, email, password);
```


The [PlayerIOClient (v3.0.14)](https://gamesnet.yahoo.com/download/) is a dependency. 

###Wiki

Have more questions? Feel free to consult the [Wiki](https://github.com/Decagon/Rabbit/wiki).


###Bugs

If you find a bug in Rabbit, feel free to let me know in the GitHub issue tracker or, if you don't have an account, by decagongithub@gmail.com.

###NuGet
Rabbit is available on NuGet: [EE-Rabbit](http://www.nuget.org/packages/EE-Rabbit/).

Just type `Install-Package EE-Rabbit` at the NuGet prompt to install.

###Credits
NuGet package icon courtesy of [jcapaldi on Flickr](https://flic.kr/p/cVkan9).

[Yonom](https://github.com/Yonom), author of [Cupcake](https://github.com/Yonom/CupCake), significantly [helped](https://github.com/Decagon/Rabbit/commits/master?author=Yonom)!

###Everybody Edits

Since Rabbit was originally created for Everybody Edits, use `EERabbitAuth()` to authenticate with EE:


```csharp
Connection conn = new EERabbitAuth().LogOn(EmailOrTokenOrUserName, RoomID, Password);
```
