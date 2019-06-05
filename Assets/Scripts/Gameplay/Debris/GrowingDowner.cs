using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingDowner : BaseObject {

    private Vector3 GrowFactor = new Vector3(0.05f, 0.05f);
    private Spawner spawner;
    private EffectController effects;
    private float distanceBetweenObjects = 0.65f;
    private int timesGrown = 1;
    private GameSoundController gameSound;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        effects = FindObjectOfType<EffectController>();
        gameSound = FindObjectOfType<GameSoundController>();
    }

    private bool isQuitting = false;

    void OnApplicationQuit () {
        isQuitting = true;
    }

    private void OnDestroy () {
        if (!isQuitting) {
            Instantiate (Resources.Load ("DestroyEffect/" + System.Enum.GetName (typeof (DamageReductionState), state), typeof (GameObject)), transform.position, transform.rotation);
        }
        spawner.allDebris.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    public void Grow()
    {
        if (RoomForGrowth())
        {
            gameObject.transform.localScale += GrowFactor;
            timesGrown++;
        }
    }

    private bool RoomForGrowth()
    {
        var left = CheckLeft();
        var right = CheckRight();
        var rayDown = Physics2D.Raycast(transform.position, Vector2.down, (distanceBetweenObjects * timesGrown));
        var rayUp = Physics2D.Raycast(transform.position, Vector2.up, (distanceBetweenObjects * timesGrown));

        if (left && right && !rayDown && !rayUp)
        {
            return true;
        }

        return false;
    }

    private bool CheckLeft()
    {
        if (gameObject != null)
        {
            if (gameObject.transform.position.x <= -2.25f)
            {
                return true;
            }
            else
            {
                var rayLeft = Physics2D.Raycast(transform.position, Vector2.left, (distanceBetweenObjects * timesGrown));
                if (rayLeft)
                    return false;

                else
                    return true;
            }
        }
        else
        {
            return false;
        }
    }

    private bool CheckRight()
    {
        if (gameObject != null)
        {
            if (gameObject.transform.position.x >= 2.29f)
            {
                return true;
            }
            else
            {
                var rayRight = Physics2D.Raycast(transform.position, Vector2.right, (distanceBetweenObjects * timesGrown));
                if (rayRight)
                    return false;

                else
                    return true;
            }
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("LoserLine"))
        {
            Destroy(gameObject);
        }
        if (coll.CompareTag("Debris"))
        {
            Destroy(coll.gameObject);
        }
        if (coll.CompareTag("Ball"))
        {
            if (coll.gameObject.GetComponent<Orb>().turnedToPlasma)
            {
                effects.PlasmaDeath(gameObject.transform.position);
                Destroy(gameObject);
            }
            else if (coll.gameObject.GetComponent<Orb>().turnedToFire)
            {
                effects.SmallSpark(gameObject.transform.position);
                return;
            }
            else
            {
                gameSound.GrowingDownerEffect();
                coll.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                coll.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
            }
        }
    }
}