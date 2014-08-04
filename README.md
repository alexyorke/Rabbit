Rabbit
======

[![Build status](https://ci.appveyor.com/api/projects/status/6fxlb8bkqp18cg3c/branch/master)](https://ci.appveyor.com/project/Decagon/rabbit/branch/master)


This framework allows developers to have one unified authentication UI and one authentication method to authenticate multiple types of users. The authentication type is handled automatically.

Rabbit currently supports ArmorGames, Kongregate, MouseBreaker, Facebook and default (username or email) authentication mechanisms. In the future Rabbit is possibly planning to support Yahoo GamesNet.


Add Rabbit to your C# project:

```csharp
using Rabbit;
var conn = RabbitAuth.LogIn (EmailOrTokenOrUserName, RoomID, Password);
```

The password parameter is optional if Facebook authentication is used.


***Note:*** Rabbit may be able to suggest a delay time depending on the user's geographical region.
