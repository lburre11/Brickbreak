using UnityEngine;

public class BrickHealth : MonoBehaviour
{
    private Color[] HPColors;
    private SpriteRenderer _spriteRenderer;
    private int HP;

    public void Initialize(Color[] brickHP, int Color)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        HPColors = brickHP;
        HP = Color + 1;
        _spriteRenderer.color = HPColors[Color];
    }

    public void Hit()
    {
        HP--;
        if (HP == 0)
        {
            Destroy(gameObject);
            return;
        }

        _spriteRenderer.color = HPColors[HP - 1];
    }
}
