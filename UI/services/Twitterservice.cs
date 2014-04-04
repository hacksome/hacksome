using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Tweetinvi;
using TweetinviCore.Enum;
using TweetinviCore.Interfaces;

namespace comScoreSocialDashboard.services
{
    public class Twitterservice
    {

        const string AccessToken = "2374223100-g9mrE49jDRjc0YZrubFHgxzR4meZtsgU4XjCfV6";
        const string AccessTokenSecret = "YVLwV55gU6GjYG9hKpzzhAKfB5GoAvO5EKgjfjIbm81Ta";
        const string TokenConsumerKey = "YKwZWc7vmSU8svBHLzM4yQ";
        const string TokenConsumerSecret = "vZEYQ98YpJP41SGTuXh6hw2KnsiizkNERhnotr4NbM";

        public Twitterservice()
        {
            TwitterCredentials.SetCredentials(AccessToken, AccessTokenSecret, TokenConsumerKey, TokenConsumerSecret);
        }

        public class TwitterUser
        {
            public string Name { get; set; }
            public string Location { get; set; }
            public int FollowerCount { get; set; }
            public int FavoriteCount { get; set; }
            public int FriendsCount { get; set; }
        }

        public void TestThsi()
        {
            
        }
        public TwitterUser  GetTwitterUser()
        {
            var x = User.GetUserFromScreenName("comscore");

            var y = x.ProfileImageUrl;
            var z = x.Timeline;
        
            return new TwitterUser
                   {
                       Name = x.Name,
                       Location= x.Location,
                       FollowerCount = x.FollowersCount,
                       FavoriteCount = x.FavouritesCount,
                       FriendsCount = x.FriendsCount,
                       
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

        public IList<ITweet> GetSearchByKeyWordAndLocation(List<string> keywords)
        {    var searchParameter = Search.GenerateSearchTweetParameter("justin bieber");

          //  searchParameter.Locale = "Us";
            searchParameter.SetGeoCode(Geo.GenerateCoordinates(-122.398720, 37.781157), 100, DistanceMeasure.Miles);
            searchParameter.Lang = Language.English;
            searchParameter.SearchType = SearchResultType.Popular;
            searchParameter.MaximumNumberOfResults = 100;
          //  searchParameter.Until = new DateTime(2014, 12, 1);
            //searchParameter.SinceId = 399616835892781056;
           // searchParameter.MaxId = 405001488843284480;

            return Search.SearchTweets(searchParameter);
        } 

        private static void Search_SearchTweet()
        {
            // IF YOU DO NOT RECEIVE ANY TWEET, CHANGE THE PARAMETERS!

            var searchParameter = Search.GenerateSearchTweetParameter("obama");

            searchParameter.SetGeoCode(Geo.GenerateCoordinates(-122.398720, 37.781157), 1, DistanceMeasure.Miles);
            searchParameter.Lang = Language.English;
            searchParameter.SearchType = SearchResultType.Popular;
            searchParameter.MaximumNumberOfResults = 100;
            searchParameter.Until = new DateTime(2013, 12, 1);
            searchParameter.SinceId = 399616835892781056;
            searchParameter.MaxId = 405001488843284480;

            var tweets = Search.SearchTweets(searchParameter);
            tweets.ForEach(t => Console.WriteLine(t.Text));
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