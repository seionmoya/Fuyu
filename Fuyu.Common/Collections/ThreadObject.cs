namespace Fuyu.Common.Collections;
#if NET9_0_OR_GREATER
using Lock = System.Threading.Lock;
#else
using Lock = object;
#endif

public class ThreadObject<T>
{
    private T _object;
    private readonly Lock _lock;

    public ThreadObject(T value)
    {
        _object = value;
        _lock = new Lock();
    }

    public T Get()
    {
        lock (_lock)
        {
            return _object;
        }
    }

    public void Set(T value)
    {
        lock (_lock)
        {
            _object = value;
        }
    }
}