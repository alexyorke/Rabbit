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
    
    public static class UserName
    {
        /// <summary>
        /// Authenticates using the specified email.
        /// </summary>
        /// <param name="userName">
        /// The user Name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// A valid PlayerIOClient instance.
        /// </returns>
        public static Client Authenticate(string userName, string password)
        {
            var c = PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", "guest", "guest");

            var userId = "";

            try
            {
                userId = c.BigDB.Load("usernames", userName)["owner"].ToString();
            }
            catch (NullReferenceException)
            {
                // if the username is a user id
                userId = userName;
            }

            if (userId.StartsWith("simple", StringComparison.CurrentCulture))
            {
                return PlayerIO.QuickConnect.SimpleConnect(RabbitAuth.GameId, userId.Substring(6, userId.Length - 6), password);
            }

            throw new AuthenticationException("Unknown username.");
        }
    }
}