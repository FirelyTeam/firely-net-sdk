//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hl7.Fhir.Specification.Schema
//{
//    public partial class FhirSchema : IJsonSerializable
//    {
//        JObject IJsonSerializable.ToJson()
//        {
//            JObject result = new JObject();

//            if (Id != null)
//                result.Add(new JProperty("id", Id));

//            addAnnotations("annotations",OnSuccess);
//            addAnnotations("onfail", OnFailure);
//            addAnnotations("on-undecided",OnUndecided);

//            return result;

//            void addAnnotations(string name, SchemaAnnotations sa)
//            {
//                if (sa is IJsonSerializable js)
//                    result.Add(new JProperty(name,js.ToJson()));
//            }
//        }
//    }

//    public partial class SchemaAnnotations : IJsonSerializable
//    {
//        JObject IJsonSerializable.ToJson()
//        {
//            return new JObject
//            {
//                this
//                .GroupBy(o => o.GetType().Name)
//                .Select(grp => new JProperty(grp.Key,
//                   new JArray(grp.Select(v => asJToken(v)))))
//            };

//            JToken asJToken(object source)
//            {
//                var json = JsonConvert.SerializeObject(source);
//                return JToken.Parse(json);
//            }
//        }
//    }
//}
