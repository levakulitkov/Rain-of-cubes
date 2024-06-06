using UnityEngine;
using UnityEngine.Pool;

public class Pool<Poolable> : MonoBehaviour where Poolable : MonoBehaviour
{
    [SerializeField] private Poolable _template;
    [SerializeField] private Transform _container;

    protected ObjectPool<Poolable> ObjectPool;

    private void Awake()
    {
        ObjectPool = new ObjectPool<Poolable>(Create, OnGet, OnRelease, OnDestroyObject, false);
    }

    public virtual void Release(Poolable poolable)
        => ObjectPool.Release(poolable);

    public virtual Poolable Get()
        => ObjectPool.Get();

    protected virtual Poolable Create()
        => Instantiate(_template, _container);

    protected virtual void OnGet(Poolable poolable)
        => poolable.gameObject.SetActive(true);

    protected virtual void OnRelease(Poolable poolable)
        => poolable.gameObject.SetActive(false);

    protected virtual void OnDestroyObject(Poolable poolable)
        => Destroy(poolable);
}
