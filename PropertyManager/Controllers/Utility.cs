using PropertyManager.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace PropertyManager.Controllers
{
    public static class Utility
    {
        public static bool IsRequestForMediaItem(HttpRequestHeaders headers)
        {
           
            if (headers.Accept.Count != 1) { return false; }

            var acceptHeader = headers.Accept.First().MediaType.ToString();

            if (acceptHeader.EndsWith("/*"))
            {
                
                acceptHeader = acceptHeader.Substring(0, acceptHeader.IndexOf("/*"));

                var formatter = new ByteFormatter();
                var mthv = formatter.SupportedMediaTypes
                    .FirstOrDefault(smt => smt.MediaType.StartsWith(acceptHeader));
                return (mthv == null) ? false : true;
            }
            else
            {           
                var formatter = new ByteFormatter();
                var mthv = formatter.SupportedMediaTypes
                    .FirstOrDefault(smt => smt.MediaType == acceptHeader);
                return (mthv == null) ? false : true;
            }
        }
    }
}