using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace comScoreSocialDashboard.services
{
    [DataContract]
    public class SearchResult
    {
        [DataMember]
        public Ranking Ranking { get; set; }
        [DataMember]
        public List<TweetInfo> TweetInfos { get; set; }
    }

    public class SearchService
    {
        public SearchResult GeSearchResult(string key)
        {
            SearchResult result = new SearchResult
                                  {
                                      TweetInfos =
                                          new Twitterservice().GetSearchByKeyWordAndLocation(true,
                                              new List<string> {key}).Take(10).ToList(),
                                      Ranking = new SemantriaService().GetRanking(key)
                                  };

            return result;
        }
    
    }
}