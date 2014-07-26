Rabbit
======

[![Build status](https://ci.appveyor.com/api/projects/status/6fxlb8bkqp18cg3c/branch/master)](https://ci.appveyor.com/project/Decagon/rabbit/branch/master)


This framework allows developers to have one unified authentication UI and one authentication method to authenticate multiple types of users. The authentication type is handled automatically, and a valid PlayerIOClient object is returned.

Rabbit currently supports ArmorGames, MouseBreaker*, Facebook and default authentication mechanisms.


Add Rabbit to your project:

```csharp
using Rabbit;
var rabbitAuth = new Rabbit.Rabbit();
var Connection = rabbitAuth.LogIn(EmailOrToken, PasswordOrToken, RoomID);
```

Now, Connection is a valid PlayerIOClient connection. Remember to initialize it!

Rabbit is going to know what to set the mysterious block delay to, depending on your geographical region and internet speed so that you, as a developer, do not need to ask the user or make a guess.

***Note:*** the MouseBreaker authentication pattern is very similar to ArmorGames and so may be difficult to deciper between them automatically.
