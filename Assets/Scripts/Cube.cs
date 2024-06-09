using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minDelay = 2f;
    [SerializeField] private float _maxDelay = 5f;

    private ColorChanger _colorChanger;
    private bool _isDestructionActivated;

    public event Action<Cube> Destroyed;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isDestructionActivated == false
            && collision.collider.TryGetComponent(out DestructiveSurface _))
        {
            _isDestructionActivated = true;

            _colorChanger.Change();

            StartCoroutine(StartDestruction());
        }
    }

    public void Reset()
    {
        _colorChanger.Reset();

        _isDestructionActivated = false;
    }

    private IEnumerator StartDestruction()
    {
        yield return new WaitForSeconds(Random.Range(_minDelay, _maxDelay));

        Destroyed?.Invoke(this);
    }
}