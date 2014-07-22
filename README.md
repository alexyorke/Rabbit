Rabbit
======

[![Build Status](https://travis-ci.org/Decagon/Rabbit.svg?branch=master)](https://travis-ci.org/Decagon/Rabbit)


This framework allows developers to have one unified authentication UI and one authentication method to authenticate multiple types of users. The authentication type is handled automatically, and a valid PlayerIOClient object is returned.

Rabbit currently supports ArmorGames, MouseBreaker, Facebook and default authentication mechanisms.


Use Rabbit like this:

`using Rabbit;`
`var rabbitAuth = new Rabbit.Rabbit();`

`var Connection = rabbitAuth.LogIn(EmailOrToken, PasswordOrToken, RoomID);`


Now, Connection is a valid PlayerIOClient connection.

Rabbit is going to know what to set the mysterious block delay to, depending on your geographical region and internet speed so that you, as a developer, do not need to ask the user or make a guess.
