// ***********************************************************************
// Assembly         : Rabbit
// Author           : Decagon
// Created          : 07-22-2014
// ***********************************************************************
// <copyright file="Kongregate.cs" company="None">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Rabbit.Auth
{
    using System.Diagnostics.CodeAnalysis;

    using PlayerIOClient;

    /// <summary>
    /// Class Kongregate.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here because it is a name of a company.")]
    public static class Kongregate
    {
        /// <summary>
        /// Authenticates using the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>A valid PlayerIOClient instance.</returns>
        public static Client Authenticate(string gameId, string email, string password)
        {
            return PlayerIO.QuickConnect.KongregateConnect(gameId, email, password);
        }
    }
}