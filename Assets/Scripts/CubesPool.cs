using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CubesPool : MonoBehaviour
{
    [SerializeField] private Cube _template;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _spawnerTransform;

    private ObjectPool<Cube> _pool;
    private float _minPosX;
    private float _maxPosX;
    private float _minPosY;
    private float _maxPosY;

    private void Start()
    {
        _pool = new ObjectPool<Cube>(Create, OnGet, OnRelease, OnDestroyObject, false);

        float halfExtentX = _spawnerTransform.transform.localScale.x / 2;
        _minPosX = _spawnerTransform.transform.position.x - halfExtentX;
        _maxPosX = _spawnerTransform.transform.position.x + halfExtentX;

        float halfExtentY = _spawnerTransform.transform.localScale.y / 2;
        _minPosY = _spawnerTransform.transform.position.y - halfExtentY;
        _maxPosY = _spawnerTransform.transform.position.y + halfExtentY;
    }

    public void Release(Cube cube)
        => _pool.Release(cube);

    public Cube Get()
    => _pool.Get();

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

        cube.gameObject.SetActive(false);
    }

    private void OnDestroyObject(Cube cube)
        => Destroy(cube);

    private void SetSpawnPositionAndRotation(Cube cube)
        => cube.transform.SetPositionAndRotation(new Vector2(
            Random.Range(_minPosX, _maxPosX),
            Random.Range(_minPosY, _maxPosY)),
            Quaternion.identity);
}