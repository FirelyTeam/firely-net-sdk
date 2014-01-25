using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public static class SerializationConfig
    {
        public const string BINARY_CONTENT_MEMBER_NAME = "content";


        public static bool AcceptUnknownMembers { get; set; }

        private static Lazy<ModelInspector> _inspector = createDefaultModelInspector();

        private static Lazy<ModelInspector> createDefaultModelInspector()
        {
            return new Lazy<ModelInspector>(() =>
                {
                    var result = new ModelInspector();
                    result.Import(typeof(Resource).Assembly);
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
            if (type.IsEnum)
                Inspector.ImportEnum(type);
            else
                Inspector.ImportType(type);
        }
    }
}
