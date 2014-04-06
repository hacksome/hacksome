﻿using System;
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

            List<ITweet> tweets = svc.GeTweetsByKeyWord(searchkey);
            sems.AddRange(tweets.Select(x => new SemanticScoreData.SemanticElement(searchkey, x)));
            var analysisData = _getSemanticDataForTweets(tweets);

            foreach (var sem in sems)
            {
                sem.SemanticData = analysisData.FirstOrDefault(x => x.Tag == sem.TweetData.IdStr);
            }
            var resultGroups = sems.GroupBy(x => x.ProductId);
            var grp = resultGroups.FirstOrDefault();
            var ranking = new Ranking();
            var count = sems.Count;
            var semData = sems.Where(x => x.SemanticData != null).ToList();
            double score=0.0;
            double index = 0.0;
            if (semData.Count>0){
                score =  semData.Sum(x => x.SemanticData.SentimentScore) /semData.Count;
                index = Math.Round(score/count*1000);
            }
            ranking = new Ranking()
                      {
                          Name = searchkey,
                          Score = Math.Round(score * 100),
                          Total = count,
                          Index = index
                      };
            return ranking;
        }

        private IList<DocAnalyticData> _getSemanticDataForTweets(List<ITweet> tweets)
        {
            ISerializer serializer = new JsonSerializer();
            IList<DocAnalyticData> results = new List<DocAnalyticData>();
            // Initializes new session with the serializer weobject and the keys.
            using (Session session = Session.CreateSession(ConsumerKey, ConsumerSecret, serializer))
            {
                // Error callback handler. This event will be rised in case of server-side error
                session.Error += new Session.ErrorHandler(delegate(object sender, ResponseErrorEventArgs ea)
                                                          {
                                                              Console.WriteLine(string.Format("{0}: {1}",
                                                                  (int)ea.Status, ea.Message));
                                                          });

                foreach (ITweet tweet in tweets)
                {
                    string docId = Guid.NewGuid().ToString();
                    // Creates a sample document which need to be processed on Semantria
                    Document doc = new Document()
                    {
                        // Unique document ID
                        Id = string.Format("{0}", docId),
                        // Source text which need to be processed
                        Text = tweet.Text,
                        Tag = tweet.IdStr
                    };

                    // Queues document for processing on Semantria service
                    if (session.QueueDocument(doc) != -1)
                        Console.WriteLine(string.Format("\"{0}\" document queued successfully.", docId));
                }
                // Console.WriteLine();

                // As Semantria isn't real-time solution you need to wait some time before getting of the processed results
                // In real application here can be implemented two separate jobs, one for queuing of source data another one for retreiving
                // Wait ten seconds while Semantria process queued document
                int resCount = tweets.Count;

                while (resCount > 0)
                {
                    System.Threading.Thread.Sleep(2000);

                    // Requests processed results from Semantria service
                    Console.WriteLine("Retrieving your processed results...");
                    ((List<DocAnalyticData>)results).AddRange(session.GetProcessedDocuments());

                    resCount -= results.Count;
                }

            }

            return results;
        }

        //public void test()
        //{
        //    // Initial texts for processing
        //    List<string> initialTexts = new List<string>()
        //                                {
        //                                    @"Lisa - there's 2 Skinny cow coupons available $5 skinny cow ice cream coupons on special k boxes and Printable FPC from facebook - a teeny tiny cup of ice cream. I printed off 2 (1 from my account and 1 from dh's). I couldn't find them instore and i'm not going to walmart before the 19th. Oh well sounds like i'm not missing much ...lol",
        //                                    @"In Lake Louise - a guided walk for the family with Great Divide Nature Tours  rent a canoe on Lake Louise or Moraine Lake  go for a hike to the Lake Agnes Tea House. In between Lake Louise and Banff - visit Marble Canyon or Johnson Canyon or both for family friendly short walks. In Banff  a picnic at Johnson Lake  rent a boat at Lake Minnewanka  hike up Tunnel Mountain  walk to the Bow Falls and the Fairmont Banff Springs Hotel  visit the Banff Park Museum. The ""must-do"" in Banff is a visit to the Banff Gondola and some time spent on Banff Avenue - think candy shops and ice cream.",
        //                                    @"On this day in 1786 - In New York City  commercial ice cream was manufactured for the first time.",
        //                                };

        //    Console.WriteLine("Semantria Document processing mode demo.");

        //    // Creates JSON serializer instance
        //    ISerializer serializer = new JsonSerializer();

        //    // Initializes new session with the serializer object and the keys.
        //    using (Session session = Session.CreateSession(ConsumerKey, ConsumerSecret, serializer))
        //    {
        //        // Error callback handler. This event will be rised in case of server-side error
        //        session.Error += new Session.ErrorHandler(delegate(object sender, ResponseErrorEventArgs ea)
        //                                                  {
        //                                                      Console.WriteLine(string.Format("{0}: {1}",
        //                                                          (int)ea.Status, ea.Message));
        //                                                  });

        //        foreach (string text in initialTexts)
        //        {
        //            string docId = Guid.NewGuid().ToString();
        //            // Creates a sample document which need to be processed on Semantria
        //            Document doc = new Document()
        //                           {
        //                               // Unique document ID
        //                               Id = string.Format("{0}", docId),
        //                               // Source text which need to be processed
        //                               Text = text
        //                           };

        //            // Queues document for processing on Semantria service
        //            if (session.QueueDocument(doc) != -1)
        //                Console.WriteLine(string.Format("\"{0}\" document queued successfully.", docId));
        //        }
        //        Console.WriteLine();

        //        // As Semantria isn't real-time solution you need to wait some time before getting of the processed results
        //        // In real application here can be implemented two separate jobs, one for queuing of source data another one for retreiving
        //        // Wait ten seconds while Semantria process queued document
        //        int resCount = initialTexts.Count;
        //        IList<DocAnalyticData> results = new List<DocAnalyticData>();
        //        while (resCount > 0)
        //        {
        //            System.Threading.Thread.Sleep(2000);

        //            // Requests processed results from Semantria service
        //            Console.WriteLine("Retrieving your processed results...");
        //            ((List<DocAnalyticData>)results).AddRange(session.GetProcessedDocuments());

        //            resCount -= results.Count;
        //        }

        //        foreach (DocAnalyticData data in results)
        //        {
        //            // Printing of document sentiment score
        //            Console.WriteLine(string.Format("Document {0}. Sentiment score: {1}", data.Id, data.SentimentScore));

        //            // Printing of document themes
        //            if (data.Themes != null && data.Themes.Count > 0)
        //            {
        //                Console.WriteLine("Document themes:");
        //                foreach (DocTheme theme in data.Themes)
        //                    Console.WriteLine(string.Format("\t{0} (sentiment: {1})", theme.Title, theme.SentimentScore));
        //            }

        //            // Printing of document entities
        //            if (data.Entities != null && data.Entities.Count > 0)
        //            {
        //                Console.WriteLine("Entities:");
        //                foreach (DocEntity entity in data.Entities)
        //                    Console.WriteLine(string.Format("\t{0} : {1} (sentiment: {2})", entity.Title,
        //                        entity.EntityType, entity.SentimentScore));
        //            }

        //            Console.WriteLine();
        //        }

        //    }
        //    //Console.ReadKey(false);
        //}

    }

}
