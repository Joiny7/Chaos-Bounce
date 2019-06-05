using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotationStart : MonoBehaviour {

	void Start () {
        Quaternion rot = new Quaternion(0,0, Random.Range(-1.0f, 1.0f), 1);
        transform.rotation = rot;
	}
}
