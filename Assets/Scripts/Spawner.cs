using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<Spawnable> : MonoBehaviour 
    where Spawnable : MonoBehaviour
{
    [SerializeField] private float _interval;
    [SerializeField] private Spawnable _template;
    [SerializeField] private Transform _container;

    private ObjectPool<Spawnable> _pool;

    public float Interval => _interval;

    private void Start()
    {
        _pool = new ObjectPool<Spawnable>(Create, OnGet, OnRelease, 
            OnDestroyObject, false);
    }

    protected virtual Spawnable Create()
    {
        Spawnable spawnable = Instantiate(_template, _container);
        SetSpawnPositionAndRotation(spawnable);

        return spawnable;
    }

    protected virtual void OnGet(Spawnable spawnable)
    {
        SetSpawnPositionAndRotation(spawnable);

        spawnable.gameObject.SetActive(true);

        spawnable.Destroyed += _pool.Release;
    }

    protected virtual void OnRelease(Spawnable spawnable)
    {
        spawnable.Destroyed -= _pool.Release;

        spawnable.Reset();
    }

    private void OnDestroyObject(Spawnable spawnable)
        => Destroy(spawnable);
}
