using System;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Introspection
{
    public interface IElementNavigation
    {
        int Count { get; }
        Profile.ElementComponent Current { get; }
        bool MoveToFirstChild();
        bool MoveToNext();
        bool MoveToParent();
        bool MoveToPrevious();
        void Reset();

        Bookmark Bookmark();
        bool ReturnToBookmark(Bookmark bookmark);
    }
}
