using Hl7.Fhir.Introspection;
using System;

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Common interface for different version-specific variants of ResourceReference
    /// </summary>
    public interface IResourceReference
    {
        /// <summary>
        /// Native .net typed version of the string Reference property
        /// </summary>
        Uri Url { get; }
 
        /// <summary>
        /// Relative, internal or absolute URL reference
        /// </summary>
        string Reference { get; }

        /// <summary>
        /// Text alternative for the resource
        /// </summary>
        string Display { get; }

        /// <summary>
        /// Logical reference, when literal reference is not known
        /// </summary>
        IIdentifier Identifier { get; }

        /// <summary>
        /// Determines whether the ResourceReference is a reference to a contained resource (i.e. the reference value starts with an Url anchor ('#'))
        /// </summary>
        bool IsContainedReference { get; }
    }

    /// <summary>
    /// Resource reference class common across different versions
    /// </summary>
    /// <remarks>Not to be serialized / de-serialized, hence the NotMapped attribute</remarks>
    [NotMapped]
    public class CommonResourceReference : IResourceReference
    {
        public CommonResourceReference(string reference)
        {
            Reference = reference;
        }

        public CommonResourceReference(string reference, string display)
        {
            Reference = reference;
            Display = display;
        }

        public CommonResourceReference()
        {

        }

        /// <summary>
        /// Native .net typed version of the string Reference property
        /// </summary>
        public Uri Url
        {
            get
            {
                if (Reference != null)
                    return new Uri(Reference, UriKind.RelativeOrAbsolute);
                else
                    return null;
            }
            set
            {
                if (value != null)
                    Reference = value.ToString();
                else
                    Reference = null;
            }
        }

        /// <summary>
        /// Relative, internal or absolute URL reference
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Text alternative for the resource
        /// </summary>
        public string Display { get; set;  }

        /// <summary>
        /// Logical reference, when literal reference is not known
        /// </summary>
        public IIdentifier Identifier { get; set; }

        /// <summary>
        /// Determines whether the ResourceReference is a reference to a contained resource (i.e. the reference value starts with an Url anchor ('#'))
        /// </summary>
        public bool IsContainedReference
        {
            get
            {
                return Reference != null && Reference.StartsWith("#");
            }
        }
    }
}
