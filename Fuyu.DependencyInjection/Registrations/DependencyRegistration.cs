namespace Fuyu.DependencyInjection.Registrations
{
	internal abstract class DependencyRegistration
    {
        public string Id { get; set; }

        public abstract object GetValue();
    }
}
