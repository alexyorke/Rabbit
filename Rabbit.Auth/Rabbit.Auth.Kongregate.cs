using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerIOClient;
using System.Text.RegularExpressions;

namespace Rabbit
{
    public static class Kongregate
    {
        public static Client Authenticate(string email, string password)
        {
            return PlayerIO.QuickConnect.KongregateConnect("everybody-edits-su9rn58o40itdbnw69plyw", email, password);
        }
    }

}