// ***********************************************************************
// Assembly         : Rabbit
// Author           : Decagon
// Created          : 07-22-2014
// ***********************************************************************
// <copyright file="ArmorGames.cs" company="None">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary>ArmorGames authentication. </summary>

using System.Runtime.InteropServices;
using System.Threading;

namespace Rabbit.Auth
{
    using System;
    using System.Security.Authentication;

    using PlayerIOClient;

    /// <summary>
    /// ArmorGames authentication class.
    /// </summary>
    public static class ArmorGames
    {
        /// <summary>
        /// Authenticates the user using ArmorGames authentication.
        /// </summary>
        /// <param name="email">The user id of the user.</param>
        /// <param name="password">The user token.</param>
        /// <returns>A valid client object.</returns>
        public static Client Authenticate(string email, string password)
        {
            var resetEvent = new ManualResetEvent(false);
            var guestClient = PlayerIO.QuickConnect.SimpleConnect(RabbitAuth.GameId, "guest", "guest");
            var guestConn = guestClient.Multiplayer.JoinRoom(String.Empty, null);
            Client client = null;
            Exception exception = null;

            guestConn.OnMessage += (sender, message) =>
            {
                try
                {
                    if (message.Type != "auth" || message.Count < 2)
                        throw new AuthenticationException("Could not log into Armor Games.");

                    client = PlayerIO.Connect(
                        RabbitAuth.GameId,
                        "secure",
                        message.GetString(0),
                        message.GetString(1),
                        "armorgames");
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                finally
                {
                    resetEvent.Set();
                    guestConn.Disconnect();
                }
            };

            guestConn.OnDisconnect += (sender, message) =>
            {
                resetEvent.Set();
            };

            guestConn.Send("auth", email, password);
            resetEvent.WaitOne();

            if (exception != null)
                throw exception;
            return client;
        }
    }
}
