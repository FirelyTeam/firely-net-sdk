#nullable enable

/* 
 * This code has been copied and adapted from https://github.com/WebApiContrib/WebAPIContrib/blob/master/src/WebApiContrib/Content/CompressedContent.cs
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    internal class CompressedContent : HttpContent
    {
        private readonly HttpContent _wrapped;
        private readonly DecompressionMethods _method;

        public CompressedContent(HttpContent wrapped, DecompressionMethods method)
        {
            _wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
            _method = method;

            bool supported = _method is DecompressionMethods.GZip or DecompressionMethods.Deflate;

            if (!supported)
                throw new NotSupportedException($"Encoding '{_method}' is not supported. Only supports gzip or deflate encoding.");

            foreach (KeyValuePair<string, IEnumerable<string>> header in _wrapped.Headers)
                    Headers.TryAddWithoutValidation(header.Key, header.Value);

            Headers.ContentEncoding.Add(ContentType.DecompressionMethodHeaderValue(_method));
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context)
        {
            Stream compressedStream = _method switch
            {
                DecompressionMethods.GZip => new GZipStream(stream, CompressionMode.Compress, leaveOpen: true),
                DecompressionMethods.Deflate => new DeflateStream(stream, CompressionMode.Compress, leaveOpen: true),
                _ => throw new InvalidOperationException("Unsupported compression mode found.")
            }; 

            return _wrapped
                .CopyToAsync(compressedStream)
                .ContinueWith(tsk =>  { compressedStream?.Dispose();  });
        }
    }
}

#nullable restore