using System;

namespace Hl7.Fhir.Model
{
    public interface IModelInfo
    {
        Type GetTypeForFhirType(string name);
        string GetFhirTypeNameForType(Type type);
        bool IsProfiledQuantity(string type);
    }

    public static class ModelInfos
    {
        public static IModelInfo Get(Version version)
        {
            switch (version)
            {
                case Version.DSTU2:
                    return DSTU2ModelInfo.Instance;
                case Version.STU3:
                    return STU3ModelInfo.Instance;
                default:
                    throw new ArgumentException($"Unknown or not support FHIR version '{version}'", nameof(version));
            }
        }

        private class DSTU2ModelInfo : IModelInfo
        {
            public static DSTU2ModelInfo Instance = new DSTU2ModelInfo();

            public Type GetTypeForFhirType(string name)
            {
                return DSTU2.ModelInfo.GetTypeForFhirType(name);
            }

            public string GetFhirTypeNameForType(Type type)
            {
                return DSTU2.ModelInfo.GetFhirTypeNameForType(type);
            }

            public bool IsProfiledQuantity(string type)
            {
                return DSTU2.ModelInfo.IsProfiledQuantity(type);
            }
        }

        private class STU3ModelInfo : IModelInfo
        {
            public static STU3ModelInfo Instance = new STU3ModelInfo();

            public Type GetTypeForFhirType(string name)
            {
                return STU3.ModelInfo.GetTypeForFhirType(name);
            }

            public string GetFhirTypeNameForType(Type type)
            {
                return STU3.ModelInfo.GetFhirTypeNameForType(type);
            }

            public bool IsProfiledQuantity(string type)
            {
                return STU3.ModelInfo.IsProfiledQuantity(type);
            }
        }
    }
}
