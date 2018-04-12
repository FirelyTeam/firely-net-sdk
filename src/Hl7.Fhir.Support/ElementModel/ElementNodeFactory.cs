/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.ElementModel
{
    public partial class ElementNode
    {
        public static ElementNode FromNavigator(IElementNavigator navigator)
        {
            return buildNode(navigator);
        }

        private static ElementNode buildNode(IElementNavigator navigator)
        {
            var me = new ElementNode(navigator.Name, navigator.Value, navigator.Type);

            var childNav = navigator.Clone();

            if(childNav.MoveToFirstChild())
            {
                do
                {
                    me.Add(buildNode(childNav));
                } while (childNav.MoveToNext());
            }

            return me;
        }
    }
}
