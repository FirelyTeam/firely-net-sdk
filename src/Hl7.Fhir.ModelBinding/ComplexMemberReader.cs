using Hl7.Fhir.ModelBinding.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Hl7.Fhir.ModelBinding
{
    public class ComplexMemberReader
    {
        public const string RESOURCETYPE_MEMBER_NAME = "resourceType";
        private readonly string NORMALIZED_RESOURCETYPE_MEMBER_NAME = RESOURCETYPE_MEMBER_NAME.ToUpperInvariant();

        private JObject _data;

        public ComplexMemberReader(JObject data)
        {
            _data = data;
        }

        public void Deserialize(object existingInstance)
        {
            if (existingInstance == null) throw Error.ArgumentNull("existingInstance");
           
            Type objectType = existingInstance.GetType();
            if (!ReflectionHelper.IsComplexType(objectType)) throw Error.InvalidOperation(Messages.CanOnlyDeserializeComplex, objectType.Name);

            var typeMembers = ReflectionHelper.FindPublicProperties(objectType);
                
            foreach (var memberData in _data)
            {
                var memberName = memberData.Key;
                var normalizedMemberName = memberName.ToUpperInvariant();

                PropertyInfo propInfo;

                if (typeMembers.TryGetValue(normalizedMemberName, out propInfo))
                {
                    Debug.WriteLine("Handling member " + memberName);

                    var value = propInfo.GetValue(existingInstance,null);

                    var reader = new DispatchingReader(_data[memberName]);

                    if (value == null)
                        value = reader.Deserialize(propInfo.PropertyType);
                    else
                        value = reader.Deserialize(value);

                    propInfo.SetValue(existingInstance, value, null);
                }
                else if(NORMALIZED_RESOURCETYPE_MEMBER_NAME.Equals(normalizedMemberName))
                {
                    // Ignore type discriminator in Resources, that member is used in the 
                    // DispatchingReader to figure out the actual type when this is unknown
                    // from the context
                }
                else
                {
                    if (BindingConfiguration.AcceptUnknownMembers == false)
                        throw Error.InvalidOperation(Messages.DeserializeUnknownMember, memberName);
                    else
                        Debug.WriteLine("Skipping unknown member " + memberName);
                }

                //TODO: handle extension array in complex object
                //TODO: handle _name containing id/extensions for primitive members
                //TODO: handle Code<> members
                //TODO: handle narrative
                //TODO: handle contained objects
            }
        }
    }

}
