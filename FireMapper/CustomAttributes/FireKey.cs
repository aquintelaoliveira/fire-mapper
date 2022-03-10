using System;

namespace FireMapper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FireKeyAttribute : Attribute 
    {
        public FireKeyAttribute(){ }
    }
}