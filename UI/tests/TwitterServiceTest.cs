using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using comScoreSocialDashboard.services;
using Newtonsoft.Json;
using NUnit.Framework;

namespace comScoreSocialDashboard.tests
{
    [TestFixture]
    public class TwitterServiceTest
    {
        [Test]
        public void TestUser()
        {
            var x = new Twitterservice().GetTwitterUser();

            Console.WriteLine();
            
        }

        [Test]
        public void TestSearch()
        {
            var keys = new List<string>() { "comscore", 
                "comscore vce", "comScore Video Metrix", 
                "comScore multiplatform", "comScore digital analytics" };
            foreach (var key in keys)
            {
                var tweets = new Twitterservice().GeTweetsByKeyWord(key);
                Console.WriteLine(key + " " + tweets.Count());
                tweets = tweets.OrderBy(x =>x.RetweetCount).Take(2).ToList();
                foreach (var tweet in tweets)
                {
                    Console.WriteLine(tweet.Text);
                    //Console.WriteLine(tweet.);
                }
                Console.WriteLine();
            }
        }

        [Test]
        public void TestGeoSearch()
        {

            var tweets = new Twitterservice().GetSearchByKeyWordAndLocation().OrderBy(x => x.Date).ToList();
            
            Console.WriteLine(tweets[0].Date);
            Console.WriteLine(tweets.Count);
            
           Console.WriteLine(JsonConvert.SerializeObject(tweets, Formatting.Indented));
            
            //foreach (var tweet in tweets)
            //{
            //    Console.WriteLine(tweet.Coordinates);
            //    Console.WriteLine(tweet.Msg);
            //    //Console.WriteLine(tweet.Creator.ProfileBackgroundColor);

            //    //Console.WriteLine(tweet.);
            //}
        }

    }
}