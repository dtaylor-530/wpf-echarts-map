
using System;
using System.Windows.Markup;

namespace SomeNameSpace
{
    public class NestedTypeExtension : TypeExtension
    {
        public NestedTypeExtension() { }

        public NestedTypeExtension(string typeName) : base(typeName) { }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            string[] types = TypeName.Split('.');
            IXamlTypeResolver resolver = (IXamlTypeResolver)serviceProvider.GetService(typeof(IXamlTypeResolver));
            if (resolver != null && types.Length > 0)
            {
                Type t = resolver.Resolve(types[0]);
                for (int i = 1; i < types.Length; i++)
                {
                    t = t.GetNestedType(types[i]);
                }
                Type = t;
                return t;
            }
            return null;
        }
    }
}