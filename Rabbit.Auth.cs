//-----------------------------------------------------------------------
// <copyright file="Rabbit.Auth.cs" company="Decagon">
//     Copyright Decagon
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;
using PlayerIOClient;
using Rabbit.Rabbit.Auth;

namespace Rabbit
{
    /// <summary>
    ///     Authentication core.
    /// </summary>
    public class Auth
    {
        /// <summary>
        ///     Gets the Client for the main authentication system.
        /// </summary>
        public Client Client { get; internal set; }

        /// <summary>
        ///     Gets the main everybody edits conncetion to the server.
        /// </summary>
        public Connection EeConn { get; set; }

        /// <summary>
        ///     Connects to the PlayerIO service using the provided credentials.
        /// </summary>
        /// <param name="email">Email address</param>
        /// <param name="password">Password or token</param>
        /// <param name="worldId">The room id of the world to join</param>
        /// <param name="createRoom">Whether or not to create a room or join an existing one.</param>
        /// <returns>A valid connection object.</returns>
        public Connection LogIn(string email, string password, string worldId, bool createRoom = false)
        {
            // Clean the email from any whitespace.
            // Any userids, tokens, emails or usernames
            // do not contain any whitespace.

            email = Regex.Replace(email, @"\s+", "");

            string accountType = "regular";

            if (string.IsNullOrEmpty(password) &&
                email.Length > 100 &&
                Regex.IsMatch(email, @"\A\b[0-9a-zA-Z]+\b\Z"))
            {
                // Facebook login if "password" is over 100 characters, contains only A-Z,a-z and 0-9 and yeah.
                // there is no email supplied (null)
                accountType = "facebook";
            }

            if (password != null && (Regex.IsMatch(email, @"^\d+$") &&
                                     Regex.IsMatch(password, @"\A\b[0-9a-f]+\b\Z") &&
                                     password.Length == 64)) // http://stackoverflow.com/questions/894263
            {
                // user id is a number
                // token thing is 64 characeters in hex
                // and all of the letters are lowercase
                // then it's Kongregate
                accountType = "kongregate";
            }

            // 32 (length) in hex for both user id and auth token
            // for ArmourGames

            if (password != null && (Regex.IsMatch(password, @"\A\b[0-9a-f]+\b\Z") &&
                                     Regex.IsMatch(password, @"\A\b[0-9a-f]+\b\Z") &&
                                     password.Length == 32 &&
                                     email.Length == 32))
            {
                accountType = "ArmourGames";
            }

            switch (accountType)
            {
                case "facebook":
                {
                    Client = Facebook.Authenticate(password);
                    break;
                }
                case "kongregate":
                {
                    Client = Kongregate.Authenticate(email, password);
                    break;
                }
                case "ArmourGames":
                {
                    Client = ArmorGames.Authenticate(email, password);
                    break;
                }
                default:
                {
                    Client = PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", email,
                        password);
                    break;
                }
            }

            if (createRoom)
            {
                EeConn = Client.Multiplayer.CreateJoinRoom(worldId,
                    "Everybodyedits" + Client.BigDB.Load("config", "config")["version"], true,
                    new Dictionary<string, string>(), new Dictionary<string, string>());
            }
            else
            {
                EeConn = Client.Multiplayer.JoinRoom(
                    worldId,
                    new Dictionary<string, string>());
            }

            return EeConn;
        }
    }
}