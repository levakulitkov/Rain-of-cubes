using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubesSpawner : Spawner<Cube>
{
    [SerializeField] private float _interval;
    [SerializeField] private Transform _spawnArea;

    private float _minPositionX;
    private float _maxPositionX;
    private float _minPositionY;
    private float _maxPositionY;

    public event Action<Vector3> CubeDestroyed;

    private void OnEnable()
    {
        StartCoroutine(SpawningRoutine());
    }

    private void Start()
    {
        float halfExtentX = _spawnArea.transform.localScale.x / 2;
        _minPositionX = _spawnArea.transform.position.x - halfExtentX;
        _maxPositionX = _spawnArea.transform.position.x + halfExtentX;

        float halfExtentY = _spawnArea.transform.localScale.y / 2;
        _minPositionY = _spawnArea.transform.position.y - halfExtentY;
        _maxPositionY = _spawnArea.transform.position.y + halfExtentY;
    }

    protected override void OnGet(Cube cube)
    {
        SetSpawnPositionAndRotation(cube);

        cube.Destroyed += OnCubeDestroyed;

        base.OnGet(cube);
    }

    protected override void OnRelease(Cube cube)
    {
        cube.Destroyed -= OnCubeDestroyed;

        base.OnRelease(cube);
    }

    private void SetSpawnPositionAndRotation(Cube cube)
        => cube.transform.SetPositionAndRotation(new Vector2(
            Random.Range(_minPositionX, _maxPositionX),
            Random.Range(_minPositionY, _maxPositionY)),
            Quaternion.identity);

    private void OnCubeDestroyed(Cube cube)
        => CubeDestroyed?.Invoke(cube.transform.position);

    private IEnumerator SpawningRoutine()
    {
        var wait = new WaitForSeconds(_interval);

        while (enabled)
        {
            yield return wait;

            Get();
        }
    }
}