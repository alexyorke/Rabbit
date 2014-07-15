// A lot of this code is based on the open-source project Skylight.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerIOClient;
using System.Text.RegularExpressions;
using System.Threading;

namespace Rabbit
{
    public class Auth
    {

        public Client client { get; internal set; }

        public PlayerIOClient.Connection LogIn(string email, string password, string world_id)
        {
            // Clean the email from any whitespace.
            // Any userids, tokens, emails or usernames
            // do not contain any whitespace.

            email = Regex.Replace(email, @"\s+", "");

            var AccountType = "regular";

            if (((password == null)||(password == "")) &&
                email.Length > 100 &&
                Regex.IsMatch(email, @"\A\b[0-9a-zA-Z]+\b\Z"))
            {
                // Facebook login if "password" is over 100 characters, contains only A-Z,a-z and 0-9 and yeah.
                // there is no email supplied (null)
                AccountType = "facebook";
            }

            if (Regex.IsMatch(email, @"^\d+$") &&
                Regex.IsMatch(password, @"\A\b[0-9a-f]+\b\Z") &&
                password.Length == 64) // http://stackoverflow.com/questions/894263
            {
                // user id is a number
                // token thing is 64 characeters in hex
                // and all of the letters are lowercase
                // then it's Kongregate
                AccountType = "kongregate";
            }

            // 32 (length) in hex for both user id and auth token
            // for ArmourGames

            if (Regex.IsMatch(password, @"\A\b[0-9a-f]+\b\Z") &&
                Regex.IsMatch(password, @"\A\b[0-9a-f]+\b\Z") &&
                password.Length == 32 &&
                email.Length == 32)
            {
                AccountType = "ArmourGames";
            }


            switch (AccountType)
            {
                case "facebook":
                    {
                        client = Facebook.Authenticate(password);
                        break;
                    }
                case "kongregate":
                    {
                        client = Kongregate.Authenticate(email, password);
                        break;
                    }
                case "ArmourGames":
                    {
                        // broken auth
                        //client = ArmorGames.Authenticate(email, password);
                        break;
                    }
                default: {
                        client = PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", email, password);
                        break;
                    
                }
            }

            var ee_conn = client.Multiplayer.CreateJoinRoom(world_id, "Everybodyedits" + client.BigDB.Load("config", "config")["version"], true, new Dictionary<string, string>(), new Dictionary<string, string>());

            //ee_conn.OnMessage += new MessageReceivedEventHandler(connection_OnMessage);

            // These are disabled because the client may be interested
            // in the world initialization results.

            //ee_conn.Send("init");
            //ee_conn.Send("init2");

            return ee_conn;

         

        }
    }
}

