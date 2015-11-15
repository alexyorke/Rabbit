// ***********************************************************************
// <copyright file="Mousebreaker.cs" company="None">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Security.Authentication;
using PlayerIOClient;
using Rabbit.EE;
using Rabbit.Localizations;

namespace Rabbit.Auth
{
    /// <summary>
    /// Class Mousebreaker.
    /// </summary>
    public static class MouseBreaker
    {
        /// <summary>
        /// Authenticates using the specified email.
        /// </summary>
        /// <param name="gameId">The game id.</param>
        /// <param name="userName">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>A valid PlayerIOClient instance.</returns>
        /// <exception cref="System.NotSupportedException">Mousebreaker login is not supported for the specified game.</exception>
        /// <exception cref="AuthenticationException">Invalid credentials for Mousebreaker authentication.</exception>
        public static Client Authenticate(string gameId, string userName, string password)
        {
            if (gameId != EERabbitAuth.GameId)
                throw new NotSupportedException(strings.MousebreakerNotSupported);

            var c = Simple.Authenticate(gameId, "guest", "guest");

            var userId = c.BigDB.Load("usernames", userName)["owner"].ToString();

            if (userId.StartsWith("mouse", StringComparison.CurrentCulture))
            {
                return Simple.Authenticate(gameId, userId.Substring(5), password);
            }

            throw new AuthenticationException(strings.MouseBreakerAuthFailure);
        }
    }
}
