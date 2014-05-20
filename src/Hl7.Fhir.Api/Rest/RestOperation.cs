/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public static class RestOperation
    {
        public const string METADATA = "metadata";
        public const string HISTORY = "_history";
        public const string SEARCH = "_search";
        public const string VALIDATE = "_validate";
        public const string TAGS = "_tags";
        public const string DELETE = "_delete";

        public const string MAILBOX = "Mailbox";
        public const string DOCUMENT = "Document";

    }
}
