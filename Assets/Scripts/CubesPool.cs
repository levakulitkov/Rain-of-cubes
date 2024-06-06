using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubesPool : Pool<Cube>
{
    [SerializeField] private Transform _spawnerTransform;

    private float _minPositionX;
    private float _maxPositionX;
    private float _minPositionY;
    private float _maxPositionY;

    public Action<Vector3> CubeDestroyed;

    private void Start()
    {
        float halfExtentX = _spawnerTransform.transform.localScale.x / 2;
        _minPositionX = _spawnerTransform.transform.position.x - halfExtentX;
        _maxPositionX = _spawnerTransform.transform.position.x + halfExtentX;

        float halfExtentY = _spawnerTransform.transform.localScale.y / 2;
        _minPositionY = _spawnerTransform.transform.position.y - halfExtentY;
        _maxPositionY = _spawnerTransform.transform.position.y + halfExtentY;
    }

    protected override Cube Create()
    {
        Cube cube = base.Create();
        SetSpawnPositionAndRotation(cube);

        return cube;
    }

    protected override void OnGet(Cube cube)
    {
        SetSpawnPositionAndRotation(cube);

        cube.Destroyed += Release;
        cube.Destroyed += OnCubeDestroyed;

        base.OnGet(cube);
    }

    protected override void OnRelease(Cube cube)
    {
        cube.Destroyed -= Release;
        cube.Destroyed -= OnCubeDestroyed;

        base.OnRelease(cube);

        cube.Reset();
    }

    private void SetSpawnPositionAndRotation(Cube cube)
        => cube.transform.SetPositionAndRotation(new Vector2(
            Random.Range(_minPositionX, _maxPositionX),
            Random.Range(_minPositionY, _maxPositionY)),
            Quaternion.identity);

    private void OnCubeDestroyed(Cube cube)
        => CubeDestroyed?.Invoke(cube.transform.position);
}