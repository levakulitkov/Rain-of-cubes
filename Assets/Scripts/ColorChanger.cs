using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _newColor;

    private Renderer _renderer;
    private Color _defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        _defaultColor = _renderer.material.color;
    }

    public void SetDestructionColor()
    {
        _renderer.material.color = _newColor;
    }

    public void Reset()
    {
        _renderer.material.color = _defaultColor;
    }
}