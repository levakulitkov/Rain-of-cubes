using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CubesSpawner : Spawner<Cube>
{
    [SerializeField] private Transform _spawnArea;

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
        float halfExtentX = _spawnArea.transform.localScale.x / 2;
        _minPositionX = _spawnArea.transform.position.x - halfExtentX;
        _maxPositionX = _spawnArea.transform.position.x + halfExtentX;

        float halfExtentY = _spawnArea.transform.localScale.y / 2;
        _minPositionY = _spawnArea.transform.position.y - halfExtentY;
        _maxPositionY = _spawnArea.transform.position.y + halfExtentY;
    }

    private IEnumerator SpawningRoutine()
    {
        var wait = new WaitForSeconds(Interval);

        while (enabled)
        {
            yield return wait;

            _pool.Get();
        }
    }

    //protected override Cube Create()
    //{
    //    Cube cube = base.Create();
    //    SetSpawnPositionAndRotation(cube);

    //    return cube;
    //}

    protected virtual void OnGet(Cube cube)
    {
        SetSpawnPositionAndRotation(cube);

        cube.gameObject.SetActive(true);

        cube.Destroyed += _pool.Release;
    }

    private void SetSpawnPositionAndRotation(Cube cube)
        => cube.transform.SetPositionAndRotation(new Vector2(
            Random.Range(_minPositionX, _maxPositionX),
            Random.Range(_minPositionY, _maxPositionY)),
            Quaternion.identity);
}