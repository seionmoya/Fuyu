using System;

namespace Fuyu.DependencyInjection.Registrations
{
    internal class TransientRegistration<T> : DependencyRegistration
    {
        public Func<T> Factory { get; }

        internal TransientRegistration(string id, Func<T> factory)
        {
            Id = id;
            Factory = factory;
        }

        public override object GetValue()
        {
            return Factory();
        }
    }
}
