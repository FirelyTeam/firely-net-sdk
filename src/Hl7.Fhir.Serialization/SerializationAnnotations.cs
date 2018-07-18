/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{
    public class SourceComments
    {
        /// <summary>
        /// Comments encountered before this node, but after the previous sibling
        /// </summary>
        public string[] CommentsBefore;

        /// <summary>
        /// Comments encountered after the last child of this element
        /// </summary>
        public string[] ClosingComments;

        /// <summary>
        /// Comments encountered after the root element of the document
        /// </summary>
        public string[] DocumentEndComments;
    } 
    
    public class ResourceTypeIndicator
    {
        public string ResourceType;
    }

    public static class SerializationNavigatorExtensions
    {
        public static string GetResourceType(this IAnnotated ia) => ia.TryGetAnnotation<ResourceTypeIndicator>(out var rt) ? rt.ResourceType : null;

        public static string GetResourceType(this ISourceNavigator navigator) => navigator is IAnnotated ia ? ia.GetResourceType() : null;

        public static string GetResourceType(this IElementNavigator navigator) => navigator is IAnnotated ia ? ia.GetResourceType() : null;
    }
}