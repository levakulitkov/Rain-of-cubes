using System;
using System.Collections;
using UnityEngine;

public class CubesSpawner : Spawner<Cube>
{
    [SerializeField] protected float _interval = 0.1f;

    private void OnEnable()
    {
        StartCoroutine(SpawningRoutine());
    }

    private IEnumerator SpawningRoutine()
    {
        var wait = new WaitForSeconds(_interval);

        while (enabled)
        {
            yield return wait;

            Pool.Get();
        }
    }
}