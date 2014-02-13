using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public static class UriParamExtensions
    {
        //public static Uri AddParam(this Uri uri, string name, params string[] values)
        //{
        //    UriBuilder builder = new UriBuilder(uri);
        //    ICollection<Tuple<string, string>> paramlist = HttpUtil.SplitParams(builder.Query).ToList();

        //    foreach (string value in values)
        //        paramlist.Add(new Tuple<string, string>(name, value));

        //    builder.Query = HttpUtil.JoinParams(paramlist);
             
        //    return builder.Uri;
        //}
    }
}
