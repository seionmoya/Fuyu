using System;

namespace Fuyu.DependencyInjection.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class InjectAllAttribute : InjectAttribute
{
    // InjectAttribute's id can be null here as it doesn't get used
    // in Resolve if the InjectAttribute type is InjectAllAttribute
    public InjectAllAttribute() : base(null)
    {
    }
}