/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

namespace Hl7.ElementModel
{
    public interface IElementNavigator : INavigator<IElementNavigator>, ITypeNameProvider, INamedNode, IValueProvider, IPositionProvider
    {
    }

    public interface IElementNode : INode<IElementNode>, ITypeNameProvider, INamedNode, IValueProvider, IPositionProvider
    {
    }
}