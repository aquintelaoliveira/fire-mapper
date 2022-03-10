using System.Reflection;

namespace FireMapper
{
    public class ComplexPropertyBinder : AbstractComplexPropertyBinder
    {
        private PropertyInfo Property;
        private string FireKeyName;

        public ComplexPropertyBinder(PropertyInfo property, bool isFireIgnore, string fireKeyName, IDataMapper dataMapper) 
            : base(property.PropertyType, property.Name, isFireIgnore, dataMapper)
        {
            this.Property = property;
            this.FireKeyName = fireKeyName;
        }

        public override object GetValue(object target)
        {
            return this.Property.GetValue(target);
        }

        public override object GetFireKeyValue(object target)
        {
            return target.GetType().GetProperty(FireKeyName).GetValue(target);
        }
    }
}