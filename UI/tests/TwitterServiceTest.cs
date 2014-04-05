using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using comScoreSocialDashboard.services;
using Geocoding;
using Geocoding.Google;
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

            var tweets = new Twitterservice().GetSearchByKeyWordAndLocation(true).OrderBy(x => x.Date).ToList();
            
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

        [Test]
        public void GeoCode()
        {
            GoogleGeocoder geocoder = new GoogleGeocoder() ;
            var addresses = geocoder.Geocode("Mumbai, India");

            var country = addresses.Where(a => !a.IsPartialMatch).Select(a => a[GoogleAddressType.Country]).First();
            Console.WriteLine("Country: " + addresses.ToList()[0].Coordinates.Latitude + ", " + country.ShortName);
//            IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyDrgMqxT3DIKg4PQkuqNeqbjOCZwLQKxc0" };
//       var addresses = geocoder.Geocode("").ToArray();
////Console.WriteLine("Formatted: " +addresses  .FormattedAddress); //Formatted: 1600 Pennslyvania Avenue Northwest, Presiden'ts Park, Washington, DC 20500, USA
//        Console.WriteLine("Coordinates: " + addresses[0].Coordinates.Latitude + ", " + addresses[0].Coordinates.Longitude); //Coordinates: 38.8978378, -77.0365123
        }

    }
}