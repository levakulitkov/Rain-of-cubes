using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    public ColorChanger ColorChanger;

    private void Awake()
    {
        ColorChanger = GetComponent<ColorChanger>();
    }

    public void Reset()
    {
        ColorChanger.Reset();
    }
}