using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    private ColorChanger _colorChanger;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    public void Reset()
    {
        _colorChanger.Reset();
    }

    public void SetDestructionColor()
    {
        _colorChanger.SetDestructionColor();
    }
}