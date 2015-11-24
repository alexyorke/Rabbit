using System;
using System.Security.Authentication;
using PlayerIOClient;
using Rabbit.EE;

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
        public static Client Authenticate(string gameId, string userName, string password, string[] playerInsightSegments = null)
        {
            if (gameId != EERabbitAuth.GameId)
                throw new NotSupportedException();

            var c = Simple.Authenticate(gameId, "guest", "guest");
            var userId = c.BigDB.Load("usernames", userName)["owner"].ToString();

            if (userId.StartsWith("mouse", StringComparison.CurrentCulture))
            {
                return Simple.Authenticate(gameId, userId.Substring(5), password);
            } else {
                if (userId != null) {
                    // It's possible that a user chose a password that happened to be a base64 string
                    // that's 88 characters long and they do not use mousebreaker.

                    // Reset authentication type and try again

                    var rabbit = new RabbitAuth();
                    rabbit.AuthenticationType = AuthenticationType.Unknown;

                    return rabbit.LogOn(gameId, userId, password);
                }
            }

            throw new AuthenticationException();
        }
    }
}
