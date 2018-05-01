/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/


namespace Hl7.Fhir.Serialization
{
    public class SourceComments
    {
        /// <summary>
        /// Comments encountered after this but before the next element in the document
        /// </summary>
        public string[] CommentsBefore;

        /// <summary>
        /// Comments encountered before the first child in this element
        /// </summary>
        public string[] ClosingComments;

        /// <summary>
        /// Comments encountered before the root element of the document
        /// </summary>
        public string[] DocumentEndComments;
    }   
}