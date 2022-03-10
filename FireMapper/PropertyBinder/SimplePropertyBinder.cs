using System.Reflection;

namespace FireMapper
{
    public class SimplePropertyBinder : AbstractSimplePropertyBinder
    {
        private PropertyInfo Property;

        public SimplePropertyBinder(PropertyInfo property, bool isFireIgnore) 
            : base(property.PropertyType, property.Name, isFireIgnore)
        {
            this.Property = property;
        }

        public override object GetValue(object target)
        {
            return this.Property.GetValue(target);
        }
    }
}