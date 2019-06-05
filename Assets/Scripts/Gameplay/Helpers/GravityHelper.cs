using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHelper : MonoBehaviour
{
    private const float TimeToCheck = 2f;
    public Rigidbody2D body;
    private Vector3? lastWallHit;
    private Vector3? previousWallHit;
    private float timer;
    private float outMinX;
    private float outMaxX;
    private float outMinY;
    private float outMaxY;

    public float timeMultiplier = 1f;

    MainController mainController;

    private void OnEnable()
    {
        timer = 0;
        lastWallHit = null;
        previousWallHit = null;

        if(mainController == null) {
            mainController = FindObjectOfType<MainController> ();
        }

        outMinX = mainController.LeftWall.transform.position.x;
        outMaxX = mainController.RightWall.transform.position.x;
        outMaxY = mainController.TopWall.transform.position.y;
        outMinY = mainController.BottomWall.transform.position.y;
    }

    void Update()
    {
        yCheck();
        xCheck();
        OutOfBoundsCheck();
    }

    private void OutOfBoundsCheck()
    {
        if (transform.position.x < outMinX || transform.position.x > outMaxX || transform.position.y > outMaxY || transform.position.y < outMinY)
        {
            FindObjectOfType<OrbReturn>().OutOfBoundsReturn(GetComponent<Orb>());
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var yValueString = transform.position.y.ToString("#.#");
        var xValueString = transform.position.x.ToString("#.#");
        float yValue;
        float.TryParse(yValueString, out yValue);
        float xValue;
        float.TryParse(xValueString, out xValue);

        if (col.gameObject.CompareTag("Wall"))
        {
            previousWallHit = lastWallHit;
            //lastWallHit = gameObject.transform.position;
            lastWallHit = new Vector3(xValue, yValue);
        }
        else if (col.gameObject.GetComponent<Downer>() || col.gameObject.GetComponent<GrowingDowner>())
        {
            previousWallHit = lastWallHit;
            //lastWallHit = gameObject.transform.position;
            lastWallHit = new Vector3(xValue, yValue);
        }
        else
        {
            previousWallHit = null;
            timer = 0;
        }
    }

    private void yCheck()
    {
        if (lastWallHit.GetValueOrDefault().y == previousWallHit.GetValueOrDefault().y)
        {
            timer += Time.deltaTime;

            if (timer >= TimeToCheck * timeMultiplier)
            {
                Gravity();
            }
        }
    }

    private void xCheck()
    {
        if (lastWallHit.GetValueOrDefault().x == previousWallHit.GetValueOrDefault().x)
        {
            timer += Time.deltaTime;

            if (timer >= TimeToCheck * timeMultiplier)
            {
                Nudge();
            }
        }
    }

    private void Nudge()
    {
        if (transform.position.x > 0f) {
            body.AddForce (Vector2.left * 6);
        } else {
            body.AddForce (Vector2.right * 6);
        }
    }

    private void Gravity()
    {
        body.gravityScale = 0.2f;
    }
}