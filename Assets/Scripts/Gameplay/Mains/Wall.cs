using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallType {
	Left,
	Right,
	Top
}

public class Wall : MonoBehaviour {
	public WallType type;

	void Awake() {
		Vector3 rightTopScreenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		switch (type) {
			case WallType.Left:
				transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x, transform.position.y, transform.position.z);
				break;
			case WallType.Right:
				transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x, transform.position.y, transform.position.z);
				break;
			case WallType.Top:
				transform.position = new Vector3(transform.position.x, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.safeArea.max.y - Screen.height * 0.06f, 0)).y, transform.position.z);
				break;
			default:
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag("Debris")) {
			Destroy(col.gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag("Debris")) {
			Destroy(col.gameObject);
		}
	}
}