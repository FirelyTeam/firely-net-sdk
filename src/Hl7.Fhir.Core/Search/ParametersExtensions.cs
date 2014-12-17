/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Search
{
    public static class ParametersExtensions
    {
        public static Parameters Include(this Parameters qry, string path)
        {
            qry.Includes.Add(path);

            return qry;
        }

		public static Parameters For<TResource>(this Parameters qry)
		{
            qry.ResourceSearchType = ModelInfo.GetResourceNameForType(typeof(TResource));
			return qry;
		}
		
		public static Parameters For(this Parameters qry, string resourceName)
        {
            qry.ResourceSearchType = resourceName;
            return qry;
        }

        public static Parameters Where(this Parameters qry, string criterium)
        {
            var keyVal = criterium.SplitLeft('=');
            qry.AddParameter(keyVal.Item1, keyVal.Item2);

            return qry;
        }

        public static Parameters Custom(this Parameters qry, string customQueryName)
        {
			if (customQueryName == null) throw Error.ArgumentNull("customQueryName");

			qry.QueryName = customQueryName;
            return qry;
        }

        public static Parameters OrderBy(this Parameters qry, string paramName, SortOrder order = SortOrder.Ascending)
        {
            if (paramName == null) throw Error.ArgumentNull("paramName");

            var sort = qry.Sort ?? new List<Tuple<string, SortOrder>>();
            sort.Add(Tuple.Create(paramName, order));
            qry.Sort = sort;
            return qry;
        }

        public static Parameters LimitTo(this Parameters qry, int count)
        {
            qry.Count = count;
            return qry;
        }

        public static Parameters SummaryOnly(this Parameters qry, bool summaryOnly = true)
        {
            qry.Summary = summaryOnly;
            return qry;
        }

    }
}
