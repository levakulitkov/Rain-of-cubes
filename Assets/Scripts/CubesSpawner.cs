using System.Collections;
using UnityEngine;

public class CubesSpawner : Spawner<Cube>
{
    private void OnEnable()
    {
        StartCoroutine(SpawningRoutine());
    }

    private IEnumerator SpawningRoutine()
    {
        var wait = new WaitForSeconds(Interval);

        while (enabled)
        {
            yield return wait;

            Pool.Get();
        }
    }
}