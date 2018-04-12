/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
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

namespace Hl7.Fhir.ElementModel
{
    // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    [DebuggerDisplay(@"\{{ShortPath,nq}}")]
    public class PocoNavigator : IElementNavigator
    {
        public PocoNavigator(Base model)
        {
            if (model == null) throw Error.ArgumentNull(nameof(model));

            //_current = new PocoElementNavigator(model.TypeName, model);
            _parentLocation = "";
            _parentShortPath = "";
            _parentCommonPath = "";

            _nav = new PocoElementNavigator(model);
        }

        private PocoNavigator()     // for clone
        {
        }

        private PocoElementNavigator _nav;
        private string _parentLocation;
        private string _parentShortPath;
        private string _parentCommonPath;

        private PocoElementNavigator Current => _nav;

        /// <summary>
        /// Returns 
        /// </summary>
        public object Value => Current.Value;

        /// <summary>
        /// Returns 
        /// </summary>
        public Base FhirValue => Current.FhirValue;

        /// <summary>
        /// Return the FHIR TypeName
        /// </summary>
        public string Type => FhirValue is BackboneElement ? "BackboneElement" : Current.TypeName;

        /// <summary>
        /// The FHIR TypeName is also returned for the name of the root element
        /// </summary>
        public string Name => Current.Name;

        public string Location
        {
            get
            {
                var cur = Current;
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
                var cur = Current;
                if (String.IsNullOrEmpty(_parentShortPath))
                {
                    return cur.Name;
                }
                else
                {
                    // Needs to consider that the index might be irrelevant
                    if (cur.AtCollection)
                    {
                        return $"{_parentShortPath}.{cur.Name}[{cur.ArrayIndex}]";
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
                    if (cur.AtCollection)
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
                        return $"{_parentCommonPath}.{cur.Name}[{cur.ArrayIndex}]";
                    }
                    return $"{_parentCommonPath}.{cur.Name}";
                }
            }
        }

        private object lockObject = new object();

        public bool MoveToFirstChild(string nameFilter = null)
        {
            var oldLoc = Location;
            var oldSP = ShortPath;
            var oldCP = CommonPath;
                
            if (Current.MoveToFirstChild(nameFilter))
            {
                lock(lockObject)
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
            return Current.MoveToNext(nameFilter);
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
    }
}
