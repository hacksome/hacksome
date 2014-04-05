using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using comScoreSocialDashboard.services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace comScoreSocialDashboard
{
    /// <summary>
    /// Summary description for TwitterTweets
    /// </summary>
    public class TwitterTweets : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            if (!String.IsNullOrEmpty(context.Request.QueryString["map"]))
            {
                context.Response.Write(_getTweetsForMap());
            }
            else
            {
                context.Response.Write(_getMostRecentTweets(context));
            }
        }

        private string _getTweetsForMap()
        {
            var svc = new Twitterservice();

            var tweets = svc.GetSearchByKeyWordAndLocation(true);

            return JsonConvert.SerializeObject(tweets);
        }


        private string _getMostRecentTweets(HttpContext context)
        {
            var svc = new Twitterservice();
            List<TweetInfo> tweets=svc.GetSearchByKeyWordAndLocation().ToList().OrderByDescending(x=>x.Date).ToList();
            
            if (!String.IsNullOrEmpty(context.Request.QueryString["limit"]) ){
                tweets=tweets.Take(10).ToList();
            }

            return JsonConvert.SerializeObject(tweets);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}