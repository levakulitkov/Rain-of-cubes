using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : Poolable<T>
{
    [SerializeField] private T _template;
    [SerializeField] private Transform _container;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(Create, OnGet, OnRelease,
            OnDestroyObject, false);
    }

    public T Get()
        => _pool.Get();

    protected virtual T Create()
        => Instantiate(_template, _container);

    protected virtual void OnGet(T poolable)
    {
        poolable.gameObject.SetActive(true);

        poolable.Destroyed += _pool.Release;
    }

    protected virtual void OnRelease(T poolable)
    {
        poolable.Destroyed -= _pool.Release;

        poolable.Reset();

        poolable.gameObject.SetActive(false);
    }

    private void OnDestroyObject(T poolable)
        => Destroy(poolable);
}