#nullable enable

#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
    public sealed class CollectionBuilderAttribute : Attribute
    {
        /// <summary>Initializes a new instance of <see cref="T:System.Runtime.CompilerServices.CollectionBuilderAttribute" /> that refers to the <paramref name="methodName" /> method on the <paramref name="builderType" /> type.</summary>
        /// <param name="builderType">The type of the builder to use to construct the collection.</param>
        /// <param name="methodName">The name of the method on the builder to use to construct the collection.</param>
        public CollectionBuilderAttribute(Type builderType, string methodName)
        {
            this.BuilderType = builderType;
            this.MethodName = methodName;
        }

        /// <summary>Gets the type of the builder to use to construct the collection.</summary>
        public Type BuilderType { get; }

        /// <summary>Gets the name of the method on the builder to use to construct the collection.</summary>
        public string MethodName { get; }
    }
}
#endif