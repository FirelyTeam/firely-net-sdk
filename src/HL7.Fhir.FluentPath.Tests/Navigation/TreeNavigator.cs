using System;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.FluentPath.InstanceTree;

namespace Hl7.Fhir.Navigation
{
    public class TreeNavigator : IElementNavigator
    {
        FhirInstanceTree tree;
        FhirInstanceTree current;

        public TreeNavigator(FhirInstanceTree tree)
        {
            this.tree = tree;
            current = tree;
        }

        public TreeNavigator(FhirInstanceTree tree, FhirInstanceTree current)
        {
            this.tree = tree;
            this.current = current;
        }

        public string Name
        {
            get
            {
                return current.Name;
            }
        }

        public object Value
        {
            get
            {
                return current.ObjectValue;
            }
        }

        public IElementNavigator Clone()
        {
            return new TreeNavigator(tree, current);
        }

        public bool MoveToFirstChild()
        {
            if (current.FirstChild != null) 
            {
                current = current.FirstChild;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MoveToNext()
        {
            if (current.NextSibling != null)
            {
                current = current.NextSibling;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}