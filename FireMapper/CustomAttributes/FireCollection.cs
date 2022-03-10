using System;

namespace FireMapper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class FireCollectionAttribute : Attribute 
    {
        string name;
        public FireCollectionAttribute(string name) 
        {
            this.name = name;
        }
        public virtual string Name
        {
            get { return name; }
        }
    }
}