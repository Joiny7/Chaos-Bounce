using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyColorLerp : MonoBehaviour {

    private SpriteRenderer rend;
    public Color colour1;
    public Color colour2;

    private void Awake()
    {
        colour1.a = 1;
        colour2.a = 1;
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        rend.color = Color.Lerp(colour1, colour2, Mathf.PingPong(Time.time, 1));
    }
}