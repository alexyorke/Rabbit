// ***********************************************************************
// Assembly         : Rabbit
// Author           : Decagon
// Created          : 12-19-2014
//
// Last Modified By : Decagon
// Last Modified On : 12-19-2014
// ***********************************************************************
// <copyright file="Simple.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlayerIOClient;

namespace Rabbit.Auth
{
    /// <summary>
    /// Class Simple.
    /// </summary>
    public static class Simple
    {
        /// <summary>
        /// Authenticates with the specified email and password.
        /// </summary>
        /// <param name="gameId">The game id.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>Client.</returns>
        public static Client Authenticate(string gameId, string email, string password)
        {
            return PlayerIO.QuickConnect.SimpleConnect(gameId, email, password);
        }
    }
}
