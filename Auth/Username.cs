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
namespace Rabbit.Auth
{
    using System;
    using System.Security.Authentication;
    using PlayerIOClient;

    /// <summary>
    /// Class Username.
    /// </summary>
    public static class Username
    {
        /// <summary>
        /// Authenticates using the specified email.
        /// </summary>
        /// <param name="username">
        /// The user Name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// A valid PlayerIOClient instance.
        /// </returns>
        public static Client Authenticate(string username, string password)
        {
            string userId;
            try
            {
                var c = PlayerIO.QuickConnect.SimpleConnect(RabbitAuth.GameId, "guest", "guest");
                userId = c.BigDB.Load("usernames", username).GetString("owner");
            }
            catch
            {
                userId = username;
            }

            if (userId.StartsWith("simple", StringComparison.CurrentCulture))
            {
                return Simple.Authenticate(userId.Substring(5),password);
            }

            throw new AuthenticationException("Username login currently only supports everybodyedits.com users.");
        }
    }
}