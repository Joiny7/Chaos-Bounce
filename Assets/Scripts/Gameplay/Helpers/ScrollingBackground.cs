using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    public Rigidbody2D body;
    public BoxCollider2D coll;
    private float verticalLength;
    public float scrollSpeed;

	void Start ()
    {
        verticalLength = coll.size.y;
        body.velocity = new Vector2(0, -scrollSpeed);
	}
	
	void Update ()
    {
        if (transform.position.y <= -verticalLength)
        {
            RepositionBackground();
        }
	}

    private void RepositionBackground()
    {
        transform.localPosition += new Vector3(0, verticalLength * 3, 0);
    }
}