namespace Hl7.ElementModel
{
    public interface INavigator<out T>
        where T : INavigator<T>
    {
        bool MoveToNext();
        bool MoveToFirstChild();
        T Clone();
    }
}