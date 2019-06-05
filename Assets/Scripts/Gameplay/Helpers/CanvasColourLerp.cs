using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasColourLerp : MonoBehaviour {

    public Color colour1;
    public Color colour2;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        colour1.a = 1;
        colour2.a = 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        spriteRenderer.color = Color.Lerp(colour1, colour2, Mathf.PingPong(Time.time, 1));
    }
}
