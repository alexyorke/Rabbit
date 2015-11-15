// ***********************************************************************
// Assembly         : Rabbit
// Author           : Decagon
// Created          : 07-22-2014
// ***********************************************************************
// <copyright file="Username.cs" company="None">
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
    /// Class Username.
    /// </summary>
    public static class UserName
    {
        /// <summary>
        /// Authenticates using the specified email.
        /// </summary>
        /// <param name="gameId">The game id.</param>
        /// <param name="UserName">The user Name.</param>
        /// <param name="password">The password.</param>
        /// <returns>A valid PlayerIOClient instance.</returns>
        /// <exception cref="System.NotSupportedException">Username login is not supported for the specified game.</exception>
        /// <exception cref="AuthenticationException">Username login currently only supports everybodyedits.com users.</exception>
        public static Client Authenticate(string gameId, string UserName, string password)
        {
            if (gameId != EERabbitAuth.GameId)
                throw new NotSupportedException(strings.UsernameNotSupported);

            string userId;
            try
            {
                var c = Simple.Authenticate(gameId, "guest", "guest");
                userId = c.BigDB.Load("usernames", UserName).GetString("owner");
            }
            catch
            {
                userId = UserName;
            }

            if (userId.StartsWith("simple", StringComparison.CurrentCulture))
            {
                return Simple.Authenticate(gameId, userId.Substring(5),password);
            }

            throw new AuthenticationException(strings.UsernameNotSupported);
        }
    }
}