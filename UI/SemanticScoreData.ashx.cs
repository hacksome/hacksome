using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;
using Semantria.Com;
using Semantria.Com.Mapping;
using Semantria.Com.Mapping.Output;
using Semantria.Com.Serializers;
using TweetinviCore.Interfaces;
using comScoreSocialDashboard.services;

namespace comScoreSocialDashboard
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class SemanticScoreData : IHttpHandler, IRequiresSessionState 
    {
        private HttpContext _context;
        public void ProcessRequest(HttpContext context)
        {
            _context = context;
            context.Response.ContentType = "text/plain";
            context.Response.Write(string.Format("{{\"data\": {0} }}"
                , string.IsNullOrEmpty(context.Request.QueryString["b"]) ? _getTweetPieCount() : _getTweetBarData()));
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


        private const string ConsumerKey = "287518e2-985d-424c-9ca2-a2fb4dae0aa5";
        private const string ConsumerSecret = "4d876254-0da8-417b-aafc-738c2175e1d6";

        public class SemanticElement
        {
            public string ProductId { get; private set; }
            public ITweet TweetData { get; private set; }
            public Guid Guid { get; private set; }
            public string  Id { get; set; }
            public DocAnalyticData SemanticData { get; set; }

            public SemanticElement(string productId, ITweet tweet)
            {
                Guid = Guid.NewGuid();
                ProductId = productId;
                TweetData = tweet;
            }
        }

        List<SemanticElement> Semantics
        {
            get
            {
                if (_context.Session["sems"] == null)
                {
                    var svc = new Twitterservice();
                    var sems = new List<SemanticElement>();
                    foreach (var key in _keys)
                    {
                        List<ITweet> tweet = svc.GeTweetsByKeyWord(key);
                        sems.AddRange(tweet.Select(x => new SemanticElement(key, x)));
                    }

                    _populateSemantic(sems);
                    _context.Session["sems"] = sems;
                }
                return _context.Session["sems"] as List<SemanticElement>;
            }
        }



        private JArray _getTweetPieCount()
        {
            var attrArr = new JArray();

            var resultGroups = Semantics.GroupBy(x => x.ProductId);

            int i = 0;
            foreach (var group in resultGroups)
            {
                double score = group.Sum(x => x.SemanticData.SentimentScore)/group.Count();
                attrArr.Add(new JObject
                           {
                               {"name", new JValue(group.Key)},
                               {"score", new JValue(score * 100)},
                               {"totaltweets", new JValue(group.Count())},
                               {"xfactor", new JValue(score/group.Count() * 1000)}
                           });
            }
            return attrArr;
        }


        private JArray _getTweetBarData()
        {
            var attrArr = new JArray();

            var resultGroups = Semantics.GroupBy(x => x.ProductId);

            int i = 0;
            foreach (var group in resultGroups)
            {
                double score = group.Sum(x => x.SemanticData.SentimentScore) / group.Count();
                var o = new JObject();
                o.Add("pos", 2*i + 1);
                o.Add("score", score * 100);
                attrArr.Add(new JObject
                           {
                               {"label", new JValue(group.Key)},
                               {"data", o},
                               {"color", new JValue(Colors[i++ % Colors.Count])}
                           });
            }
            return attrArr;
        }

        private void _populateSemantic(List<SemanticElement> sems)
        {
            ISerializer serializer = new JsonSerializer();

            // Initializes new session with the serializer object and the keys.
            using (Session session = Session.CreateSession(ConsumerKey, ConsumerSecret, serializer))
            {
                // Error callback handler. This event will be rised in case of server-side error
                session.Error += new Session.ErrorHandler(delegate(object sender, ResponseErrorEventArgs ea)
                {
                    //Console.WriteLine(string.Format("{0}: {1}", (int)ea.Status, ea.Message));
                });


                IEnumerable<int> statuses = sems.Select(x => session.QueueDocument(new Document() { Id = x.Guid.ToString(), Text = x.TweetData.Text }));

                if (statuses.Any(x => x == -1))
                {
                    return;
                }

                // As Semantria isn't real-time solution you need to wait some time before getting of the processed results
                // In real application here can be implemented two separate jobs, one for queuing of source data another one for retreiving
                // Wait ten seconds while Semantria process queued document
                int resCount = sems.Count;
                IList<DocAnalyticData> results = new List<DocAnalyticData>();
                while (resCount > 0)
                {
                    System.Threading.Thread.Sleep(2000);

                    // Requests processed results from Semantria service
                    //Console.WriteLine("Retrieving your processed results...");
                    ((List<DocAnalyticData>)results).AddRange(session.GetProcessedDocuments());

                    resCount -= results.Count;
                }


                foreach (var result in results)
                {
                    sems.First(x => x.Guid.ToString() == result.Id).SemanticData = result;
                }

            }

            //Console.ReadKey(false);
        }


    }
}