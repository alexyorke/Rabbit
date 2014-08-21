// ***********************************************************************
// Assembly         : Rabbit
// Author           : Decagon
// Created          : 07-22-2014
// ***********************************************************************
// <copyright file="ArmorGames.cs" company="None">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary>ArmorGames authentication. </summary>

namespace Rabbit.Auth
{
    using System;
    using System.Security.Authentication;

    using PlayerIOClient;

    /// <summary>
    /// ArmorGames authentication class.
    /// </summary>
    public static class ArmorGames
    {
        /// <summary>
        /// Gets or sets the client that is used throughout the Bot.
        /// </summary>
        /// <value>The client.</value>
        private static Client Client { get; set; }

        /// <summary>
        /// Authenticates the user using ArmorGames authentication.
        /// </summary>
        /// <param name="email">The user id of the user.</param>
        /// <param name="password">The user token.</param>
        /// <returns>A valid client object.</returns>
        public static Client Authenticate(string email, string password)
        {
            var c =
                PlayerIO.QuickConnect.SimpleConnect(RabbitAuth.GameId, "guest", "guest")
                    .Multiplayer.JoinRoom(string.Empty, null);

            c.OnMessage += (sender, message) =>
            {
                if (message.Type != "auth")
                {
                    return;
                }

                if (message.Count == 0)
                {
                    throw new AuthenticationException("Could not log into Armor Games.");
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
