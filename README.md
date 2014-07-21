Rabbit
======

An EE framework that helps EE libraries.

This framework is meant to be integrated into another library (so that features such as login don't have to be constantly reimplemented) and is able to adjust the delay between requests to the server, depending on the user's geographical location. Trying to figure out what delay you should use between requests? Rabbit knows.

The authentication type is handled automatically. Developers are now **not** required to create seperate tabs or UI's for different authentication mechanisms. Just type it in the credentials, and Rabbit does the rest.


Use Rabbit like this:

`using Rabbit;`
`var rabbitAuth = new Rabbit.Auth();`

`var Connection = rabbitAuth.LogIn(EmailOrToken, PasswordOrToken, RoomID);`


Now, Connection is a valid PlayerIOClient connection.
