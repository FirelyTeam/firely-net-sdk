/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Model.Primitives;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.FhirPath.Tests
{
    public static class PocoNavigatorExtensions
    {
        internal static bool _fhirSymbolTableExtensionsAdded = false;
        public static void PrepareFhirSybolTableFunctions()
        {
            if (!_fhirSymbolTableExtensionsAdded)
            {
                _fhirSymbolTableExtensionsAdded = true;
                Hl7.FhirPath.FhirPathCompiler.DefaultSymbolTable.AddFhirExtensions();
            }
        }

        // TODO: Add support for the custom fhirpath function hasValue() on the default symbol table
        //
        public static SymbolTable AddFhirExtensions(this SymbolTable t)
        {
            t.Add("hasValue", (IElementNavigator f) => f.HasValue(), doNullProp: false);
            t.Add("resolve", (IElementNavigator f) => f.Resolve(), doNullProp: false);

            return t;
        }

        /// <summary>
        /// Check if the node has a value, and not just extensions.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool HasValue(this IElementNavigator focus)
        {
            if (focus == null)
                return false;
            if (focus.Value == null)
                return false;
            return true;
        }

        /// <summary>
        /// Where this item is a reference, resolve it to an actual resource, and return that
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static IElementNavigator Resolve(this IElementNavigator focus)
        {
            if (focus == null)
                return null;
            if (focus.Value == null)
                return null;
            if (focus.Value is PocoElementNavigator)
            {
                var fv = (focus.Value as PocoElementNavigator).FhirValue;
                if (fv is ResourceReference)
                {
                    string reference = (fv as ResourceReference).Reference;
                    if (string.IsNullOrEmpty(reference))
                        return null;
                    Fhir.Rest.ResourceIdentity ri = new Fhir.Rest.ResourceIdentity(reference);

                    // Go retrieve the resource? (seriously?)
                    System.Diagnostics.Debug.WriteLine("Evaluating a reolve call: " + reference);
                }
            }
            return null;
        }

        public static IEnumerable<Base> ToFhirValues(this IEnumerable<IElementNavigator> results)
        {
            return results.Select(r =>
            {
                if (r == null)
                    return null;

                if (r is Hl7.Fhir.FhirPath.PocoNavigator && (r as Hl7.Fhir.FhirPath.PocoNavigator).FhirValue != null)
                {
                    return ((PocoNavigator)r).FhirValue;
                }
                object result;
                if (r.Value is Hl7.FhirPath.ConstantValue)
                {
                    result = (r.Value as Hl7.FhirPath.ConstantValue).Value;
                }
                else
                {
                    result = r.Value;
                }

                if (result is bool)
                {
                    return new FhirBoolean((bool)result);
                }
                if (result is long)
                {
                    return new Integer((int)(long)result);
                }
                if (result is decimal)
                {
                    return new FhirDecimal((decimal)result);
                }
                if (result is string)
                {
                    return new FhirString((string)result);
                }
                if (result is PartialDateTime)
                {
                    var dt = (PartialDateTime)result;
                    return new FhirDateTime(dt.ToUniversalTime());
                }
                else
                {
                    // This will throw an exception if the type isn't one of the FHIR types!
                    return (Base)result;
                }
            });
        }

        public static IEnumerable<Base> Select(this Base input, string expression, Resource resource = null)
        {
            var inputNav = new PocoNavigator(input);
            var resourceNav = resource != null ? new PocoNavigator(resource) : null;

            var result = inputNav.Select(expression, resourceNav);
            return result.ToFhirValues();            
        }

        public static object Scalar(this Base input, string expression, Resource resource = null)
        {
            var inputNav = new PocoNavigator(input);
            var resourceNav = resource != null ? new PocoNavigator(resource) : null;

            return inputNav.Scalar(expression, resourceNav);
        }

        public static bool Predicate(this Base input, string expression, Resource resource = null)
        {
            var inputNav = new PocoNavigator(input);
            var resourceNav = resource != null ? new PocoNavigator(resource) : null;

            return inputNav.Predicate(expression, resourceNav);
        }

        public static bool IsBoolean(this Base input, string expression, bool value, Resource resource = null)
        {
            var inputNav = new PocoNavigator(input);
            var resourceNav = resource != null ? new PocoNavigator(resource) : null;

            return inputNav.IsBoolean(expression, value, resourceNav);
        }

    }
}
