// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RabbitTests.cs" company="None">
//   Copyright 2014 (c).
// </copyright>
// <summary>
//   Defines the RabbitTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Rabbit.Auth;

namespace Rabbit.Tests
{
    /// <summary>
    /// The rabbit tests.
    /// </summary>
    [TestFixture]
    public class RabbitTests
    {
        /// <summary>
        /// The null authentication test.
        /// </summary>
        [Test]
        public void NullAuthenticationTest()
        {
            InvalidOperationException expectedException = null;

            try
            {
                RabbitAuth.GetAuthType(null, null);
            }
            catch (InvalidOperationException ex)
            {
                expectedException = ex;
            }

            Assert.IsNotNull(expectedException);
        }

        /// <summary>
        /// The regular authentication test.
        /// </summary>
        [Test]
        public void RegularAuthenticationTest()
        {
            var authResult = RabbitAuth.GetAuthType("test@email.com", "testpassword");

            Assert.AreEqual(AuthenticationType.Regular, authResult);
        }

        /// <summary>
        /// The facebook authentication test.
        /// </summary>
        [Test]
        public void FacebookAuthenticationTest()
        {
            var authResult = RabbitAuth.GetAuthType(null, "TcsL8qdSwvzDqudWGfYEGF5RrBLBSanHW78t5Z87ngKzUDdhCE4Jbtq6Vrwk2vuVS8WW2RYT54hwFxchWywLLvQaUyA2k2fanfAs");

            Assert.AreEqual(AuthenticationType.Facebook, authResult);
        }

        /// <summary>
        /// The kongregate authentication test.
        /// </summary>
        [Test]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here because it is a name of a company.")]
        public void KongregateAuthenticationTest()
        {
            var authResult = RabbitAuth.GetAuthType("123456", "969ad76abf19b5c3e5917321e659abe6d8d6f3aba73e5158863c3b4159c00366");

            Assert.AreEqual(AuthenticationType.Kongregate, authResult);
        }

        /// <summary>
        /// The armor games authentication test.
        /// </summary>
        [Test]
        public void ArmorGamesAuthenticationTest()
        {
            var authResult = RabbitAuth.GetAuthType("ee6ab941d09050400c4f916dbb47aad8", "23b9a5088c3c940f82945b6f3df80abc");

            Assert.AreEqual(AuthenticationType.ArmorGames, authResult);
        }
    }
}
