using UnityEngine;

public class BombesSpawner : Spawner<Bomb>
{
    [SerializeField] private CubesSpawner _cubesSpawner;

    private void OnEnable()
    {
        _cubesSpawner.CubeDestroyed += OnCubeDestroyed;
    }

    private void OnDisable()
    {
        _cubesSpawner.CubeDestroyed -= OnCubeDestroyed;
    }

    public void OnCubeDestroyed(Vector3 position)
    {
        Spawn(position);
    }

    private void Spawn(Vector3 position)
    {
        Bomb bomb = Get();

        bomb.transform.position = position;

        bomb.Detonate();
    }
}