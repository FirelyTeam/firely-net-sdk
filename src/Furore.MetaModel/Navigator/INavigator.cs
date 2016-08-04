namespace Furore.MetaModel
{
    public interface INavigator<out T>
        where T : INavigator<T>
    {
        bool MoveToNext();
        bool MoveToFirstChild();
        T Clone();
    }
}