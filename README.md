Rabbit [![Build status](https://ci.appveyor.com/api/projects/status/6fxlb8bkqp18cg3c/branch/master)](https://ci.appveyor.com/project/Decagon/rabbit/branch/master)
======

Supporting ArmorGames, Kongregate, MouseBreaker, Facebook and default (username or email) authentication mechanisms Rabbit allows developers to seamlessly integrate many forms of authentication in one interface. Rabbit detects the type of input and authenticates users to the appropriate service.

To use Rabbit, type:

```csharp
var conn = RabbitAuth.LogIn (EmailOrTokenOrUserName, RoomID, Password = null);
```

_The password parameter is optional if Facebook authentication is used._

Using NuGet? Just download the EE-Rabbit package.


Rabbit icon by https://flic.kr/p/cVkan9
