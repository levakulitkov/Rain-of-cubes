using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestructionActivator : MonoBehaviour
{
    [SerializeField] private float _minDelay = 2f;
    [SerializeField] private float _maxDelay = 5f;
    [SerializeField] private CubesPool _pool;

    private readonly List<Cube> _activatedCubes = new();

    public void TryActivate(Cube cube)
    {
        if (!_activatedCubes.Contains(cube))
        {
            cube.SetDestructionColor();

            _activatedCubes.Add(cube);

            StartCoroutine(StartDestruction(cube));
        }
    }

    private IEnumerator StartDestruction(Cube cube)
    {
        var wait = new WaitForSeconds(Random.Range(_minDelay, _maxDelay));
        yield return wait;

        _pool.Release(cube);

        _activatedCubes.Remove(cube);
    }
}