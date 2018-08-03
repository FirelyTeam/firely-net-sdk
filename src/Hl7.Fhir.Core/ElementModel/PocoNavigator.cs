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
    public class PocoNavigator : IElementNavigator, IElementDefinitionSummary, IAnnotated
    {
        [Obsolete("Do not use the constructor directly, instead call ToElementNavigator() on any resource or datatype")]
        public PocoNavigator(Base model, string rootName = null)
        {
            if (model == null) throw Error.ArgumentNull(nameof(model));

            //_current = new PocoElementNavigator(model.TypeName, model);
            _parentLocation = "";
            _parentShortPath = "";
            _parentCommonPath = "";

            _nav = new PocoElementNavigator(model, rootName ?? model.TypeName);
        }

        private PocoNavigator()     // for clone
        {
        }

        private PocoElementNavigator _nav;
        private string _parentLocation;
        private string _parentShortPath;
        private string _parentCommonPath;

        /// <summary>
        /// Returns 
        /// </summary>
        public object Value => _nav.Value;

        /// <summary>
        /// Returns 
        /// </summary>
        public Base FhirValue => _nav.FhirValue;

        /// <summary>
        /// Return the FHIR TypeName
        /// </summary>
        public string Type => _nav.TypeName;

        /// <summary>
        /// The FHIR TypeName is also returned for the name of the root element
        /// </summary>
        public string Name => _nav.Name;

        public int Order => _nav.DefinitionSummary.Order;

        public string Location
        {
            get
            {
                var cur = _nav;
                if (String.IsNullOrEmpty(_parentLocation))
                {
                    return cur.Name;
                }
                else
                {
                    return $"{_parentLocation}.{cur.Name}[{cur.ArrayIndex}]";
                }
            }
        }

        /// <summary>
        /// The ShortPath is almost the same as the Path except that
        /// where the item is not an array, the array signature is not included.
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public string ShortPath
        {
            get
            {
                if (String.IsNullOrEmpty(_parentShortPath))
                {
                    return Name;
                }
                else
                {
                    // Needs to consider that the index might be irrelevant
                    if (IsCollection)
                    {
                        return $"{_parentShortPath}.{Name}[{_nav.ArrayIndex}]";
                    }
                    return $"{_parentShortPath}.{Name}";
                }
            }
        }


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
                var cur = _nav;
                if (String.IsNullOrEmpty(_parentCommonPath))
                {
                    return cur.Name;
                }
                else
                {
                    // Needs to consider that the index might be irrelevant
                    if (IsCollection)
                    {
                        Base fhirValue = cur.FhirValue;
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
                        return $"{_parentCommonPath}.{cur.Name}[{cur.ArrayIndex}]";
                    }
                    return $"{_parentCommonPath}.{cur.Name}";
                }
            }
        }

        public string ElementName => Name;

        public bool IsCollection => _nav.DefinitionSummary.IsCollection;

        public bool IsChoiceElement => _nav.DefinitionSummary.IsChoiceElement;

        public bool IsRequired => _nav.DefinitionSummary.IsRequired;

        public bool IsResource => _nav.DefinitionSummary.IsResource;

        public XmlRepresentation Representation => _nav.DefinitionSummary.Representation;

        public bool InSummary => _nav.DefinitionSummary.InSummary;

        ITypeSerializationInfo[] IElementDefinitionSummary.Type => null;

        public string NonDefaultNamespace => null;

        private readonly object lockObject = new object();

        public bool MoveToFirstChild(string nameFilter = null)
        {
            var oldLoc = Location;
            var oldSP = ShortPath;
            var oldCP = CommonPath;

            if (_nav.MoveToFirstChild(nameFilter))
            {
                lock (lockObject)
                {
                    _parentLocation = oldLoc;
                    _parentShortPath = oldSP;
                    _parentCommonPath = oldCP;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Move onto the next element in the list
        /// </summary>
        /// <returns>
        /// true if there was a next element, false if it was the last element
        /// </returns>
        public bool MoveToNext(string nameFilter = null)
        {
            return _nav.MoveToNext(nameFilter);
        }

        /// <summary>
        /// Clone this navigator
        /// </summary>
        /// <returns></returns>
        public IElementNavigator Clone()
        {
            var result = new PocoNavigator();

            lock (lockObject)
            {
                result._nav = this._nav.Clone();
                result._parentLocation = this._parentLocation;
                result._parentShortPath = this._parentShortPath;
                result._parentCommonPath = this._parentCommonPath;
            }

            return result;
        }

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(ElementDefinitionSummary))
            {
                return new[]
                {
                    new ElementDefinitionSummary(this)
                };
            }
            else if (type == typeof(PrettyPath))
                return new[] { new PrettyPath { Path = ShortPath } };
            else
                return _nav.Annotations(type);
        }
    }


    public static class PocoNavigatorExtensions
    {
#pragma warning disable 612, 618
        public static IElementNavigator ToElementNavigator(this Base @base, string rootName=null) => new PocoNavigator(@base, rootName);

#pragma warning restore 612, 618
    }
}
