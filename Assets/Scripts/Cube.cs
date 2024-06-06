using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minDelay = 2f;
    [SerializeField] private float _maxDelay = 5f;

    private ColorChanger _colorChanger;
    private bool _isDestructionActivated;

    public Action<Cube> Destroyed;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isDestructionActivated 
            && collision.collider.TryGetComponent(out DestructiveSurface _))
        {
            _isDestructionActivated = true;

            _colorChanger.SetDestructionColor();

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
        var wait = new WaitForSeconds(
            UnityEngine.Random.Range(_minDelay, _maxDelay));
        yield return wait;

        Destroyed?.Invoke(this);
    }
}