using UnityEngine;

public class BombesPool : Pool<Bomb>
{
    protected override void OnGet(Bomb bomb)
    {
        bomb.Exploded += Release;

        base.OnGet(bomb);
    }

    protected override void OnRelease(Bomb bomb)
    {
        bomb.Exploded -= Release;

        base.OnRelease(bomb);

        bomb.Reset();
    }
}