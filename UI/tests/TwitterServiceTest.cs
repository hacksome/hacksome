using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using comScoreSocialDashboard.services;
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
            Console.WriteLine(x.Name);
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

            var tweets = new Twitterservice().GetSearchByKeyWordAndLocation(new List<string>{"agjag"});
            
            Console.WriteLine(tweets.Count);
            foreach (var tweet in tweets)
            {
                Console.WriteLine(tweet.Text);
                //Console.WriteLine(tweet.);
            }
        }

    }
}