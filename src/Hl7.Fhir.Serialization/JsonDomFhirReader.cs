using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public class JsonDomFhirReader : IFhirReader
    {
        public const string RESOURCETYPE_MEMBER_NAME = "resourceType";

        private JToken _current;

        internal JToken Current { get { return _current; } }            // just for while refactoring

        public JsonDomFhirReader(JToken root)
        {
            _current = root;
        }

        public bool IsAtComplexObject()
        {
            return _current is JObject;
        }

        public string GetResourceTypeName()
        {
            if (!IsAtComplexObject())
                throw Error.InvalidOperation("Need to be at a complex object to determine resource type");

            var resourceTypeMember = ((JObject)_current)[RESOURCETYPE_MEMBER_NAME];

            if (resourceTypeMember != null)
            {
                if (resourceTypeMember is JValue)
                {
                    var memberValue = (JValue)resourceTypeMember;

                    if (memberValue.Type == JTokenType.String)
                    {
                        return (string)memberValue.Value;
                    }
                }

                throw Error.InvalidOperation("resourceType should be a primitive string json value");
            }

            throw Error.InvalidOperation("Cannot determine type of resource to create from json input data: no member {0} was found", RESOURCETYPE_MEMBER_NAME);
        }

        //When enumerating properties for a complex object, make sure not to let resourceType get through
        //TODO: Detecting whether this is a special, serialization format-specific member should be
        //done in an abstraction around the json or xml readers.


        public IEnumerable<Tuple<string, IFhirReader>> GetMembers()
        {
            var complex = _current as JObject;
 
            if (complex == null)
                throw Error.InvalidOperation("Need to be at a complex object to list child members");
           
            foreach(var member in complex)
            {
                var memberName = member.Key;

                if(memberName != RESOURCETYPE_MEMBER_NAME)
                {
                    IFhirReader nestedReader = new JsonDomFhirReader(member.Value);

                    // Map contents of _membername elements to the normal 'membername'
                    // effectively treating this as if an objects properies are spread out
                    // over two separate json objects
                    if (memberName.StartsWith("_")) memberName = memberName.Remove(0, 1);

                    yield return Tuple.Create(memberName,nestedReader);
                }
            }
        }
    }
}
