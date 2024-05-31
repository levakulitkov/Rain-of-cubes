using System.Collections;
using UnityEngine;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField] private CubesPool _cubesPool;
    [SerializeField] private float _interval;

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

            _cubesPool.Get();
        }
    }
}