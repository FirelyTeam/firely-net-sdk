/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Diagnostics;

namespace Hl7.Fhir.FhirPath
{
    // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    [DebuggerDisplay(@"\{{ShortPath,nq}}")]
    public class PocoNavigator : IElementNavigator
    {
        public PocoNavigator(Base model)
        {
            if (model == null) throw Error.ArgumentNull(nameof(model));

            //_current = new PocoElementNavigator(model.TypeName, model);
            _parentPath = "";
            _parentShortPath = "";
            _parentCommonPath = "";

            var me = new PocoElementNavigator(model.TypeName, model);
            _siblings = new List<PocoElementNavigator> { me };
            _index = 0;
        }

        private PocoNavigator()
        {
        }

        private IList<PocoElementNavigator> _siblings;
        private int _index;
        private string _parentPath;
        private string _parentShortPath;
        private string _parentCommonPath;

        private PocoElementNavigator Current
        {
            get
            {
                return _siblings[_index];
            }
        }

        /// <summary>
        /// Returns 
        /// </summary>
        public object Value
        {
            get
            {
#if DEBUGX
                Console.WriteLine("    -> Read Value of {0}: {1}", _current.Name, _current.Value);
#endif
                return Current.Value;
            }
        }

        /// <summary>
        /// Returns 
        /// </summary>
        public Base FhirValue
        {
            get
            {
#if DEBUGX
                Console.WriteLine("    -> Read Value of {0}: {1}", _current.Name, _current.Value);
#endif
                return Current.FhirValue;
            }
        }

        /// <summary>
        /// Return the FHIR TypeName
        /// </summary>
        public string Type
        {
            get
            {
                return Current.TypeName; // This needs to be fixed
            }
        }

        /// <summary>
        /// The FHIR TypeName is also returned for the name of the root element
        /// </summary>
        public string Name
        {
            get
            {
#if DEBUGX
                Console.WriteLine("Read Name: {0} (value = {1})", _current.Name, _current.Value);
#endif
                return Current.Name;
            }
        }

        public string Location
        {
            get
            {
                var cur = Current;
                if (String.IsNullOrEmpty(_parentPath))
                {
                    return cur.Name;
                }
                else
                {
                    return $"{_parentPath}.{cur.Name}[{cur._arrayIndex}]";
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
                var cur = Current;
                if (String.IsNullOrEmpty(_parentShortPath))
                {
                    return cur.Name;
                }
                else
                {
                    // Needs to consider that the index might be irrelevant
                    if (cur.PropMap.IsCollection)
                    {
                        return $"{_parentShortPath}.{cur.Name}[{cur._arrayIndex}]";
                    }
                    return $"{_parentShortPath}.{cur.Name}";
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
                var cur = Current;
                if (String.IsNullOrEmpty(_parentCommonPath))
                {
                    return cur.Name;
                }
                else
                {
                    // Needs to consider that the index might be irrelevant
                    if (cur.PropMap.IsCollection)
                    {
                        Base fhirValue = cur.FhirValue;
                        if (fhirValue is Identifier)
                        {
                            // Need to construct a where clause for this property
                            Identifier ident = fhirValue as Identifier;
                            if (!string.IsNullOrEmpty(ident.System))
                                return $"{_parentCommonPath}.{cur.Name}.where(system='{ident.System}')";
                        }
                        else if (fhirValue is ContactPoint)
                        {
                            // Need to construct a where clause for this property
                            var cp = fhirValue as ContactPoint;
                            if (cp.System.HasValue)
                                return $"{_parentCommonPath}.{cur.Name}.where(system='{cp.System.Value.GetLiteral()}')";
                        }
                        else if (fhirValue is Coding)
                        {
                            // Need to construct a where clause for this property
                            var item = fhirValue as Coding;
                            if (!string.IsNullOrEmpty(item.System))
                                return $"{_parentCommonPath}.{cur.Name}.where(system='{item.System}')";
                        }
                        else if (fhirValue is Address)
                        {
                            // Need to construct a where clause for this property
                            var addr = fhirValue as Address;
                            if (addr.Use.HasValue)
                                return $"{_parentCommonPath}.{cur.Name}.where(use='{addr.Use.Value.GetLiteral()}')";
                        }
                        else if (fhirValue is Questionnaire.GroupComponent)
                        {
                            // Need to construct a where clause for this property
                            var item = fhirValue as Questionnaire.GroupComponent;
                            if (!string.IsNullOrEmpty(item.LinkId))
                                return $"{_parentCommonPath}.{cur.Name}.where(linkId='{item.LinkId}')";
                        }
                        else if (fhirValue is Questionnaire.QuestionComponent)
                        {
                            // Need to construct a where clause for this property
                            var item = fhirValue as Questionnaire.QuestionComponent;
                            if (!string.IsNullOrEmpty(item.LinkId))
                                return $"{_parentCommonPath}.{cur.Name}.where(linkId='{item.LinkId}')";
                        }
                        else if (fhirValue is QuestionnaireResponse.GroupComponent)
                        {
                            // Need to construct a where clause for this property
                            var item = fhirValue as QuestionnaireResponse.GroupComponent;
                            if (!string.IsNullOrEmpty(item.LinkId))
                                return $"{_parentCommonPath}.{cur.Name}.where(linkId='{item.LinkId}')";
                        }
                        else if (fhirValue is QuestionnaireResponse.QuestionComponent)
                        {
                            // Need to construct a where clause for this property
                            var item = fhirValue as QuestionnaireResponse.QuestionComponent;
                            if (!string.IsNullOrEmpty(item.LinkId))
                                return $"{_parentCommonPath}.{cur.Name}.where(linkId='{item.LinkId}')";
                        }
                        else if (fhirValue is Extension)
                        {
                            // Need to construct a where clause for this property
                            // The extension is different as with fhirpath there
                            // is a shortcut format of .extension('url'), and since
                            // all extensions have a property name of extension, can just at the brackets and string name
                            var item = fhirValue as Extension;
                            return $"{_parentCommonPath}.{cur.Name}('{item.Url}')";
                        }
                        return $"{_parentCommonPath}.{cur.Name}[{cur._arrayIndex}]";
                    }
                    return $"{_parentCommonPath}.{cur.Name}";
                }
            }
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            var children = Current.Children(nameFilter);
            if (children.Any())
            {
                lock(this)
                {
                    _parentPath = Location;
                    _parentShortPath = ShortPath;
                    _parentCommonPath = CommonPath;

                    _siblings = (List<PocoElementNavigator>)children;
                    _index = 0;
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
        public bool MoveToNext()
        {
            if (_siblings.Count > _index+1)
            { 
                // string oldName = Current.Name;
                _index++;
                // string newName = Current.Name;
                // Console.WriteLine("Move Next: {0} -> {1}", oldName, newName);
                return true;
            }
            // Console.WriteLine("Move Next: {0} (no more sibblings, didn't move)", this.GetName());
            return false;
        }

        /// <summary>
        /// Clone this navigator
        /// </summary>
        /// <returns></returns>
        public IElementNavigator Clone()
        {
            var result = new PocoNavigator();
            lock (this)
            {
                result._siblings = this._siblings;
                result._index = this._index;
                result._parentPath = this._parentPath;
                result._parentShortPath = this._parentShortPath;
                result._parentCommonPath = this._parentCommonPath;
            }
            return result;
        }
    }
}
