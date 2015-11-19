using System;
using System.Security.Authentication;
using PlayerIOClient;
using Rabbit.EE;

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
        public static Client Authenticate(string gameId, string UserName, string password, string[] playerInsightSegments = null)
        {
            if (gameId != EERabbitAuth.GameId)
                throw new NotSupportedException();

            string userId;

            try
            {
                var c = Simple.Authenticate(gameId, "guest", "guest");
                userId = c.BigDB.Load("usernames", UserName).GetString("owner");
            }
            catch (PlayerIOError)
            {
                userId = UserName;
            }

            if (userId.StartsWith("simple", StringComparison.CurrentCulture))
            {
                return Simple.Authenticate(gameId, userId.Substring(5), password, playerInsightSegments);
            }

            throw new AuthenticationException();
        }
    }
}