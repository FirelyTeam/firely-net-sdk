/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Fhir.Profiling
{
    public static class ValidateExtensions
    {
        public static Report Validate(this Specification specification)
        {
            SpecificationValidator pv = new SpecificationValidator(specification);
            return pv.Validate();
        }

        public static Report Validate(this Specification specification, XPathNavigator root)
        {
            ResourceValidator validator = new ResourceValidator(specification);
            Report report = validator.Validate(root);
            return report; 
        }

        public static bool Assert(this Specification specification, XPathNavigator resource, out Outcome outcome)
        {
            ResourceValidator validator = new ResourceValidator(specification);
            validator.LogOutcome += (o) => { if (o.Kind.Failed()) throw new ValidationException(o); };
            try
            {
                Report report = validator.Validate(resource);
                outcome = null;
                return true; 
            }
            catch (ValidationException e)
            {
                outcome = e.Outcome;
                return false;
            }
            
        }

        

    }
}
