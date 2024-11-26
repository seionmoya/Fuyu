using System;

namespace Fuyu.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class InjectAttribute : Attribute
    {
        public InjectAttribute(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
