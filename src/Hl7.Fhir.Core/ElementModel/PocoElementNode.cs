/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;

namespace Hl7.Fhir.ElementModel
{
    internal class PocoElementNode : IElementNode, IAnnotated, IExceptionSource, IElementDefinitionSummary
    {
        private readonly object _me;
        public readonly PocoStructureDefinitionSummaryProvider Provider;
        public readonly IElementDefinitionSummary DefinitionSummary;

        public readonly string ShortPath;
        public readonly string CommonPath;

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        internal PocoElementNode(Base parent, PocoStructureDefinitionSummaryProvider provider)
        {
            _me = parent;
            Type = parent.TypeName;
            var typeInfo = provider.Provide(parent.GetType());
            DefinitionSummary = Specification.ElementDefinitionSummary.ForRoot(parent.TypeName, typeInfo);
            Location = Type;
            ShortPath = Type;
            CommonPath = Type;
        }

        private PocoElementNode(object instance, PocoElementNode parent, string location, string shortPath, string commonPath,
            IElementDefinitionSummary summary)
        {
            _me = instance;
            Type = determineInstanceType(instance, summary);
            Provider = parent.Provider;
            ExceptionHandler = parent.ExceptionHandler;
            DefinitionSummary = summary;
            Location = location;
            ShortPath = shortPath;
            CommonPath = commonPath;
        }

        public int Order => DefinitionSummary.Order;

        public string Name => DefinitionSummary.ElementName;

        public string ElementName => DefinitionSummary.ElementName;

        public bool IsCollection => DefinitionSummary.IsCollection;

        public bool IsChoiceElement => DefinitionSummary.IsChoiceElement;

        public bool IsRequired => DefinitionSummary.IsRequired;

        public bool IsResource => DefinitionSummary.IsResource;

        public XmlRepresentation Representation => DefinitionSummary.Representation;

        public bool InSummary => DefinitionSummary.InSummary;

        ITypeSerializationInfo[] IElementDefinitionSummary.Type => DefinitionSummary.Type;

        public string NonDefaultNamespace => DefinitionSummary.NonDefaultNamespace;


        public IEnumerable<IElementNode> Children(string name)
        {
            if (!(_me is Base parentBase)) yield break;

            var children = parentBase.NamedChildren;

            string oldElementName = null;
            int arrayIndex = 0;
            var childElementDefinitions = Provider.Provide(this.Type).GetElements();

            foreach (var child in children)
            {
                if (name == null || child.ElementName == name)
                {
                    var mySummary = childElementDefinitions.Single(c => c.ElementName == name);

                    if (!mySummary.IsCollection || oldElementName != child.ElementName)
                        arrayIndex = 0;
                    else
                        arrayIndex += 1;

                    var location = Location == null ? child.ElementName :
                                $"{Location}.{child.ElementName}[{arrayIndex}]";
                    var shortPath = ShortPath == null ? child.ElementName :
                        (mySummary.IsCollection ?
                            $"{ShortPath}.{child.ElementName}[{arrayIndex}]" :
                            $"{ShortPath}.{child.ElementName}");
                    var commonPath = buildCommonPath(CommonPath, child.ElementName, mySummary.IsCollection, (Base)child.Value, arrayIndex);

                    yield return new PocoElementNode(child.Value, this, location, shortPath, commonPath, mySummary);
                }

                oldElementName = child.ElementName;
            }
        }

        /// <summary>
        /// This is only needed for search data extraction (and debugging)
        /// to be able to read the values from the selected node (if a coding, so can get the value and system)
        /// </summary>
        /// <remarks>Will return null if on id, value, url (primitive attribute props in xml)</remarks>
        public Base FhirValue => _me as Base;

        public object Value
        {
            get
            {
                if (_me is string)
                    return _me;

                try
                {
                    switch (_me)
                    {
                        case Hl7.Fhir.Model.Instant ins:
                            return ins.ToPartialDateTime();
                        case Hl7.Fhir.Model.Time time:
                            return time.ToTime();
                        case Hl7.Fhir.Model.Date dt:
                            return dt.ToPartialDateTime();
                        case FhirDateTime fdt:
                            return fdt.ToPartialDateTime();
                        case Hl7.Fhir.Model.Integer fint:
                            return (long)fint.Value;
                        case Hl7.Fhir.Model.PositiveInt pint:
                            return (long)pint.Value;
                        case Hl7.Fhir.Model.UnsignedInt unsint:
                            return (long)unsint.Value;
                        case Hl7.Fhir.Model.Base64Binary b64:
                            return b64.Value != null ? PrimitiveTypeConverter.ConvertTo<string>(b64.Value) : null;
                        case Primitive prim:
                            return prim.ObjectValue;
                        default:
                            return null;
                    }
                }
                catch (FormatException)
                {
                    // If it fails, just return the unparsed contents
                    return (_me as Primitive)?.ObjectValue;
                }
            }
        }


        public string Type { get; private set; }

        public static string determineInstanceType(object instance, IElementDefinitionSummary summary)
        {
            var typeName = !summary.IsChoiceElement && !summary.IsResource ?
                        summary.Type.Single().GetTypeName() : ((Base)instance).TypeName;

            if (ModelInfo.IsProfiledQuantity(typeName))
                return "Quantity";
            else
                return typeName;
        }

        public string Location { get; private set; }

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(ElementDefinitionSummary))
                return new[] {  new ElementDefinitionSummary(this)  };
            else if(type == typeof(PocoElementNode))
                return new[] { this };
            else if (type == typeof(PrettyPath))
                return new[] { new PrettyPath { Path = ShortPath } };
            else if (FhirValue is IAnnotated ia)
                return ia.Annotations(type);
            else
                return Enumerable.Empty<object>();
        }

        public static string buildCommonPath(string parentCommonPath, string name, bool isCollection, Base fhirValue, int arrayIndex)
        {
            if (String.IsNullOrEmpty(parentCommonPath))
                return name;
            else
            {
                // Needs to consider that the index might be irrelevant
                if (isCollection)
                {
                    if (fhirValue is Identifier ident)
                    {
                        // Need to construct a where clause for this property
                        if (!string.IsNullOrEmpty(ident.System))
                            return $"{parentCommonPath}.{name}.where(system='{ident.System}')";
                    }
                    else if (fhirValue is ContactPoint cp)
                    {
                        // Need to construct a where clause for this property
                        if (cp.System.HasValue)
                            return $"{parentCommonPath}.{name}.where(system='{cp.System.Value.GetLiteral()}')";
                    }
                    else if (fhirValue is Coding cd)
                    {
                        // Need to construct a where clause for this property
                        if (!string.IsNullOrEmpty(cd.System))
                            return $"{parentCommonPath}.{name}.where(system='{cd.System}')";
                    }
                    else if (fhirValue is Address addr)
                    {
                        // Need to construct a where clause for this property
                        if (addr.Use.HasValue)
                            return $"{parentCommonPath}.{name}.where(use='{addr.Use.Value.GetLiteral()}')";
                    }
                    else if (fhirValue is Questionnaire.GroupComponent gc)
                    {
                        // Need to construct a where clause for this property
                        if (!string.IsNullOrEmpty(gc.LinkId))
                            return $"{parentCommonPath}.{name}.where(linkId='{gc.LinkId}')";
                    }
                    else if (fhirValue is Questionnaire.QuestionComponent qc)
                    {
                        // Need to construct a where clause for this property
                        if (!string.IsNullOrEmpty(qc.LinkId))
                            return $"{parentCommonPath}.{name}.where(linkId='{qc.LinkId}')";
                    }
                    else if (fhirValue is QuestionnaireResponse.GroupComponent rgc)
                    {
                        // Need to construct a where clause for this property
                        if (!string.IsNullOrEmpty(rgc.LinkId))
                            return $"{parentCommonPath}.{name}.where(linkId='{rgc.LinkId}')";
                    }
                    else if (fhirValue is QuestionnaireResponse.QuestionComponent rqc)
                    {
                        // Need to construct a where clause for this property
                        if (!string.IsNullOrEmpty(rqc.LinkId))
                            return $"{parentCommonPath}.{name}.where(linkId='{rqc.LinkId}')";
                    }
                    else if (fhirValue is Extension ext)
                    {
                        // Need to construct a where clause for this property
                        // The extension is different as with fhirpath there
                        // is a shortcut format of .extension('url'), and since
                        // all extensions have a property name of extension, can just at the brackets and string name
                        return $"{parentCommonPath}.{name}('{ext.Url}')";
                    }
                    return $"{parentCommonPath}.{name}[{arrayIndex}]";
                }
                return $"{parentCommonPath}.{name}";
            }
        }

    }
}