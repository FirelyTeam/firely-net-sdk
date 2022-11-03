/* 
 * Copyright (c) 2022, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable


using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Introspection
{

    /// <summary>
    /// Extensions on ModelInspector. This code should be moved to the ModelInspector class itself when Bundle moved
    /// to Support.Poco.
    /// </summary>
    public static class ModelInspectorExtensions
    {

        private static readonly Assembly[] _commonAssemblies = new[] { typeof(IModelInfo).Assembly, typeof(Bundle).Assembly };

        /// <summary>
        /// Returns a fully configured <see cref="ModelInspector"/> with the
        /// FHIR metadata contents of the given FHIR instance. Calling this function repeatedly for
        /// the same assembly will return the same inspector.
        /// </summary>
        /// <param name="base">The FHIR instance</param>
        /// <returns>The fully configured <see cref="ModelInspector"/> </returns>
        /// <exception cref="System.Exception"></exception>
        public static ModelInspector ForInstance(Base @base)
        {
            var callingAssembly = @base.GetType().Assembly;
            var inspector = getInspectorForCommonInstance(@base, callingAssembly);
            return inspector ?? ModelInspector.ForAssembly(callingAssembly);
        }

        private static ModelInspector? getInspectorForCommonInstance(Base @base, Assembly callingAssembly)
        {
            if (_commonAssemblies.Contains(callingAssembly))
            {
                var assemblies = referredResources(@base)
                    .Select(r => r.GetType().Assembly)
                    .Distinct()
                    .Except(new[] { typeof(IModelInfo).Assembly, callingAssembly }); // remove the common assembly, because that's always loaded


                if (assemblies.Count() > 1)
                {
                    // 2022-11-03 MV: we know this goes wrong when the end user has custom resources in a separate assembly. A bundle
                    // with a mix of standard resources and custom resource will result in more than 1 assembly. We can't fix this yet.

                    throw new InvalidOperationException($"Found an instance of type {@base.GetType()} from the common assembly " +
                        $"that contains resources from more than one satellite assembly: " +
                        string.Join(",", assemblies.Select(a => a.FullName)));
                }

                var assembly = assemblies.SingleOrDefault();
                if (assembly is not null) return ModelInspector.ForAssembly(assembly);
            }

            return null;
        }

        private static IEnumerable<Base> referredResources(Base @base)
        {
            List<Base> resources = (@base switch
            {
                DomainResource domainResource => domainResource.Contained,
                Bundle bundle => bundle.Entry.Select(e => e.Resource).Cast<Base?>()
                                .Concat(bundle.Entry.Select(e => e.Response?.Outcome)),
                Parameters parameters => parameters.Parameter.Select(p => p.Value),

                _ => Enumerable.Empty<Base>()
            }).Where(t => t is not null).Cast<Base>().ToList();

            return resources.Any() == false
                ? resources
                : addRange(resources, resources.SelectMany(r => referredResources(r)));

            static List<T> addRange<T>(List<T> l, IEnumerable<T> items)
            {
                l.AddRange(items);
                return l;
            }
        }

    }
}

#nullable restore