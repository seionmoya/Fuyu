namespace Fuyu.DependencyInjection.Registrations
{
    internal abstract class AbstractDependencyRegistration
    {
        public string Id { get; set; }

        public abstract object GetValue();
    }
}