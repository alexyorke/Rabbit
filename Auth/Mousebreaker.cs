// ***********************************************************************
// <copyright file="Mousebreaker.cs" company="None">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Rabbit.Auth
{
    using System;
    using System.Globalization;
    using System.Security.Authentication;
    using PlayerIOClient;

    /// <summary>
    /// Class Mousebreaker.
    /// </summary>
    public static class MouseBreaker
    {
        /// <summary>
        /// Authenticates using the specified email.
        /// </summary>        
        /// <param name="gameId">
        /// The game id.
        /// </param>
        /// <param name="userName">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// A valid PlayerIOClient instance.
        /// </returns>
        public static Client Authenticate(string gameId, string userName, string password)
        {
            var c = PlayerIO.QuickConnect.SimpleConnect(gameId, "guest", "guest");

            var userId = c.BigDB.Load("usernames", userName)["owner"].ToString();

            if (userId.StartsWith("mouse", StringComparison.CurrentCulture))
            {
                return PlayerIO.QuickConnect.SimpleConnect(gameId, userId.Substring(5, userId.Length - 5), password);
            }

            throw new AuthenticationException("Invalid credentials for mousebreaker authentication.");
        }
    }
}
