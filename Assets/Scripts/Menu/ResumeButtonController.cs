using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButtonController : MonoBehaviour {

	void Start () {
        if(!SaveSystem.GameCanBeResumed()) {
            gameObject.SetActive (false);
        }
	}
}
