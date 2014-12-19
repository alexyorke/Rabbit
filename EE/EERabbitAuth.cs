using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerIOClient;
using Rabbit.Auth;

namespace Rabbit.EE
{
    class EERabbitAuth : RabbitAuth
    {

        /// <summary>
        /// The game identifier
        /// </summary>
        public static string GameId = "everybody-edits-su9rn58o40itdbnw69plyw";

        /// <summary>
        /// Gets or sets a value indicating whether to create a multiplayer room.
        /// </summary>
        public bool CreateRoom { get; set; }

        public EERabbitAuth()
        {
            CreateRoom = true;
        }

        /// <summary>
        /// Ees the login.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>        
        /// <param name="worldId">
        /// The room id of the world to join
        /// </param>
        /// <returns></returns>
        public new Connection LogOn(string email, string password, string worldId)
        {
            var client = base.LogOn(GameId, email, password);

            // Parse the world id (if it exists in another format)
            worldId = IdParser.Parse(worldId);

            Connection eeConn;
            if (CreateRoom)
            {
                var roomPrefix = worldId.StartsWith("BW", StringComparison.InvariantCulture)
                    ? "Beta"
                    : "Everybodyedits";

                var serverVersion = client.BigDB.Load("config", "config")["version"];
                eeConn = client.Multiplayer.CreateJoinRoom(
                    worldId,
                    roomPrefix + serverVersion,
                    true,
                    null,
                    null);
            }
            else
            {
                eeConn = client.Multiplayer.JoinRoom(worldId, null);
            }

            return eeConn;
        }

        public new Connection LogOn(string token, string worldId)
        {
            return this.LogOn(token, null, worldId);
        }
    }
}
