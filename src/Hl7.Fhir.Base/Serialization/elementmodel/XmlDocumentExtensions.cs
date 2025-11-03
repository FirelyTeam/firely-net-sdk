/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization;

internal static class XDocumentExtensions
{
    internal static void writeTo(this XDocument doc, XmlWriter destination)
    {
        if (doc.Root != null)
            doc.WriteTo(destination);

        destination.Flush();
    }

    internal static async Task writeToAsync(this XDocument doc, XmlWriter destination)
    {
        if (doc.Root != null)
            await doc.WriteToAsync(destination, CancellationToken.None);

        await destination.FlushAsync().ConfigureAwait(false);
    }
}