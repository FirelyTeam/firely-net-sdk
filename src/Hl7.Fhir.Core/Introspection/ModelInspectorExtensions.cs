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

        private static Assembly[] _commonAssemblies = new[] { typeof(IModelInfo).Assembly, typeof(Bundle).Assembly };

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
            ModelInspector? inspector = null;

            if (_commonAssemblies.Contains(callingAssembly))
            {
                var assemblies = referredResources(@base)
                    .Select(r => r.GetType().Assembly)
                    .Distinct()
                    .Except(new[] { typeof(IModelInfo).Assembly, }); // remove the common assembly, because that's always loaded


                if (assemblies.Count() > 1)
                {
                    throw new InvalidOperationException("Found multiple assemblies " +
                        $"({string.Join(",", assemblies.Select(a => a.FullName))}) which is not supported.");
                }

                var assembly = assemblies.SingleOrDefault();

                if (assembly is not null) inspector = ModelInspector.ForAssembly(assembly);
            }

            return inspector ?? ModelInspector.ForAssembly(callingAssembly);
        }

        private static IEnumerable<Base> referredResources(Base @base)
        {
            var resources = @base switch
            {
                DomainResource domainResource => domainResource.Contained.Select(c => c).OfType<Base>(),
                Bundle bundle => bundle.Entry.Select(e => e.Resource).OfType<Base>()
                                .Concat(bundle.Entry.Select(e => e.Response?.Outcome).OfType<Base>()),
                Parameters parameters => parameters.Parameter.Select(p => p.Value),

                _ => Enumerable.Empty<Base>()
            };
            resources = resources.Where(t => t is not null);

            return resources.Any() == false
                ? resources
                : resources.Concat(resources.SelectMany(r => referredResources(r))).ToList();
        }

    }
}

#nullable restore