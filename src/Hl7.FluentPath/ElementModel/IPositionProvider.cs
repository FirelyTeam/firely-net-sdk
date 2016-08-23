using System;
using System.Linq;
using System.Collections.Generic;

namespace Hl7.ElementModel
{
    public interface ITextSource
    {
        string OriginalText { get; }

        int SpanStart { get; }
        int SpanEnd { get; }
    }

}