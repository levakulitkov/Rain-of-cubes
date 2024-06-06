using UnityEngine;

public class BombesSpawner : Spawner<Bomb>
{
    [SerializeField] private CubesPool _cubesPool;

    private void OnEnable()
    {
        _cubesPool.CubeDestroyed += OnCubeDestroyed;
    }

    private void OnDisable()
    {
        _cubesPool.CubeDestroyed -= OnCubeDestroyed;   
    }

    public void OnCubeDestroyed(Vector3 position)
    {
        Spawn(position);
    }

    private void Spawn(Vector3 position)
    {
        Bomb bomb = Pool.Get();

        bomb.transform.position = position;

        bomb.Detonate();
    }
}