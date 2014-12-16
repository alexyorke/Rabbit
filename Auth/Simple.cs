using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerIOClient;

namespace Rabbit.Auth
{
    public static class Simple
    {
        /// <summary>
        /// Authenticates with the specified email and password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static Client Authenticate(string email, string password)
        {
            return PlayerIO.QuickConnect.SimpleConnect(RabbitAuth.GameId, email, password);
        }
    }
}
