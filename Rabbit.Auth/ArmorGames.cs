//-----------------------------------------------------------------------
// <copyright file="Rabbit.Auth.ArmorGames.cs" company="Decagon">
//     Copyright Decagon
// </copyright>
//-----------------------------------------------------------------------

using System;
using PlayerIOClient;

namespace Rabbit.Auth
{
    /// <summary>
    ///     ArmorGames authentication class.
    /// </summary>
    public static class ArmorGames
    {
        /// <summary>
        ///     sets the client that is used throughout the Bot.
        /// </summary>
        public static Client Client { get; set; }

        /// <summary>
        ///     Authenticates the user using ArmorGames authentication.
        /// </summary>
        /// <param name="email">The user id of the user.</param>
        /// <param name="password">The user token.</param>
        /// <returns>A valid client object.</returns>
        public static Client Authenticate(string email, string password)
        {
            var c =
                PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", "guest", "guest")
                    .Multiplayer.JoinRoom(string.Empty, null);

            c.OnMessage += (sender, message) =>
            {
                if (message.Type != "auth")
                {
                    return;
                }

                if (message.Count == 0)
                {
                    throw new Exception("Could not log into ArmorGames.");
                }
                Client = PlayerIO.Connect(
                    "everybody-edits-su9rn58o40itdbnw69plyw",
                    "secure",
                    message.GetString(0),
                    message.GetString(1),
                    "armorgames");

                c.Disconnect();
            };

            c.Send("auth", email, password);

            return Client;
        }
    }
}