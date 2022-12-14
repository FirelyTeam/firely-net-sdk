/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

#nullable enable

namespace Hl7.Fhir.Utility
{
    /// <summary>
    /// Utility methods for generating delegates to support the deserializer.
    /// </summary>
    public static class PropertyInfoExtensions
    {
        // Will be set to true when we detect the platform has no codegen support.
        // This happens the first time we catch a PlatformNotSupportedException.
        public static bool NoCodeGenSupport { get; set; } = false;

        /// <summary>
        /// Generates a function that creates an instance of the given type.
        /// </summary>
        public static Func<object> BuildFactoryMethod(this Type type)
        {
            var ti = type.GetTypeInfo();

            if (!ti.IsClass) throw new NotSupportedException($"Can only create factory methods for classes (which {type} is not).");

            var constructor = ti.GetConstructor(Type.EmptyTypes);
            if (constructor is null) throw new NotSupportedException($"Cannot generate factory method for type {type}: there is no default constructor.");

            try
            {
                if (NoCodeGenSupport) return createInstance;

                DynamicMethod getter = new($"{type.Name}_new", typeof(object), Type.EmptyTypes);
                ILGenerator il = getter.GetILGenerator();

                il.Emit(OpCodes.Newobj, constructor);
                if (ti.IsValueType)
                    il.Emit(OpCodes.Box, type);

                il.Emit(OpCodes.Ret);

                return (Func<object>)getter.CreateDelegate(typeof(Func<object>));
            }
            catch (PlatformNotSupportedException)
            {
                NoCodeGenSupport = true;
                return createInstance;
            }

            object createInstance() => Activator.CreateInstance(type)!;
        }

        /// <summary>
        /// Generates a function that creates an instance of a list of the given type.
        /// </summary>
        /// <remarks>The returned instance will actually be of type <see cref="List{T}"/> where T is the given type.</remarks>
        public static Func<IList> BuildListFactoryMethod(this Type type)
        {
            // Note that MakeGenericType() will return the same Type instance for the same List<T> type instantiations,
            // so we don't have to cache the result.
            var listType = typeof(List<>).MakeGenericType(type);
            var constructor = listType.GetTypeInfo().GetConstructor(Type.EmptyTypes)
                ?? throw new ArgumentException($"Type {type.Name} does not have a parameterless constructor.");

            try
            {
                if (NoCodeGenSupport) return createList;

                DynamicMethod getter = new($"new_list_of_{type.Name}", typeof(IList), Type.EmptyTypes);
                ILGenerator il = getter.GetILGenerator();

                il.Emit(OpCodes.Newobj, constructor);
                il.Emit(OpCodes.Ret);

                return (Func<IList>)getter.CreateDelegate(typeof(Func<IList>));
            }
            catch (PlatformNotSupportedException)
            {
                NoCodeGenSupport = true;
                return createList;
            }

            IList createList() => (IList)Activator.CreateInstance(listType)!;
        }

        /// <summary>
        /// Generates a function that, when passed an instance, gets the value of the given property.
        /// </summary>
        public static Func<T, object> GetValueGetter<T>(this PropertyInfo propertyInfo)
        {
            MethodInfo getMethod = propertyInfo.GetMethod ?? throw new InvalidOperationException($"Property {propertyInfo.Name} does not have a getter.");

            if (typeof(T) != propertyInfo.DeclaringType && typeof(T) != typeof(object))
                throw new ArgumentException("Generic param T should be the type of property's declaring class.", nameof(propertyInfo));

            try
            {
                if (NoCodeGenSupport) return getValue;

                DynamicMethod getter = new($"{propertyInfo.Name}_get", typeof(object), new Type[] { typeof(object) },
                    propertyInfo.DeclaringType!);

                ILGenerator il = getter.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Castclass, propertyInfo.DeclaringType!);
                il.EmitCall(OpCodes.Callvirt, getMethod, null);

                if (propertyInfo.PropertyType.GetTypeInfo().IsValueType)
                    il.Emit(OpCodes.Box, propertyInfo.PropertyType);

                il.Emit(OpCodes.Ret);

                return (Func<T, object>)getter.CreateDelegate(typeof(Func<T, object>));
            }
            catch (PlatformNotSupportedException)
            {
                NoCodeGenSupport = true;
                return getValue;
            }

            object getValue(T instance) => propertyInfo.GetValue(instance, null)!;
        }

        /// <summary>
        /// Generates a function that, when passed an object instance, gets the value of the given property.
        /// </summary>   
        public static Func<object, object?> GetValueGetter(this PropertyInfo propertyInfo) =>
            GetValueGetter<object?>(propertyInfo);

        /// <summary>
        /// Generates a function that, when passed an instance and a value, sets the value of the given property.
        /// </summary>
        public static Action<T, object?> GetValueSetter<T>(this PropertyInfo propertyInfo)
        {
            MethodInfo setMethod = propertyInfo.SetMethod ?? throw new InvalidOperationException($"Property {propertyInfo.Name} does not have a setter."); ;

            if (typeof(T) != propertyInfo.DeclaringType && typeof(T) != typeof(object))
                throw new ArgumentException("Generic param T should be the type of property's declaring class.", nameof(propertyInfo));

            try
            {
                if (NoCodeGenSupport) return setValue;

                Type[] arguments = new Type[] { typeof(object), typeof(object) };
                DynamicMethod setter = new($"{propertyInfo.Name}_set", typeof(object), arguments, propertyInfo.DeclaringType!, true);
                ILGenerator il = setter.GetILGenerator();

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Castclass, propertyInfo.DeclaringType!);
                il.Emit(OpCodes.Ldarg_1);

                if (propertyInfo.PropertyType.GetTypeInfo().IsClass)
                    il.Emit(OpCodes.Castclass, propertyInfo.PropertyType);
                else
                    il.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);

                il.EmitCall(OpCodes.Callvirt, setMethod, null);
                il.Emit(OpCodes.Ldarg_0);

                il.Emit(OpCodes.Ret);

                var del = (Func<T, object?, object>)setter.CreateDelegate(typeof(Func<T, object?, object>));
                void actionDelegate(T obj, object? val) => del(obj, val);

                return actionDelegate;
            }
            catch (PlatformNotSupportedException)
            {
                NoCodeGenSupport = true;
                return setValue;
            }

            void setValue(T instance, object? value) => propertyInfo.SetValue(instance, value, null);
        }

        /// <summary>
        /// Generates a function that, when passed an object instance and a value, sets the value of the given property.
        /// </summary>
        public static Action<object, object?> GetValueSetter(this PropertyInfo propertyInfo) => GetValueSetter<object>(propertyInfo);

#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER

        public static Func<C, T> GetField<C, T>(string fieldName)
        {
            FieldInfo? field = typeof(C).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field is null) throw new ArgumentException($"Cannot find field {fieldName} in type {typeof(C).Name}.", nameof(fieldName));

            try
            {
                if (NoCodeGenSupport) return getField;

                Type[] arguments = new Type[] { typeof(C) };
                DynamicMethod setter = new($"getField_{fieldName}", typeof(T), arguments, typeof(C), true);
                ILGenerator il = setter.GetILGenerator();

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, field);
                il.Emit(OpCodes.Ret);

                return (Func<C, T>)setter.CreateDelegate(typeof(Func<C, T>));
            }
            catch (PlatformNotSupportedException)
            {
                NoCodeGenSupport = true;
                return getField;
            }

            T getField(C instance) => (T)field.GetValue(instance)!;
        }
#endif
    }
}

#nullable restore