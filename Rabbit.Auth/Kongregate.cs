// ***********************************************************************
// Assembly         : Rabbit
// Author           : Decagon
// Created          : 07-22-2014
//
// Last Modified By : Decagon
// Last Modified On : 07-24-2014
// ***********************************************************************
// <copyright file="Kongregate.cs" company="">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Rabbit.Auth
{
    using PlayerIOClient;

    /// <summary>
    /// Class Kongregate.
    /// </summary>
    public static class Kongregate
    {
        /// <summary>
        /// Authenticates using the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>A valid PlayerIOClient instance.</returns>
        public static Client Authenticate(string email, string password)
        {
            return PlayerIO.QuickConnect.KongregateConnect(Rabbit.GameId, email, password);
        }
    }
}