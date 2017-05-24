/* 
 * Copyright (c) 2017+ brianpos, Furore and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Hl7.Fhir.WebApi
{
    public class BinaryFhirFormatter : FhirMediaTypeFormatter
    {
        public BinaryFhirFormatter() : base()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(FhirMediaType.BinaryResource));
        }

        public override bool CanReadType(Type type)
        {
            if (typeof(Resource).IsAssignableFrom(type) || type == typeof(OperationOutcome))
                return true;
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            if (typeof(Resource).IsAssignableFrom(type) || type == typeof(OperationOutcome))
                return true;
            return false;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            MemoryStream stream = new MemoryStream();
            readStream.CopyTo(stream);

            IEnumerable<string> xContentHeader;
            var success = content.Headers.TryGetValues("X-Content-Type", out xContentHeader);

            if (!success)
            {
                throw new HttpException((int)System.Net.HttpStatusCode.BadRequest, "POST to binary must provide a Content-Type header");
            }

            string contentType = xContentHeader.FirstOrDefault();

            Binary binary = new Binary();
            binary.Content = stream.ToArray();
            binary.ContentType = contentType;

            return System.Threading.Tasks.Task.FromResult<object>(binary);
        }

        private string GetFileExtensionForMimeType(string mimetype)
        {
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mimetype, false);
            if (regKey != null && regKey.GetValue("Extension") != null)
                return regKey.GetValue("Extension").ToString();
            return null;
        }

        public override System.Threading.Tasks.Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, System.Net.TransportContext transportContext)
        {
            if (type == typeof(Binary))
            {
                Binary binary = (Binary)value;

                content.Headers.ContentType = new MediaTypeHeaderValue(binary.ContentType);
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = String.Format("fhir_binary_{0}_{1}.{2}",
                    binary.Id,
                    binary.Meta != null ? binary.Meta.VersionId : "0",
                    GetFileExtensionForMimeType(binary.ContentType))
                };

                var stream = new MemoryStream(binary.Content);
                stream.CopyTo(writeStream);
                stream.Flush();
            }
            if (type == typeof(OperationOutcome))
            {
            }
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}