/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using Hl7.Fhir.Utility;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Specification;

namespace Hl7.Fhir.ElementModel
{
    // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    [DebuggerDisplay(@"\{{ShortPath,nq}}")]
    public class PocoNavigator : IElementNavigator, IAnnotated, IExceptionSource
    {
        private IList<ITypedElement> _siblings;
        private int _index;
        internal ITypedElement Current => _siblings[_index];

        [Obsolete("This class has been deprecated. Do not use this class directly, instead call " +
            "ToElementNavigator() (for backwards compatibility) or the new ToElementNode() on any resource or datatype")]
        public PocoNavigator(Base model, string rootName = null)
        {
            if (model == null) throw Error.ArgumentNull(nameof(model));

            _siblings = new List<ITypedElement>
            {
                new PocoElementNode(model, new PocoStructureDefinitionSummaryProvider(), rootName: rootName)
            };

            _index = 0;
            _parentCommonPath = "";
        }

        private PocoNavigator()     // for clone
        {
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        private string _parentCommonPath;

        /// <summary>
        /// Returns 
        /// </summary>
        public object Value => Current.Value;

        /// <summary>
        /// Returns 
        /// </summary>
        public Base FhirValue => ((PocoElementNode)Current).Current as Base;

        /// <summary>
        /// Return the FHIR TypeName
        /// </summary>
        public string Type => Current.InstanceType;

        /// <summary>
        /// The FHIR TypeName is also returned for the name of the root element
        /// </summary>
        public string Name => Current.Name;

        public string Location => Current.Location;

        public bool IsCollection => Current.Definition.IsCollection;

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
                        else if (fhirValue is Questionnaire.GroupComponent gc)
                        {
                            // Need to construct a where clause for this property
                            if (!string.IsNullOrEmpty(gc.LinkId))
                                return $"{_parentCommonPath}.{cur.Name}.where(linkId='{gc.LinkId}')";
                        }
                        else if (fhirValue is Questionnaire.QuestionComponent qc)
                        {
                            // Need to construct a where clause for this property
                            if (!string.IsNullOrEmpty(qc.LinkId))
                                return $"{_parentCommonPath}.{cur.Name}.where(linkId='{qc.LinkId}')";
                        }
                        else if (fhirValue is QuestionnaireResponse.GroupComponent rgc)
                        {
                            // Need to construct a where clause for this property
                            if (!string.IsNullOrEmpty(rgc.LinkId))
                                return $"{_parentCommonPath}.{cur.Name}.where(linkId='{rgc.LinkId}')";
                        }
                        else if (fhirValue is QuestionnaireResponse.QuestionComponent rqc)
                        {
                            // Need to construct a where clause for this property
                            if (!string.IsNullOrEmpty(rqc.LinkId))
                                return $"{_parentCommonPath}.{cur.Name}.where(linkId='{rqc.LinkId}')";
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



        private readonly object lockObject = new object();

        public bool MoveToFirstChild(string nameFilter = null)
        {
            var oldCP = CommonPath;

            var children = Current.Children().ToList();

            if (!children.Any()) return false;

            var found = nextMatch(children, nameFilter);
            if (found == -1) return false;

            _siblings = children;
            _index = found;
            _parentCommonPath = oldCP;
            return true;
        }

        private int nextMatch(IList<ITypedElement> nodes, string namefilter = null, int startAfter = -1)
        {
            for (int scan = startAfter + 1; scan < nodes.Count; scan++)
            {
                if (namefilter == null || nodes[scan].Name == namefilter)
                    return scan;
            }

            return -1;
        }

        /// <summary>
        /// Move onto the next element in the list
        /// </summary>
        /// <returns>
        /// true if there was a next element, false if it was the last element
        /// </returns>
        public bool MoveToNext(string nameFilter = null)
        {
            var found = nextMatch(_siblings, nameFilter, _index);

            if (found == -1) return false;

            _index = found;
            return true;
        }



        /// <summary>
        /// Clone this navigator
        /// </summary>
        /// <returns></returns>
        public IElementNavigator Clone() =>
            new PocoNavigator()
            {
                _siblings = _siblings,
                _index = _index,
                _parentCommonPath = _parentCommonPath,
                ExceptionHandler = ExceptionHandler
            };

        public IEnumerable<object> Annotations(Type type)
        {
                return Current.Annotations(type);
        }
    }


    public static class PocoNavigatorExtensions
    {
#pragma warning disable 612, 618
        public static IElementNavigator ToElementNavigator(this Base @base, string rootName = null) =>
            new PocoNavigator(@base, rootName);

        public static ITypedElement ToElementNode(this Base @base, string rootName = null) =>
            new PocoNavigator(@base, rootName).ToElementNode();

#pragma warning restore 612, 618
    }
}
