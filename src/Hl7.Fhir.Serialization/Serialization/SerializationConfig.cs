using Hl7.Fhir.Introspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public static class SerializationConfig
    {
        public const string RESOURCETYPE_MEMBER_NAME = "resourceType";
        public const string BINARY_CONTENT_MEMBER_NAME = "content";


        public static bool AcceptUnknownMembers { get; set; }

        private static Lazy<ModelInspector> _inspector = new Lazy<ModelInspector>();

        internal static ModelInspector Inspector 
        { 
            get
            {
                return _inspector.Value;
            }
        }

        private static Lazy<ModelFactoryList> _modelClassFactories = createDefaultClassFactoryList();

        private static Lazy<ModelFactoryList> createDefaultClassFactoryList()
        {
            return new Lazy<ModelFactoryList>(() =>
                new ModelFactoryList { new DefaultModelFactory(Inspector) });
        }

        public static void Clear()
        {
            _inspector = new Lazy<ModelInspector>();
        }

        public static ModelFactoryList ModelClassFactories
        {
            get { return _modelClassFactories.Value; }
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
