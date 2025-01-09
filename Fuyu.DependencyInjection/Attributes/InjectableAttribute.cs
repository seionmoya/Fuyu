using System;

namespace Fuyu.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class InjectableAttribute : Attribute
    {
    }
}