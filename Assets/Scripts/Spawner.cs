using UnityEngine;

public class Spawner<Snawnable> : MonoBehaviour where Snawnable : MonoBehaviour
{
    [SerializeField] protected Pool<Snawnable> Pool;
    [SerializeField] protected float Interval;
}
