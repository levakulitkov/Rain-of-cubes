using UnityEngine;
using Random = UnityEngine.Random;

public class CubesPool : Pool<Cube>
{
    [SerializeField] private Transform _spawnerTransform;

    private float _minPositionX;
    private float _maxPositionX;
    private float _minPositionY;
    private float _maxPositionY;

    private void Start()
    {
        float halfExtentX = _spawnerTransform.transform.localScale.x / 2;
        _minPositionX = _spawnerTransform.transform.position.x - halfExtentX;
        _maxPositionX = _spawnerTransform.transform.position.x + halfExtentX;

        float halfExtentY = _spawnerTransform.transform.localScale.y / 2;
        _minPositionY = _spawnerTransform.transform.position.y - halfExtentY;
        _maxPositionY = _spawnerTransform.transform.position.y + halfExtentY;
    }

    public override void Release(Cube cube)
    {
        cube.Destroyed -= Release;

        base.Release(cube);
    }

    public override Cube Get()
    {
        Cube newCube = base.Get();
        newCube.Destroyed += Release;

        return newCube;
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

        base.OnGet(cube);
    }

    protected override void OnRelease(Cube cube)
        => cube.Reset();

    private void SetSpawnPositionAndRotation(Cube cube)
        => cube.transform.SetPositionAndRotation(new Vector2(
            Random.Range(_minPositionX, _maxPositionX),
            Random.Range(_minPositionY, _maxPositionY)),
            Quaternion.identity);
}