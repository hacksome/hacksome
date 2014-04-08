using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using comScoreSocialDashboard.services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Semantria.Com;
using Semantria.Com.Mapping;
using Semantria.Com.Mapping.Output;
using Semantria.Com.Serializers;
using TweetinviCore.Interfaces;

namespace comScoreSocialDashboard
{
    /// <summary>
    /// Summary description for search
    /// </summary>
    public class search : IHttpHandler
    {
        
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (String.IsNullOrEmpty(context.Request.QueryString["q"])) return;
            var svc = new SearchService();
            var search = svc.GeSearchResult(context.Request.QueryString["q"].Trim());
            var res = JsonConvert.SerializeObject(search);
            context.Response.Write(res);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}