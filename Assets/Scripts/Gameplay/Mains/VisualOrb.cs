using UnityEngine;

public class VisualOrb : MonoBehaviour {

    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite plasmaSprite;
    [SerializeField]
    private Sprite fireSprite;
    [SerializeField]
    private Sprite electricitySprite;

    SpriteRenderer spriteRenderer;

    private void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Plasma(bool state)
    {
        spriteRenderer.sprite = state ? plasmaSprite : defaultSprite;
    }

    public void Fire (bool state) {
        spriteRenderer.sprite = state ? fireSprite : defaultSprite;
    }

    public void Electricity (bool state) {
        spriteRenderer.sprite = state ? electricitySprite : defaultSprite;
    }
}