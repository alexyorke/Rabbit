Rabbit [![Build status](https://ci.appveyor.com/api/projects/status/6fxlb8bkqp18cg3c/branch/master)](https://ci.appveyor.com/project/Decagon/rabbit/branch/master) [![Issue Stats](http://www.issuestats.com/github/decagon/rabbit/badge/pr)](http://www.issuestats.com/github/decagon/rabbit) [![Issue Stats](http://www.issuestats.com/github/decagon/rabbit/badge/issue)](http://www.issuestats.com/github/decagon/rabbit)
======

Rabbit allows PlayerIO developers to seamlessly integrate all supported types (Armor Games, Kongregate, Mousebreaker, Facebook, email, Yahoo*, and standard authentication) into one login interface. 

Want to use Rabbit in your project? It's simple!

```csharp
Connection conn = new RabbitAuth().LogOn(gameId, email, passwordOrToken, useSecureApiRequests);
```

The overload `(bool)useSecureApiRequests` option by default is `false`.

Rabbit requires .NET framework v3.5 or later.

###Wiki

Have more questions? Feel free to consult the [Wiki](https://github.com/Decagon/Rabbit/wiki).


###Bugs

If you find a bug in Rabbit, feel free to let me know in the GitHub issue tracker or, if you don't have an account, by decagongithub@gmail.com.

###NuGet

To get Rabbit on NuGet, simply type `Install-Package RabbitIO` or go to http://www.nuget.org/packages/RabbitIO/

**To continue recieving updates for Rabbit, upgrade to `RabbitIO` and delete `EE-Rabbit`.**

###Credits
NuGet package icon courtesy of [jcapaldi on Flickr](https://flic.kr/p/cVkan9).

[Yonom](https://github.com/Yonom), author of [Cupcake](https://github.com/Yonom/CupCake), significantly [helped](https://github.com/Decagon/Rabbit/commits/master?author=Yonom)!

###Everybody Edits

Use `EERabbitAuth()` to specifically authenticate with Everybody Edits:


```csharp
Connection conn = new EERabbitAuth().LogOn(EmailOrTokenOrUserName, RoomID, Password);
```

Using this method allows for the room to be joined (or created if one does not exist), has built-in room id verification, and is able to extract the id from url's. However, you may use `RabbitAuth()` instead.
