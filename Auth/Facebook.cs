using PlayerIOClient;

namespace Rabbit.Auth
{
    /// <summary>
    /// Class Facebook.
    /// </summary>
    public static class Facebook
    {
        /// <summary>
        /// Authenticates the specified token.
        /// </summary>
        /// <param name="gameId">The game id.</param>
        /// <param name="token">The token.</param>
        /// <returns>A valid PlayerIOClient instance.</returns>
        public static Client Authenticate(string gameId, string token, string[] playerInsightSegments = null)
        {
            return PlayerIO.QuickConnect.FacebookOAuthConnect(gameId, token, null, playerInsightSegments);
        }
    }
}