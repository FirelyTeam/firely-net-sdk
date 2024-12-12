using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Model;

internal abstract record PocoMapper<TResult>(Func<Base, TResult> BaseMapper, Func<object, TResult> PrimitiveMapper)
{
    internal abstract TResult MapSingle(object instance);
};

internal record TreeMapper<TResult>(
    Func<Base, TResult> BaseMapper,
    Func<object, TResult> PrimitiveMapper,
    Func<IEnumerable<Base>, TResult> BaseListMapper,
    Func<IEnumerable<object>, TResult> PrimitiveListMapper
) : PocoMapper<TResult>(BaseMapper, PrimitiveMapper)
{
    internal override TResult MapSingle(object instance)
    {
        return instance switch
        {
            Base baseInstance => baseInstance.MapSingle(mapper),
            IEnumerable<Base> baseList => mapper.BaseListMapper(baseList),
            IEnumerable<object> primitiveList => mapper.PrimitiveListMapper(primitiveList),
            object primitive => mapper.PrimitiveMapper(primitive),
            _ => throw new InvalidOperationException("Unexpected instance type")
        };
    }
}

internal record FlatMapper<TResult>(Func<Base, TResult> BaseMapper, Func<object, TResult> PrimitiveMapper) : PocoMapper<TResult>(BaseMapper, PrimitiveMapper)
{
    internal override TResult MapSingle(object instance)
    {
        return instance switch
        {
            Base baseInstance => baseInstance.MapSingle(mapper),
            object primitive => mapper.PrimitiveMapper(primitive),
            _ => throw new InvalidOperationException("Unexpected instance type")
        };
    }
}

public static partial class BaseExtensions
{
    internal static TResult MapSingle<TResult>(this Base instance, PocoMapper<TResult> mapper)
    {
        return mapper.BaseMapper(instance);
    }
    
    internal static IEnumerable<TResult> MapChildren<TResult>(this Base instance, PocoMapper<TResult> mapper)
    {
        return instance.EnumerateElements().Select(child => child.Value.MapSingle(mapper));
    }
}