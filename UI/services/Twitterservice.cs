using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Web;
using Geocoding.Google;
using Tweetinvi;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Models;
using System.Runtime.Serialization;

namespace comScoreSocialDashboard.services
{
    [DataContract]
    public class TwitterUser
    {
        [DataMember(EmitDefaultValue = false)]
        public string ScreenName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Location { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int FollowerCount { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int FavoriteCount { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int FriendsCount { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string ProfileImgUrl { get; set; }
    }

    [DataContract]
    public class TweetInfo
    {
        [DataMember]
        public string Msg { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string MsgId { get; set; }
        [DataMember]
        public Double Long { get; set; }
        [DataMember]
        public Double Lat { get; set; }
        [DataMember]
        public TwitterUser User { get; set; }
    }

    public class Twitterservice
    {

        const string AccessToken = "2374223100-g9mrE49jDRjc0YZrubFHgxzR4meZtsgU4XjCfV6";
        const string AccessTokenSecret = "YVLwV55gU6GjYG9hKpzzhAKfB5GoAvO5EKgjfjIbm81Ta";
        const string TokenConsumerKey = "YKwZWc7vmSU8svBHLzM4yQ";
        const string TokenConsumerSecret = "vZEYQ98YpJP41SGTuXh6hw2KnsiizkNERhnotr4NbM";

        private List<string> _preferedkeywordList = new List<string>
                                        {
                                            "comscore", 
                                            "comscore vce, comScore Validated Campaign Essentials", 
                                            "comScore Video Metrix", 
                                            "comScore multiplatform, Media Metrix Multi-Platform", 
                                            "comScore digital analytics",
                                            "comScore Ad Metrix",
                                            "comScore Mobile Metrix",
                                            "comScore PlanMetrix",
                                            "comscore mce, comScore Validated Media Essentials",
                                            "comScore Action Lift",
                                            "comScore Survey Lift",
                                            "comScore Subscriber Analytics",
                                            "comScore Subscriber Analytics Marketing",
                                            "comScore Offline Sales Lift",
                                            "comScore e-Commerce Measurement",
                                            "comScore Mobile Metrix",
                                            "comScore Social Essentials",
                                            "comScore Segment Metrix",
                                            "comScore qSearch",
                                            "comScore Device Essentials",
                                            "comScore Reach/Frequency"
                                        };
        public Twitterservice()
        {
            TwitterCredentials.SetCredentials(AccessToken, AccessTokenSecret, TokenConsumerKey, TokenConsumerSecret);
        }


        public TwitterUser GetTwitterUser()
        {
            var x = User.GetUserFromScreenName("comscore");

            var y = x.ProfileImageUrl;
            var z = x.Timeline;

            return new TwitterUser
                   {
                       ScreenName = x.Name,
                       Location = x.Location,
                       FollowerCount = x.FollowersCount,
                       FavoriteCount = x.FavouritesCount,
                       FriendsCount = x.FriendsCount

                   };
            // User_GetCurrentUser();

            // User_GetUserFromName(1478171);
            // User_GetUserFromName(USER_SCREEN_NAME_TO_TEST);

            // User_GetFavorites(USER_SCREEN_NAME_TO_TEST);

            // User_GetFriends(USER_SCREEN_NAME_TO_TEST);
            // User_GetFriendIds(USER_SCREEN_NAME_TO_TEST);
            // User_GetFriendIdsUpTo(USER_SCREEN_NAME_TO_TEST, 10000);

            // User_GetFollowers(USER_SCREEN_NAME_TO_TEST);
            // User_GetFollowerIds(USER_SCREEN_NAME_TO_TEST);
            // User_GetFollowerIdsUpTo(USER_SCREEN_NAME_TO_TEST, 10000);

            // User_GetRelationshipBetween("tweetinvitest", USER_SCREEN_NAME_TO_TEST);

            // User_BlockUser(USER_SCREEN_NAME_TO_TEST);
            // User_DownloadProfileImage(USER_SCREEN_NAME_TO_TEST);
            // User_DownloadProfileImageAsync(USER_SCREEN_NAME_TO_TEST);
            // User_GenerateProfileImageStream(USER_SCREEN_NAME_TO_TEST);
            // User_GenerateProfileImageBitmap(USER_SCREEN_NAME_TO_TEST);
        }


        #region Search

        public List<ITweet> GeTweetsByKeyWord(string keyword)
        {
            // IF YOU DO NOT RECEIVE ANY TWEET, CHANGE THE PARAMETERS!
            return Search.SearchTweets(keyword);
        }

        public List<TweetInfo> GetSearchByKeyWordAndLocation(bool geoCode = false,  List<string> keywords = null)
        {
            keywords = keywords ?? _preferedkeywordList;
            GoogleGeocoder geocoder = new GoogleGeocoder() ;
            List<TweetInfo> infoList = new List<TweetInfo>();
            foreach (var keyword in keywords)
            {
                var words = keyword.Split(new[] { ',' });
                foreach (string t in words)
                {
                    var searchParameter = Search.GenerateSearchTweetParameter(t);
                   
                   // searchParameter.
                    var tweets = Search.SearchTweets(searchParameter);
                    foreach (var tweet in tweets)
                    {
                        double latitude = 0.0;
                        double longitude = 0.0;

                        var info = new TweetInfo
                                     {
                                         Msg = tweet.Text,
                                         MsgId = tweet.IdStr,
                                         Date = tweet.CreatedAt,
                                         User = new TwitterUser
                                                {
                                                    ScreenName = tweet.Creator.ScreenName,
                                                    ProfileImgUrl = tweet.Creator.ProfileImageUrl,
                                                    Location = tweet.Creator.Location
                                                }
                                     };
                        if (geoCode)
                        {
                            if (tweet.Coordinates != null)
                            {
                                latitude = tweet.Coordinates.Latitude;
                                longitude = tweet.Coordinates.Longitude;
                            } 
                            else if (!String.IsNullOrEmpty(tweet.Creator.Location))
                            {
                                try
                                {
                                    var address = geocoder.Geocode(tweet.Creator.Location.Trim()).ToList();
                                    if (address.Count > 0)
                                    {
                                        latitude = address.ToList()[0].Coordinates.Latitude;
                                        longitude = address.ToList()[0].Coordinates.Longitude;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(tweet.Creator.Location);
                                }
                            }
                            info.Lat = latitude;
                            info.Long = longitude;
                                      
                        }
                        infoList.Add(info);
                    }
                }
            }
            return infoList;
        }


        private static void Search_FilteredSearch()
        {
            var searchParameter = Search.GenerateSearchTweetParameter("#tweetinvi");
            searchParameter.TweetSearchFilter = TweetSearchFilter.OriginalTweetsOnly;

            var tweets = Search.SearchTweets(searchParameter);
            tweets.ForEach(t => Console.WriteLine(t.Text));
        }

        private static void Search_SearchAndGetMoreThan100Results()
        {
            var searchParameter = Search.GenerateSearchTweetParameter("us");
            searchParameter.MaximumNumberOfResults = 200;

            var tweets = Search.SearchTweets(searchParameter);
            tweets.ForEach(t => Console.WriteLine(t.Text));
        }
        #endregion
    }
}