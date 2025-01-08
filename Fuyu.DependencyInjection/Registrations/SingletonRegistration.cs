namespace Fuyu.DependencyInjection.Registrations
{
    internal class SingletonRegistration<T> : AbstractDependencyRegistration
    {
        public T Instance { get; }

        internal SingletonRegistration(string id, T instance)
        {
            Id = id;
            Instance = instance;
        }

        public override object GetValue()
        {
            return Instance;
        }
    }
}
