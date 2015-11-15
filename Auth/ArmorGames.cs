// ***********************************************************************
// Assembly         : Rabbit
// Author           : Decagon
// Created          : 07-22-2014
// ***********************************************************************
// <copyright file="ArmorGames.cs" company="None">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary>Armor Games authentication. </summary>

using System;
using System.Security.Authentication;
using System.Threading;
using PlayerIOClient;
using Rabbit.EE;
using Rabbit.Localizations;

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
        /// <returns>A valid client object.</returns>
        /// <exception cref="System.NotSupportedException">Armor Games login is not supported for the specified game.</exception>
        public static Client Authenticate(string gameId, string email, string password)
        {
            if (gameId != EERabbitAuth.GameId)
                throw new NotSupportedException(strings.ArmorGamesNotSupported);

            var resetEvent = new ManualResetEvent(false);
            var guestClient = Simple.Authenticate(gameId, "guest", "guest");
            var guestConn = guestClient.Multiplayer.JoinRoom(String.Empty, null);
            Client client = null;
            Exception exception = null;

            guestConn.OnMessage += (sender, message) =>
            {
                try
                {
                    if (message.Type != "auth" || message.Count < 2)
                        throw new AuthenticationException(strings.ArmorGamesLogOnFailure);

                    client = PlayerIO.Connect(
                        gameId,
                        "secure",
                        message.GetString(0),
                        message.GetString(1),
                        "armorgames");
                }
                catch (AuthenticationException ex)
                {
                    exception = ex;
                }
                finally
                {
                    resetEvent.Set();
                    guestConn.Disconnect();
                }
            };

            guestConn.OnDisconnect += (sender, message) => resetEvent.Set();

            guestConn.Send("auth", email, password);
            resetEvent.WaitOne();

            if (exception != null)
                throw exception;
            return client;
        }
    }
}
