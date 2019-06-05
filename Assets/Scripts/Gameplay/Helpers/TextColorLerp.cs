using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextColorLerp : MonoBehaviour {

    private TextMeshPro text;
    public Color colour1;
    public Color colour2;

    private void Awake()
    {
        colour1.a = 1;
        colour2.a = 1;
        text = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        text.faceColor = Color.Lerp(colour1, colour2, Mathf.PingPong(Time.time, 1));
    }
}
