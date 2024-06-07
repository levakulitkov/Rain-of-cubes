using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField] private float _interval;
    [SerializeField] private Cube _template;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _spawnArea;

    private ObjectPool<Cube> _pool;
    private float _minPositionX;
    private float _maxPositionX;
    private float _minPositionY;
    private float _maxPositionY;

    private void OnEnable()
    {
        StartCoroutine(SpawningRoutine());
    }

    private void Start()
    {
        _pool = new ObjectPool<Cube>(Create, OnGet, OnRelease, OnDestroyObject, false);

        float halfExtentX = _spawnArea.transform.localScale.x / 2;
        _minPositionX = _spawnArea.transform.position.x - halfExtentX;
        _maxPositionX = _spawnArea.transform.position.x + halfExtentX;

        float halfExtentY = _spawnArea.transform.localScale.y / 2;
        _minPositionY = _spawnArea.transform.position.y - halfExtentY;
        _maxPositionY = _spawnArea.transform.position.y + halfExtentY;
    }

    private IEnumerator SpawningRoutine()
    {
        var wait = new WaitForSeconds(_interval);

        while (enabled)
        {
            yield return wait;

            _pool.Get();
        }
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

        cube.Destroyed += _pool.Release;
    }

    private void OnRelease(Cube cube)
    {
        cube.Destroyed -= _pool.Release;

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