/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Serialization
{
    public static class SerializationConfig
    {
        public const string BINARY_CONTENT_MEMBER_NAME = "content";


        public static bool AcceptUnknownMembers { get; set; }

        public static bool EnforceNoXsiAttributesOnRoot { get; set; }

        private static Lazy<ModelInspector> _inspector = createDefaultModelInspector();

        private static Lazy<ModelInspector> createDefaultModelInspector()
        {
			return new Lazy<ModelInspector>(() =>
                {
                    var result = new ModelInspector();
#if PORTABLE45
                    result.Import(typeof(Resource).GetTypeInfo().Assembly);
#else
				result.Import(typeof(Resource).Assembly);
#endif
                return result;
			});

		}

        internal static ModelInspector Inspector 
        { 
            get
            {
                return _inspector.Value;
            }
        }

        public static void Clear()
        {
            _inspector = createDefaultModelInspector();
        }

        public static void AddModelAssembly(Assembly assembly)
        {
            Inspector.Import(assembly);
        }

        public static void AddModelType(Type type)
        {
            if (type.IsEnum())
                Inspector.ImportEnum(type);
            else
                Inspector.ImportType(type);
        }
    }
}
