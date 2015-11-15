// ***********************************************************************
// Assembly         : Rabbit
// Author           : Decagon
// Created          : 12-19-2014
//
// Last Modified By : Decagon
// Last Modified On : 12-19-2014
// ***********************************************************************
// <copyright file="EERabbitAuth.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using PlayerIOClient;

namespace Rabbit.EE
{
    /// <summary>
    /// Class EERabbitAuth.
    /// </summary>
    public class EERabbitAuth : RabbitAuth
    {
        /// <summary>
        /// The game identifier
        /// </summary>
        public static string GameId = "everybody-edits-su9rn58o40itdbnw69plyw";

        /// <summary>
        /// Gets or sets a value indicating whether to create a multiplayer room.
        /// </summary>
        /// <value><c>true</c> if [create room]; otherwise, <c>false</c>.</value>
        public bool CreateRoom { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EERabbitAuth" /> class.
        /// </summary>
        public EERabbitAuth()
        {
            CreateRoom = true;
        }

        /// <summary>
        /// Logs in and connects to the specified room.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="worldId">The room id of the world to join</param>
        /// <param name="CreateRoom">if set to <c>true</c> secure API requests will be used.</param>
        /// <returns>
        /// Connection.
        /// </returns>
        public Connection LogOn(string email, string password, string worldId, bool CreateRoom = true)
        {
            var client = base.LogOn(GameId, email, password);

            // Parse the world id (if it exists in another format)
            worldId = IdParser.Parse(worldId);

            Connection eeConn;
            if (this.CreateRoom)
            {
                if (!(worldId.StartsWith("PW", StringComparison.Ordinal)
                        || worldId.StartsWith("BW", StringComparison.InvariantCulture))) {
                    throw new FormatException("World ID must start with PW or BW when creating a new room.");
                }

                var roomPrefix = worldId.StartsWith("BW", StringComparison.Ordinal)
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
    }
}
