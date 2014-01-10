using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace Hl7.Fhir.Rest
{
    public static class WebRequestExtensions
    {

        public static WebResponse EndGetResponseNoEx(this WebRequest req, IAsyncResult ar)
        {
            try
            {
                return (HttpWebResponse)req.EndGetResponse(ar);
            }
            catch (WebException we)
            {
                var resp = we.Response as HttpWebResponse;
                if (resp == null)
                    throw;
                return resp;
            }
        }

        public static WebResponse GetResponseNoEx(this WebRequest req)
        {
            WebResponse result = null;
            ManualResetEvent responseReady = new ManualResetEvent(false);

            AsyncCallback callback = new AsyncCallback(ar =>
                {
                        var request = (WebRequest)ar.AsyncState;
                        result = request.EndGetResponseNoEx(ar);
                        responseReady.Set();
                });

            var async = req.BeginGetResponse(callback, req);

            // Not having thread affinity seems to work better with ManualResetEvent
            // Using AsyncWaitHandle.WaitOne() gave unpredictable results (in the
            // unit tests), when EndGetResponse would return null without any error
            // thrown
            responseReady.WaitOne();
            //async.AsyncWaitHandle.WaitOne();

            return result;
        }
    }
}
