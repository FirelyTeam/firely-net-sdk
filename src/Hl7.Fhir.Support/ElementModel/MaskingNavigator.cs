/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Support.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    public class MaskingNavigatorSettings
    {
        public bool IncludeMandatory;
        public bool IncludeInSummary;

        public bool IncludeAll;
        public string[] IncludeElements;
        public string[] ExcludeElements;

        internal MaskingNavigatorSettings Clone() =>
            new MaskingNavigatorSettings
            {
                IncludeMandatory = this.IncludeMandatory,
                IncludeInSummary = this.IncludeInSummary,
                IncludeAll = this.IncludeAll,
                IncludeElements = this.IncludeElements.ToArray(),
                ExcludeElements = this.ExcludeElements.ToArray()
            };

    }

    public class MaskingNavigator : IElementNavigator, IAnnotated, IExceptionSource
    {
        public static MaskingNavigator ForSummary(IElementNavigator nav) =>
            new MaskingNavigator(nav, new MaskingNavigatorSettings { IncludeInSummary = true });

        public static MaskingNavigator ForText(IElementNavigator nav) =>
            new MaskingNavigator(nav, new MaskingNavigatorSettings
            { IncludeElements = new[] { "text", "id", "meta" }, IncludeMandatory = true });

        public static MaskingNavigator ForData(IElementNavigator nav) =>
            new MaskingNavigator(nav, new MaskingNavigatorSettings
            { IncludeAll = true, ExcludeElements = new[] { "text" } });

        public MaskingNavigator(IElementNavigator source, MaskingNavigatorSettings settings = null)
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));

            Source = source;
            _settings = settings ?? new MaskingNavigatorSettings();
        }

        private bool atTopLevel(string location) =>
            // check whether there is a single path separator -> path looks like 'Resource.xxxxx'
            location.IndexOf('.') == location.LastIndexOf('.');


        private MaskingNavigatorSettings _settings;

        public IElementNavigator Source { get; private set; }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name => Source.Name;

        public string Type => Source.Type;

        public object Value => Source.Value;

        public string Location => Source.Location;

        private MaskingNavigator() { }   // for Clone()

        public IElementNavigator Clone()
        {
            return new MaskingNavigator()
            {
                Source = this.Source.Clone(),
                _settings = this._settings
            };
        }

        public bool Included(IElementNavigator node)
        {
            var included = _settings.IncludeAll;

            var ed = node.GetElementDefinitionSummary();
            if(ed != null)
            {
                included |= _settings.IncludeMandatory && ed.IsRequired;
                included |= _settings.IncludeInSummary && ed.InSummary;
            }

            if (atTopLevel(node.Location))
            {
                var location = node.Location.Split('.')[1].TrimEnd('[');
                included |= _settings.IncludeElements?.Any(incl => location == incl) ?? false;

                if (_settings.ExcludeElements?.Any(excl => location == excl) == true)
                    included = false;
            }

            return included;
        }


        private static readonly PipelineComponent _componentLabel = PipelineComponent.Create<MaskingNavigator>();

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(PipelineComponent))
                return (new[] { _componentLabel }).Union(Source.Annotations(typeof(PipelineComponent)));
            else
                return Source.Annotations(type);
        }

        private bool findNext(IElementNavigator scan, string nameFilter)
        {
            if (nameFilter != null)
                return !Included(scan);
            else
            {
                do
                {
                    if (Included(scan)) return true;
                }
                while (scan.MoveToNext());
                return false;
            }
        }

        public bool MoveToNext(string nameFilter = null)
        {
            var scan = Source.Clone();
            var success = scan.MoveToNext(nameFilter);

            // Return immediately if there's no match at all
            if (!success) return false;

            success = findNext(scan, nameFilter);

            if(success)
                Source = scan;

            return success;
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            var scan = Source.Clone();
            var success = scan.MoveToFirstChild(nameFilter);

            // Return immediately if there's no match at all
            if (!success) return false;

            success = findNext(scan, nameFilter);

            // When unsuccessful, restore to where we were before
            if (success)
                Source = scan;

            return success;
        }
    }
}
