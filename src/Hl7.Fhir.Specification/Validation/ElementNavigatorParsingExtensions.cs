using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// A class to do basic parsing of POCO classes from an IElementNavigator.  Can be replaced by the real
    /// IElementNavigator-based PocoParser when we have that piece of infrastructure ready.
    /// </summary>
    internal static class ElementNavigatorParsingExtensions
    {
        public static Model.Quantity ParseQuantity(this IElementNavigator instance)
        {
            var newQuantity = new Quantity();

            newQuantity.Value = instance.Children("value").SingleOrDefault()?.Value as decimal?;

            var comp = instance.Children("comparator").GetString();
            if(comp != null)
                newQuantity.ComparatorElement = new Code<Quantity.QuantityComparator> { ObjectValue = comp };

            newQuantity.Unit = instance.Children("unit").GetString();
            newQuantity.System = instance.Children("system").GetString();
            newQuantity.Code = instance.Children("code").GetString();

            return newQuantity;
        }

        /// <summary>
        /// Parses a bindeable type into either a Coding (code, Coding, Quantity, string, uri) or CodeableConcept
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="codedType"></param>
        /// <returns>An object, which is either a Coding or CodeableConcept</returns>
        public static object ParseBindable(this IElementNavigator instance, FHIRAllTypes codedType)
        {
            // 'code','Coding','CodeableConcept','Quantity','Extension', 'string', 'uri'

            if (codedType == FHIRAllTypes.Code)
                return instance.ParsePrimitiveCode();
            else if (codedType == FHIRAllTypes.Coding)
                return instance.ParseCoding();
            else if (codedType == FHIRAllTypes.CodeableConcept)
                return instance.ParseCodeableConcept();
            else if (codedType == FHIRAllTypes.Quantity)
            {
                var newCoding = new Coding();
                var q = instance.ParseQuantity();
                newCoding.Code = q.Unit;
                newCoding.System = q.System;
                return newCoding;
            }
            else if(codedType == FHIRAllTypes.String)
                return instance.ParsePrimitiveCode();
            else if (codedType == FHIRAllTypes.Uri)
                return instance.ParsePrimitiveCode();
            else if (codedType == FHIRAllTypes.Extension)
                throw new NotSupportedException($"The validator does not support binding to Extension values");
            else
                throw new NotSupportedException($"FHIR type '{codedType}' is not bindeable");
        }

        public static Coding ParsePrimitiveCode(this IElementNavigator instance)
        {
            var newCoding = new Coding();
            newCoding.Code = instance.Value?.ToString();

            return newCoding;
        }

        public static Coding ParseCoding(this IElementNavigator instance)
        {
            var newCoding = new Coding();
            newCoding.System = instance.Children("system").GetString();
            newCoding.Version = instance.Children("version").GetString();
            newCoding.Code = instance.Children("code").GetString();
            newCoding.Display = instance.Children("display").GetString();
            newCoding.UserSelected = instance.Children("userSelected").SingleOrDefault()?.Value as bool?;

            return newCoding;
        }

        public static CodeableConcept ParseCodeableConcept(this IElementNavigator instance)
        {
            var newCodeableConcept = new CodeableConcept();

            newCodeableConcept.Coding = 
                    instance.Children("coding").Select(codingNav => codingNav.ParseCoding()).ToList();
            newCodeableConcept.Text = instance.Children("text").GetString();

            return newCodeableConcept;
        }

        public static string GetString(this IEnumerable<IElementNavigator> instance)
        {
            return instance.SingleOrDefault()?.Value as string;
        }
    }
}
