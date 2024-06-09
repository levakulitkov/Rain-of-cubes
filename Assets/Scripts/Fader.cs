using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Fader : MonoBehaviour
{
    [SerializeField] private int _steps = 100;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Reset()
    {
        Color color = _renderer.material.color;
        color.a = 1f;

        _renderer.material.color = color;
    }

    public IEnumerator FadeOut(float duration)
    {
        var wait = new WaitForSeconds(duration / _steps);
        Color color = _renderer.material.color;

        for (int i = 1; i <= _steps; i++)
        {
            color.a = 1 - (i * 1f / _steps);

            _renderer.material.color = color;

            yield return wait;
        }
    }
}
