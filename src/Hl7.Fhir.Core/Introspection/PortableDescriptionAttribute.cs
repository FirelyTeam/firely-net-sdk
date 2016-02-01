#if PORTABLE45
namespace System.ComponentModel
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class DescriptionAttribute : Attribute
    {
        readonly string description;

        public DescriptionAttribute(string description)
        {
            this.description = description;
        }

        public string Description
        {
            get { return description; }
        }
    }
}
#endif
