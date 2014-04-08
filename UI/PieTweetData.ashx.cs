using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using TweetinviCore.Interfaces;
using comScoreSocialDashboard.services;

namespace comScoreSocialDashboard
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(string.Format("{{\"data\": {0} }}", _getTweetPieCount().ToString()));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private readonly List<string> _keys = new List<string>() { "comscore", 
                "comscore vce", "comScore Video Metrix", 
                "comScore multiplatform", "comScore digital analytics" };

        private static readonly List<string> Colors = new List<string>
                                                 {
                                                     "#68BC31"
                                                     ,
                                                     "#2091CF"
                                                     ,
                                                     "#AF4E96"
                                                     ,
                                                     "#DA5430"
                                                     ,
                                                     "#FEE074"
                                                 };

        private JArray _getTweetPieCount()
        {
            var attrArr = new JArray();
            var svc = new Twitterservice();

                IEnumerable<KeyValuePair<string, List<ITweet>>> results = _keys.Select(x => new KeyValuePair<string, List<ITweet>>(x, svc.GeTweetsByKeyWord(x)));

            int totalCount = results.Sum(x => x.Value.Count);

            Dictionary<string, double> data = results.ToDictionary(x => x.Key, x => (double) x.Value.Count/totalCount);

            int i = 0;
            foreach (var d in data)
            {
                attrArr.Add(new JObject
                           {
                               {"label", new JValue(d.Key)},
                               {"data", new JValue(d.Value)},
                               {"color", new JValue(Colors[i++ % Colors.Count])}
                           });
            }
            return attrArr;
        }

    }
}