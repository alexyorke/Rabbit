// ***********************************************************************
// Assembly         : Rabbit
// Author           : Decagon
// Created          : 07-22-2014
// ***********************************************************************
// <copyright file="Facebook.cs" company="">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Rabbit.Auth
{
    using PlayerIOClient;

    /// <summary>
    /// Class Facebook.
    /// </summary>
    public static class Facebook
    {
        /// <summary>
        /// Authenticates the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>A valid PlayerIOClient instance.</returns>
        public static Client Authenticate(string gameId, string token)
        {
            return PlayerIO.QuickConnect.FacebookOAuthConnect(gameId, token, null);
        }
    }
}