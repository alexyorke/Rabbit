using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerIOClient;

namespace Rabbit
{
    public class Auth
    {


        public void LogIn(string email, string password)
        {
            // Returns a valid client connection.
            var AccountType = "regular";
            if (password == null)
            {
                AccountType = "facebook";
            }

            switch (AccountType)
            {

                case "regular":
                    {
                        var client = PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", email, password);
                        break;
                    }
                case "facebook":
                    {
                        var client = PlayerIO.QuickConnect.FacebookOAuthConnect("everybody-edits-su9rn58o40itdbnw69plyw", password, null);
                    }

                    var ee_conn = client.Multiplayer.CreateJoinRoom("WORLDID", "Everybodyedits" + client.BigDB.Load("config", "config")["version"], true, new Dictionary<string, string>(), new Dictionary<string, string>());
                    //ee_conn.OnMessage += new MessageReceivedEventHandler(connection_OnMessage);
                    ee_conn.Send("init");
                    ee_conn.Send("init2");
                // Facebook login if "password" is over 100 characters, contains only A-Z,a-z and 0-9 and yeah.
                // there is no email supplied (null)

                // user id is a number
                    // token thing is 64 characeters in hex.
                /*
                case AccountType.Kongregate:
                    this.Client = PlayerIO.QuickConnect.KongregateConnect(Utilities.GameID, this.emailOrToken, this.passwordOrToken);
                    break;
                 * 
                // 32 (length) in hex for both user id and auth token.
                case AccountType.ArmorGames:
                    ArmorGamesConnect();
                    break;*/
            }
        }
    }
}
