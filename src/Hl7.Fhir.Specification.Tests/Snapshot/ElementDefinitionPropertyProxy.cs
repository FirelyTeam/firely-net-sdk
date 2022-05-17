using FluentAssertions;
using Hl7.Fhir.Model;
using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Specification.Tests
{
    /// <summary>
    /// Proxy for accessing an element definition property by name using reflection.
    /// The property can also be a specific item from a property list.
    /// The property name is specified in the constructor.
    /// 
    /// The name is either the name of the property (e.g. "Binding") or the name of the property list
    /// with a item selector (e.g. "Constraint[Key:dom-2]").
    /// </summary>
    internal class ElementDefinitionPropertyProxy
    {
        private string _propertyName;
        private string _selectorPropertyName;
        private string _selectorValue;
        private PropertyInfo _propertyInfo;
        private PropertyInfo _selectorPropertyInfo;

        public ElementDefinitionPropertyProxy(string propertyName)
        {
            getPropertyInfo(propertyName);
        }

        /// <summary>
        /// Create a new instance of the property.
        /// If the property is a list then an instance of the list item type is created as well.
        /// </summary>
        /// <param name="element">Optional element to initialize the new list item with.</param>
        /// <returns></returns>
        public object CreateInstance(Element element)
        {
            var instance = Activator.CreateInstance(_propertyInfo.PropertyType);

            if (_selectorPropertyInfo == null)
                return instance; // Primitive property

            // Create item for property list
            var item = (Element)Activator.CreateInstance(_propertyInfo.PropertyType.GenericTypeArguments[0]);

            // Initialize with specified element
            element?.CopyTo(item);

            // Set key value
            _selectorPropertyInfo.SetValue(item, _selectorValue);

            // Add item to property list
            if (instance is IList items)
                items.Add(item);

            return instance;
        }

        /// <summary>
        /// Set the value of the property.
        /// </summary>
        /// <param name="instance">Instance to set the property value for.</param>
        /// <param name="value">The value to set.</param>
        public void SetValue(object instance, object value)
        {
            _propertyInfo.SetValue(instance, value);
        }

        /// <summary>
        /// Get the value of property (typed as Element).
        /// </summary>
        /// <param name="instance">Instance to get the property value from.</param>
        /// <returns>The property value as an Element type.</returns>
        public Element GetValueAsElement(object instance)
        {
            if (_selectorPropertyInfo == null)
                return (Element)_propertyInfo.GetValue(instance);

            if (_propertyInfo.GetValue(instance, null) is not IList items)
                return null;

            foreach (var item in items)
            {
                var value = _selectorPropertyInfo.GetValue(item)?.ToString();

                if (_selectorValue.Equals(value))
                    return (Element)item;
            }

            return null;
        }

        private void getPropertyInfo(string propertyName)
        {
            parsePropertyName(propertyName);

            _propertyInfo = typeof(ElementDefinition).GetProperty(_propertyName);
            _propertyInfo.Should().NotBeNull(); // Check property exists

            // ReSharper disable once PossibleNullReferenceException
            if (!isList(_propertyInfo.PropertyType))
            {
                _propertyInfo.PropertyType.IsAssignableTo(typeof(Element)).Should().BeTrue(); // Property type should be derived from Element
                return;
            }

            _propertyInfo.PropertyType.GenericTypeArguments[0].IsAssignableTo(typeof(Element)).Should().BeTrue(); // Property type should be derived from Element

            _selectorPropertyInfo = _propertyInfo.PropertyType.GenericTypeArguments[0].GetProperty(_selectorPropertyName);
            _selectorPropertyInfo.Should().NotBeNull(); // Check property exists
        }

        private void parsePropertyName(string propertyName)
        {
            _propertyName = propertyName;

            var regex = new Regex("(.*)\\[(.*):(.*)\\]|(.*)");
            var result = regex.Match(propertyName);

            if (!result.Success || result.Groups[4].Success)
                return;

            _propertyName = result.Groups[1].Value;
            _selectorPropertyName = result.Groups[2].Value;
            _selectorValue = result.Groups[3].Value;
        }

        private static bool isList(Type type) => typeof(IList).IsAssignableFrom(type);
    }
}