/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/


namespace Hl7.Fhir.Serialization
{
    public interface INavigatingParser
    {
        /// <summary>
        /// Move to the next sibling of the current element.
        /// </summary>
        /// <returns>false when there is no next sibling, true otherwise.</returns>
        bool MoveToNext(string nameFilter = null);

        /// <summary>
        /// Move to the first child of the current element.
        /// </summary>
        /// <param name="nameFilter">
        /// If the value is provided, then only elements that have this value for the name should
        /// be considered by the navigator (during MoveNext())
        /// </param>
        /// <returns>false if the element has no children, true otherwise</returns>
        bool MoveToFirstChild(string nameFilter = null);

        /// <summary>
        /// Clone the current navigator
        /// </summary>
        /// <returns>A navigator that is positioned at the same location as the cloned navigator.</returns>
        INavigatingParser Clone();

        /// <summary>
        /// Name of the node, e.g. "active", "valueCodeableConcept".
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The literal value of the node (if the node represents a primitive FHIR value)
        /// </summary>
        string Literal { get; }

        /// <summary>
        /// A format-specific indication of the location of this node within the data that is being parsed.
        /// </summary>
        string Location { get; }
    }
}
