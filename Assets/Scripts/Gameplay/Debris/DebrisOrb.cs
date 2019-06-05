using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisOrb : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer rend;

    private void Awake()
    {
        if (GetComponent<Rigidbody2D>().sharedMaterial == null)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        //hitsRemaining = 0;
    }

    //public void SetColour(Color col)
    //{
    //    rend.color = col;
    //}

    //public void SetRandomBodyColour()
    //{
    //    float r = (Random.Range(0f, 255f) / 255);
    //    float g = (Random.Range(0f, 255f) / 255);
    //    float b = (Random.Range(0f, 255f) / 255);
    //    Color col = new Color(r, g, b, 0.8f);
    //    SetColour(col);
    //}
}