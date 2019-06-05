using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {

    public float timeUntilDestroy;
    private float timer;
	
	private void Update ()
    {
        if (timer >= timeUntilDestroy)
        {
            Destroy(gameObject);
        }

        timer += Time.deltaTime;
	}
}