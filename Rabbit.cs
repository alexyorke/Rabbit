// ***********************************************************************
// Assembly         : Rabbit
// Author           : Decagon
// Created          : 07-22-2014
//
// Last Modified By : Decagon
// Last Modified On : 07-24-2014
// ***********************************************************************
// <copyright file="Rabbit.cs" company="">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PlayerIOClient;
using Rabbit.Auth;

namespace Rabbit
{
    /// <summary>
    /// Authentication core.
    /// </summary>
    public class Rabbit
    {
        /// <summary>
        /// The game identifier
        /// </summary>
        public const string GameId = "everybody-edits-su9rn58o40itdbnw69plyw";

        /// <summary>
        /// Gets the Client for the main authentication system.
        /// </summary>
        /// <value>The client.</value>
        private Client Client { get; set; }

        /// <summary>
        /// Gets the main everybody edits conncetion to the server.
        /// </summary>
        /// <value>The ee connection.</value>
        private Connection EeConn { get; set; }

        /// <summary>
        /// Connects to the PlayerIO service using the provided credentials.
        /// </summary>
        /// <param name="email">Email address</param>
        /// <param name="password">Password or token</param>
        /// <param name="worldId">The room id of the world to join</param>
        /// <param name="createRoom">Whether or not to create a room or join an existing one.</param>
        /// <returns>A valid connection object.</returns>
        public Connection LogIn(string email, string password, string worldId, bool createRoom = false)
        {
            // Clean the email (or token) from whitespace
            email = Regex.Replace(email, @"\s+", "");

            var authType = GetAuthType(email, password);

            switch (authType)
            {
                case AuthType.Facebook:
                {
                    Client = Facebook.Authenticate(password);
                    break;
                }

                case AuthType.Kongregate:
                {
                    Client = Kongregate.Authenticate(email, password);
                    break;
                }

                case AuthType.ArmorGames:
                {
                    Client = ArmorGames.Authenticate(email, password);
                    break;
                }

                default:
                {
                    Client = PlayerIO.QuickConnect.SimpleConnect(GameId, email,
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


        /// <summary>
        /// Gets the type of the authentication.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>AuthType.</returns>
        /// <exception cref="System.ArgumentNullException">password;Password cannot be null</exception>
        public static AuthType GetAuthType(string email, string password)
        {

            // ArmorGames: Both UserID and password are 32 char hexadecimal lowercase strings
            if (!string.IsNullOrEmpty(email) &&
                Regex.IsMatch(password, @"^[0-9a-f]{32}$") &&
                Regex.IsMatch(email, @"^[0-9a-f]{32}$"))
            {
                return AuthType.ArmorGames;
            }

            // Kongregate: 
            // UserID is a number
            // Password is a 64 char hexadecimal lowercase string
            if (!string.IsNullOrEmpty(email) &&
                Regex.IsMatch(email, @"^\d+$") &&
                Regex.IsMatch(password, @"^[0-9a-f]{64}$"))
            {
                return AuthType.Kongregate;
            }

            // Facebook: password is a 100 char alphanumerical string
            // there is no UserID supplied
            if (string.IsNullOrEmpty(email) &&
                Regex.IsMatch(password, @"^[0-9a-z]{100,}$", RegexOptions.IgnoreCase))
            {
                return AuthType.Facebook;
            }

            // if it doesn't match anything then just assume regular
            return AuthType.Regular;
        }
    }
}