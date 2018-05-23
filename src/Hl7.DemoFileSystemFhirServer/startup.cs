using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Owin;
using System.Web.Http;
using Hl7.Fhir.WebApi;

namespace Hl7.DemoFileSystemFhirServer
{
    public class Startup
    {
        // This test stuff was based off the code found here:
        // http://www.asp.net/web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            DirectorySystemService.Directory = @"c:\temp\demoserver";
            if (!System.IO.Directory.Exists(DirectorySystemService.Directory))
                System.IO.Directory.CreateDirectory(DirectorySystemService.Directory);

            // Configure Web API for self-host.
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config, new DirectorySystemService()); // this is from the actual WebAPI Project
            appBuilder.UseWebApi(config);
        }
    }
}
