using UnityEngine;

public class DestructiveSurface : MonoBehaviour
{
    [SerializeField] private CubeDestructionActivator _cubeDestructionActivator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Cube cube))
            _cubeDestructionActivator.TryActivate(cube);
    }
}