using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fader))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _minDelay = 2f;
    [SerializeField] private float _maxDelay = 5f;
    [SerializeField] private float _explosuionRadius = 5f;
    [SerializeField] private float _explosuionForce = 1000f;

    private Fader _fader;

    public Action<Bomb> Exploded;

    private void Awake()
    {
        _fader = GetComponent<Fader>();
    }

    public void Reset()
    {
        _fader.Reset();
    }

    public void Detonate()
    {
        StartCoroutine(StartDetonation());
    }

    private IEnumerator StartDetonation()
    {
        float duration = UnityEngine.Random.Range(_minDelay, _maxDelay);

        yield return _fader.FadeOut(duration);

        Explode();

        Exploded?.Invoke(this);
    }

    private void Explode()
    {
        List<Rigidbody> explodableObjects = GetExplodableObjects();
        foreach (Rigidbody explodableObject in explodableObjects)
            explodableObject.AddExplosionForce(_explosuionForce,
                transform.position, _explosuionRadius);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        var explodableObjects = new List<Rigidbody>();

        Collider[] colliders = Physics.OverlapSphere(transform.position,
            _explosuionRadius);

        foreach (Collider collider in colliders)
            if (collider.TryGetComponent(out Rigidbody rigidbody))
                explodableObjects.Add(rigidbody);

        return explodableObjects;
    }
}