using LinqToTwitter;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;

namespace TwitterReply.Controllers
{
    public class StatusesController : ApiController
    {
        //api/statuses
        public async Task<string> Post(JObject tweetOject)
        {
            try
            {
                string res = "";

                var auth = new SingleUserAuthorizer
                {
                    CredentialStore = new SingleUserInMemoryCredentialStore
                    {
                        ConsumerKey = tweetOject.Value<string>("consumerKey"),
                        ConsumerSecret = tweetOject.Value<string>("consumerSecret"),
                        AccessToken = tweetOject.Value<string>("accessToken"),
                        AccessTokenSecret = tweetOject.Value<string>("accessTokenSecret")
                    }
                };

                var score = tweetOject.Value<double>("score");

                switch (score)
                {
                    case double n when (n <= 0.2):
                        res = ":'(";
                        break;
                    case double n when (n > 0.2 && n<=0.4):
                        res = ":'‑(";
                        break;
                    case double n when (n > 0.4 && n <= 0.6):
                        res = ":‑|";
                        break;
                    case double n when (n > 0.6 && n <= 0.8):
                        res = ":-]";
                        break;
                    case double n when (n > 0.8 && n <= 1):
                        res = ":‑D";
                        break;

                }


                var username = tweetOject.Value<string>("userName");
                var a = tweetOject.Value<ulong>("statusId");

                var twitterCtx = new TwitterContext(auth);
                var tweet = await twitterCtx.ReplyAsync(a,$"Los sentimientos de tu tweet @{username} son {res}");

                return tweetOject.ToString();
            }
            catch (Exception e)
            {

                throw ;
            }
        }

       

    }
}
