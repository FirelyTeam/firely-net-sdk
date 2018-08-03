/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    internal class ParseErrorStubNavigator : ISourceNavigator, IAnnotated, IExceptionSource
    {
        public ParseErrorStubNavigator(FormatException fe)
        {
            _formatException = fe;
        }

        public string Name => "(UNPARSEABLE SOURCE)";

        public string Text
        {
            get
            {
                ExceptionHandler.NotifyOrThrow(this, ExceptionNotification.Error(_formatException));
                return "(UNPARSEABLE SOURCE)";
            }
        }

        public string Location => "(UNPARSEABLE SOURCE)";

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public ISourceNavigator Clone() =>
            new ParseErrorStubNavigator(_formatException) { ExceptionHandler = ExceptionHandler };

        private FormatException _formatException = null;

        public bool MoveToFirstChild(string nameFilter = null) => false;
        public bool MoveToNext(string nameFilter = null) => false;

        IEnumerable<object> IAnnotated.Annotations(Type type) => Enumerable.Empty<object>();
    }
}
