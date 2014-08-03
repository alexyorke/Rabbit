// ***********************************************************************
// <copyright file="Mousebreaker.cs" company="None">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Rabbit.Auth
{
    using System.Security.Authentication;
    using PlayerIOClient;

    /// <summary>
    /// Class Mousebreaker.
    /// </summary>
    public static class Mousebreaker
    {
        /// <summary>
        /// Authenticates using the specified email.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// A valid PlayerIOClient instance.
        /// </returns>
        public static Client Authenticate(string username, string password)
        {
            var c = PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", "guest", "guest");

            var userId = c.BigDB.Load("usernames", username)["owner"].ToString();

            if (userId.StartsWith("mouse"))
            {
                return PlayerIO.QuickConnect.SimpleConnect(RabbitAuth.GameId, userId.Substring(5, userId.Length - 5), password);
            }

            throw new AuthenticationException("Unknown username.");
        }
    }
}