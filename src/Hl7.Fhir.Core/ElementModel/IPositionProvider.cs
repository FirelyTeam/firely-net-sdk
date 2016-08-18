using System;
using System.Linq;
using System.Collections.Generic;

namespace Hl7.ElementModel
{

    public interface IPositionProvider
    {
        // By FluentPath position
    }


    public interface IUnparsedSource
    {
        string OriginalText { get; }

        int Line { get; }
        int Pos { get; }
    }

}