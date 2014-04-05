using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;
using Semantria.Com;
using Semantria.Com.Mapping;
using Semantria.Com.Mapping.Output;
using TweetinviCore.Interfaces;
using JsonSerializer = Semantria.Com.Serializers.JsonSerializer;

namespace comScoreSocialDashboard.services
{
    public class SemanticElement
    {
        public string ProductId { get; private set; }
        public ITweet TweetData { get; private set; }
        public Guid Guid { get; private set; }
        public DocAnalyticData SemanticData { get; set; }

        public SemanticElement(string productId, ITweet tweet)
        {
            Guid = Guid.NewGuid();
            ProductId = productId;
            TweetData = tweet;
        }
    }
    [DataContract]
    public class Ranking
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Score { get; set; }
        [DataMember]
        public double Index { get; set; }
        [DataMember]
        public int Total { get; set; }
    }

    public class SemantriaService
    {
        private const string ConsumerKey = "287518e2-985d-424c-9ca2-a2fb4dae0aa5";
        private const string ConsumerSecret = "4d876254-0da8-417b-aafc-738c2175e1d6";


        public Ranking GetRanking(string searchkey)
        {
            var svc = new Twitterservice();

            var sems = new List<SemanticScoreData.SemanticElement>();

            List<ITweet> tweet = svc.GeTweetsByKeyWord(searchkey);
            sems.AddRange(tweet.Select(x => new SemanticScoreData.SemanticElement(searchkey, x)));

            _populateSemantic(sems);
            var resultGroups = sems.GroupBy(x => x.ProductId);
            int i = 0;
            var grp = resultGroups.FirstOrDefault();
            var ranking = new Ranking();
            if (grp != null)
            {
                var count = grp.Count();
                double score = grp.Sum(x => x.SemanticData.SentimentScore) / count;
                ranking = new Ranking()
                          {
                              Name = grp.Key,
                              Score = Math.Round(score * 100),
                              Total = count,
                              Index = Math.Round(score / count * 1000)
                          };
            }
            return ranking;
        }

        private void _populateSemantic(List<SemanticScoreData.SemanticElement> sems)
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
                   var s= sems.FirstOrDefault(x => x.Guid.ToString() == result.Id);
                    if (s != null){
                        s.SemanticData = result;
                    }
                }

            }
        }
        //Console.ReadKey(false);
    }

}
