using System;
using comScoreSocialDashboard.services;
using Newtonsoft.Json;
using NUnit.Framework;

namespace comScoreSocialDashboard.tests
{
    public class SemantriaTest
    {

        [Test]

        public void Test()
        {

         var r=  new SemantriaService().GetRanking("Obama");
         Console.WriteLine(JsonConvert.SerializeObject(r, Formatting.Indented));
            
        }
    }
}