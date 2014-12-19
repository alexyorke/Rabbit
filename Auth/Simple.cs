﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerIOClient;

namespace Rabbit.Auth
{
    /// <summary>
    /// Class Simple.
    /// </summary>
    public static class Simple
    {
        /// <summary>
        /// Authenticates with the specified email and password.
        /// </summary>        
        /// <param name="gameId">
        /// The game id.
        /// </param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static Client Authenticate(string gameId, string email, string password)
        {
            return PlayerIO.QuickConnect.SimpleConnect(gameId, email, password);
        }
    }
}
