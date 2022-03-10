using System;

namespace FireMapper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FireIgnoreAttribute : Attribute 
    {
        public FireIgnoreAttribute() {}
    }
}