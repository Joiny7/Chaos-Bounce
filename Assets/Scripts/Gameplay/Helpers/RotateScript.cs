using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour {

    public float RotateSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.back, RotateSpeed * Time.deltaTime);
    }
}