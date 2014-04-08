using System;
using comScoreSocialDashboard.services;
using Newtonsoft.Json;
using NUnit.Framework;

namespace comScoreSocialDashboard.tests
{
    public class SearchServiceTest
    {
        [Test]
        public void GetSearchResult()
        {

            var res = new SearchService().GeSearchResult("honda");

             Console.WriteLine(JsonConvert.SerializeObject(res, Formatting.Indented));
            
         
        }
    }
}