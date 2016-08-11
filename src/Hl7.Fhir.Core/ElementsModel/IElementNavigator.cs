namespace Hl7.Fhir.FluentPath
{
    public interface IElementNavigator : INavigator<IElementNavigator>, ITypeNameProvider, INameProvider, IValueProvider, IPositionProvider
    {
    }

    public interface IPositionProvider
    {
        // By FluentPath position
        // By line/pos position
    }
}