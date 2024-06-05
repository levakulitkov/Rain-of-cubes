using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CubesPool : MonoBehaviour
{
    [SerializeField] private Cube _template;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _spawnerTransform;

    private ObjectPool<Cube> _pool;
    private float _minPositionX;
    private float _maxPositionX;
    private float _minPositionY;
    private float _maxPositionY;

    private void Start()
    {
        _pool = new ObjectPool<Cube>(Create, OnGet, OnRelease, OnDestroyObject, false);

        float halfExtentX = _spawnerTransform.transform.localScale.x / 2;
        _minPositionX = _spawnerTransform.transform.position.x - halfExtentX;
        _maxPositionX = _spawnerTransform.transform.position.x + halfExtentX;

        float halfExtentY = _spawnerTransform.transform.localScale.y / 2;
        _minPositionY = _spawnerTransform.transform.position.y - halfExtentY;
        _maxPositionY = _spawnerTransform.transform.position.y + halfExtentY;
    }

    public void Release(Cube cube)
    {
        cube.Destroyed -= Release;

        _pool.Release(cube);
    }

    public Cube Get()
    {
        Cube newCube = _pool.Get();
        newCube.Destroyed += Release;

        return newCube;
    }

    private Cube Create()
    {
        Cube cube = Instantiate(_template, _container);
        SetSpawnPositionAndRotation(cube);

        return cube;
    }

    private void OnGet(Cube cube)
    {
        SetSpawnPositionAndRotation(cube);

        cube.gameObject.SetActive(true);
    }

    private void OnRelease(Cube cube)
    {
        cube.Reset();
    }

    private void OnDestroyObject(Cube cube)
        => Destroy(cube);

    private void SetSpawnPositionAndRotation(Cube cube)
        => cube.transform.SetPositionAndRotation(new Vector2(
            Random.Range(_minPositionX, _maxPositionX),
            Random.Range(_minPositionY, _maxPositionY)),
            Quaternion.identity);
}