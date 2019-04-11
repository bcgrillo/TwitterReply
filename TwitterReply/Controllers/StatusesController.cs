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
                    res = "Lamentamos mucho tu mensaje, en breve nos pondremos en contacto contigo... \u1F622";
                    break;
                case double n when (n > 0.2 && n <= 0.4):
                    res = "Sentimo mucho leer esto. No siempre acertamos en todo como nos gustaría. \u1F61F";
                    break;
                case double n when (n > 0.4 && n <= 0.6):
                    res = "Vaya, tomamos nota de tu mensaje. \u1F610";
                    break;
                case double n when (n > 0.6 && n <= 0.8):
                    res = "¡Gracias por tu mensaje! Seguiremos mejorando. \u1F60A";
                    break;
                case double n when (n > 0.8 && n <= 1):
                    res = "¡Wow! ¡Muchísimas gracias, nos alegra mucho leer esto! \u1F604";
                    break;

            }

            var username = tweetOject.Value<string>("userName");
            var statusId = tweetOject.Value<ulong>("statusId");

            var twitterCtx = new TwitterContext(auth);
            var tweet = await twitterCtx.ReplyAsync(statusId, $"@{username}\n{res}");

            return tweet.ToString();
        }
    }
}
