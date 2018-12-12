using Newtonsoft.Json.Linq;

public interface IAssertion : IJsonSerializable
{
}

public interface IJsonSerializable
{
    JToken ToJson();
}
