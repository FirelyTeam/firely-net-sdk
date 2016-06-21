namespace Hl7.Fhir.FluentPath
{
    public interface INavigator<T>
        where T : INavigator<T>
    {
        bool MoveToNext();
        bool MoveToFirstChild();
        T Clone();
    }
}