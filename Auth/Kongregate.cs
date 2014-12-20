// ***********************************************************************
// Assembly         : Rabbit
// Author           : Decagon
// Created          : 07-22-2014
// ***********************************************************************
// <copyright file="Kongregate.cs" company="None">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics.CodeAnalysis;
using PlayerIOClient;

namespace Rabbit.Auth
{
    /// <summary>
    /// Class Kongregate.
    /// </summary>

    public static class Kongregate
    {
        /// <summary>
        /// Authenticates using the specified email.
        /// </summary>
        /// <param name="gameId">The game id.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>A valid PlayerIOClient instance.</returns>
        public static Client Authenticate(string gameId, string email, string password)
        {
            return PlayerIO.QuickConnect.KongregateConnect(gameId, email, password);
        }
    }
}