using System;
using UnityEngine;

public abstract class Poolable<T> : MonoBehaviour
{
    public abstract event Action<T> Destroyed;

    public abstract void Reset();
}