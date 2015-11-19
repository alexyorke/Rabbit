using System;
using System.Security.Authentication;
using System.Threading;
using PlayerIOClient;
using Rabbit.EE;
using System.Collections.Generic;

namespace Rabbit.Auth
{
    /// <summary>
    /// Armor Games authentication class.
    /// </summary>
    public static class ArmorGames
    {
        /// <summary>
        /// Authenticates the user using Armor Games authentication.
        /// </summary>
        /// <param name="gameId">The game id.</param>
        /// <param name="email">The user id of the user.</param>
        /// <param name="password">The user token.</param>
        /// <returns>
        /// PlayerIO client object.
        /// </returns>
        /// <exception cref="NotSupportedException">Armor Games login is not supported for the specified game.</exception>
        public static Client Authenticate(string gameId, string userid, string token, string[] playerInsightSegments = null)
        {
            return PlayerIO.Authenticate(
            gameId,
            "public",
            new Dictionary<string, string> {
                {"userId", userid},
                {"authToken", token},
            },
            playerInsightSegments
            );

        }
    }
}
