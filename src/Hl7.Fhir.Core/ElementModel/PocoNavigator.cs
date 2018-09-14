/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel.Adapters;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    [DebuggerDisplay(@"\{{ShortPath,nq}}")]
    public class PocoNavigator : BaseNodeToNavAdapter
    {
        [Obsolete("This class has been deprecated. Do not use this class directly, instead call " +
            "ToElementNavigator() (for backwards compatibility) or the new ToTypedNode() on any resource or datatype")]
        public PocoNavigator(Base model, string rootName = null)
        {
            if (model == null) throw Error.ArgumentNull(nameof(model));

            Initialize(
                new PocoElementNode(model, new PocoStructureDefinitionSummaryProvider(), rootName: rootName));

            _parentCommonPath = "";
        }

        private PocoNavigator()     // for clone
        {
        }

        private string _parentCommonPath;

        public bool IsCollection => Current.Definition.IsCollection;

        public Base FhirValue => ((PocoElementNode)Current).Current as Base;

        /// <summary>
        /// The ShortPath is almost the same as the Path except that
        /// where the item is not an array, the array signature is not included.
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public string ShortPath => ((PocoElementNode)Current).ShortPath;

        /// <summary>
        /// This is different to the explicit path as it considers what is
        /// usually used to reference the item, rather than explicitly
        /// e.g. Questionnaire Items are referenced through LinkId,
        ///      Telecoms are referenced through use/system
        ///      Codes are referenced through system
        ///      Identifiers are referenced through system
        /// others all revert back to the normal Path indexing
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public string CommonPath
        {
            get
            {
                var cur = Current;
                if (String.IsNullOrEmpty(_parentCommonPath))
                {
                    return cur.Name;
                }
                else
                {
                    // Needs to consider that the index might be irrelevant
                    if (IsCollection)
                    {
                        Base fhirValue = FhirValue;
                        if (fhirValue is Identifier ident)
                        {
                            // Need to construct a where clause for this property
                            if (!string.IsNullOrEmpty(ident.System))
                                return $"{_parentCommonPath}.{cur.Name}.where(system='{ident.System}')";
                        }
                        else if (fhirValue is ContactPoint cp)
                        {
                            // Need to construct a where clause for this property
                            if (cp.System.HasValue)
                                return $"{_parentCommonPath}.{cur.Name}.where(system='{cp.System.Value.GetLiteral()}')";
                        }
                        else if (fhirValue is Coding cd)
                        {
                            // Need to construct a where clause for this property
                            if (!string.IsNullOrEmpty(cd.System))
                                return $"{_parentCommonPath}.{cur.Name}.where(system='{cd.System}')";
                        }
                        else if (fhirValue is Address addr)
                        {
                            // Need to construct a where clause for this property
                            if (addr.Use.HasValue)
                                return $"{_parentCommonPath}.{cur.Name}.where(use='{addr.Use.Value.GetLiteral()}')";
                        }
                        else if (fhirValue is Questionnaire.ItemComponent ic)
                        {
                            // Need to construct a where clause for this property
                            if (!string.IsNullOrEmpty(ic.LinkId))
                                return $"{_parentCommonPath}.{cur.Name}.where(linkId='{ic.LinkId}')";
                        }
                        else if (fhirValue is QuestionnaireResponse.ItemComponent ric)
                        {
                            // Need to construct a where clause for this property
                            if (!string.IsNullOrEmpty(ric.LinkId))
                                return $"{_parentCommonPath}.{cur.Name}.where(linkId='{ric.LinkId}')";
                        }
                        else if (fhirValue is Extension ext)
                        {
                            // Need to construct a where clause for this property
                            // The extension is different as with fhirpath there
                            // is a shortcut format of .extension('url'), and since
                            // all extensions have a property name of extension, can just at the brackets and string name
                            return $"{_parentCommonPath}.{cur.Name}('{ext.Url}')";
                        }
                        return $"{_parentCommonPath}.{cur.Name}[{((PocoElementNode)cur).ArrayIndex}]";
                    }
                    return $"{_parentCommonPath}.{cur.Name}";
                }
            }
        }

        protected override IList<ITypedElement> GetChildren() =>
            Current.Children().ToList();

        public override bool MoveToFirstChild(string nameFilter = null)
        {
            var oldCP = CommonPath;

            if (!base.MoveToFirstChild(nameFilter)) return false;

            _parentCommonPath = oldCP;
            return true;
        }

        public override bool MoveToNext(string nameFilter = null) => base.MoveToNext(nameFilter);

        protected override BaseNodeToNavAdapter NewClone() =>
            new PocoNavigator()
            {
                _parentCommonPath = _parentCommonPath
            };
    }


    public static class PocoNavigatorExtensions
    {
#pragma warning disable 612, 618
        public static IElementNavigator ToElementNavigator(this Base @base, string rootName = null) =>
            new PocoNavigator(@base, rootName);

        public static ITypedElement ToTypedElement(this Base @base, string rootName = null) =>
            new PocoNavigator(@base, rootName).ToTypedElement();

#pragma warning restore 612, 618
    }
}