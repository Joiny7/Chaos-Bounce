using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSizeOverTime : MonoBehaviour {

    public float sizeChangeValue;
    private float timer;
    	
	private void Update ()
    {
        gameObject.transform.localScale *= sizeChangeValue;
    }
}