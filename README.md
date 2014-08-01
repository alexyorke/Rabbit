Rabbit
======

[![Build status](https://ci.appveyor.com/api/projects/status/6fxlb8bkqp18cg3c/branch/master)](https://ci.appveyor.com/project/Decagon/rabbit/branch/master)


This framework allows developers to have one unified authentication UI and one authentication method to authenticate multiple types of users. The authentication type is handled automatically.

Rabbit currently supports ArmorGames, Kongregate, MouseBreaker*, Facebook and default (username or email) authentication mechanisms.


Add Rabbit to your C# project:

```csharp
using Rabbit;
var conn = new Rabbit.LogIn {Email      = UserEmail,
                         RoomID     = UserRoomID,
                         Password   = UserPassword,
                         CreateRoom = true,
                         AuthType   = AuthType.Unknown};
```

Connection is a valid PlayerIOClient connection. The password, createRoom and AuthType parameters are optional.

Furthermore, Rabbit springs to action when the client disconnects and automatically cleans up the session*.

***Note:*** the MouseBreaker authentication pattern is very similar to ArmorGames and so may be difficult to deciper between them automatically. Moreover there are much more ArmorGames accounts than MouseBreaker accounts. The disconnect-cleanup feature has not been implemented, yet. Rabbit may be able to suggest a delay time depending on the user's geographical region.
