#if USE_CODE_GEN

using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Hl7.Fhir.Utility
{
    public static class PropertyInfoExtensions
    {
        public static Func<T, object> GetValueGetter<T>(this PropertyInfo propertyInfo)
        {
            MethodInfo getMethod = propertyInfo.GetMethod;

            if (getMethod == null)
                throw new InvalidOperationException("Property has no get method.");

            if (typeof(T) != propertyInfo.DeclaringType && typeof(T) != typeof(object))
                throw new ArgumentException("Generic param T should be the type of property's declaring class.", nameof(propertyInfo));

            DynamicMethod getter = new DynamicMethod($"{propertyInfo.Name}_get", typeof(object), new Type[] { typeof(object) }, propertyInfo.DeclaringType);

            ILGenerator il = getter.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
            il.EmitCall(OpCodes.Callvirt, getMethod, null);

            if (propertyInfo.PropertyType.GetTypeInfo().IsValueType)
                il.Emit(OpCodes.Box, propertyInfo.PropertyType);

            il.Emit(OpCodes.Ret);

            return (Func<T, object>)getter.CreateDelegate(typeof(Func<T, object>));
        }

        public static Func<object, object> GetValueGetter(this PropertyInfo propertyInfo)
        {
            return GetValueGetter<object>(propertyInfo);
        }

        public static Action<T, object> GetValueSetter<T>(this PropertyInfo propertyInfo)
        {
            MethodInfo setMethod = propertyInfo.SetMethod;

            if (setMethod == null)
                throw new InvalidOperationException("Property has no set method.");

            if (typeof(T) != propertyInfo.DeclaringType && typeof(T) != typeof(object))
                throw new ArgumentException("Generic param T should be the type of property's declaring class.", nameof(propertyInfo));

            Type[] arguments = new Type[] { typeof(object), typeof(object) };
            DynamicMethod setter = new DynamicMethod($"{propertyInfo.Name}_set", typeof(object), arguments, propertyInfo.DeclaringType, true);
            ILGenerator il = setter.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
            il.Emit(OpCodes.Ldarg_1);

            if (propertyInfo.PropertyType.GetTypeInfo().IsClass)
                il.Emit(OpCodes.Castclass, propertyInfo.PropertyType);
            else
                il.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);

            il.EmitCall(OpCodes.Callvirt, setMethod, null);
            il.Emit(OpCodes.Ldarg_0);

            il.Emit(OpCodes.Ret);

            var del = (Func<T, object, object>)setter.CreateDelegate(typeof(Func<T, object, object>));
            Action<T, object> actionDelegate = (obj, val) => del(obj, val);

            return actionDelegate;
        }

        public static Action<object, object> GetValueSetter(this PropertyInfo propertyInfo)
        {
            return GetValueSetter<object>(propertyInfo);
        }
    }
}

#endif