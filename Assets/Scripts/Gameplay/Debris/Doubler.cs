using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ice
public class Doubler : BaseObject {

    private Spawner spawner;
    private GameSoundController gameSound;
    public Color textColour;
    EffectController effectController;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            DoubleDown();
            effectController.DoublerEffect(gameObject.transform.position);
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        gameSound = FindObjectOfType<GameSoundController>();
        effectController = FindObjectOfType<EffectController> ();
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

    private void DoubleDown()
    {
        spawner.IcePicked ();

        gameSound.DoublerEffect();
        var allAffected = spawner.allEnemies;

        for (int i = 0; i < allAffected.Count; i++)
        {
            if (allAffected[i].GetComponent<BaseObject> ().state != DamageReductionState.Metal) {
                var obj = allAffected[i].gameObject;
                DoubleHits (obj);
                effectController.TinyFreeze (obj.transform.position);
            }
        }

        FindObjectOfType<StatusController>().UpdateHistory("Frozen over!", textColour);
    }

    private void DoubleHits(GameObject obj)
    {
        if (obj != null)
        {
            if (obj.GetComponent<Brick>() != null)
            {
                var brick = obj.GetComponent<Brick>();
                brick.FreezeBlock();
            }
            if (obj.GetComponent<Ball>() != null)
            {
                var ball = obj.GetComponent<Ball>();
                ball.FreezeBlock();
            }
            if (obj.GetComponent<Hex>() != null)
            {
                var hex = obj.GetComponent<Hex>();
                hex.FreezeBlock();
            }
            if (obj.GetComponent<Pyramid>() != null)
            {
                var pyr = obj.GetComponent<Pyramid>();
                pyr.FreezeBlock();
            }
            if (obj.GetComponent<Rhombus>() != null)
            {
                var rh = obj.GetComponent<Rhombus>();
                rh.FreezeBlock();
            }
            if (obj.GetComponent<RotatingPyramid>() != null)
            {
                var rot = obj.GetComponent<RotatingPyramid>();
                rot.FreezeBlock();
            }
            if (obj.GetComponent<GrowingBrick>() != null)
            {
                var gBrick = obj.GetComponent<GrowingBrick>();
                gBrick.FreezeBlock();
            }
            if (obj.GetComponent<Pentagram>() != null)
            {
                var pent = obj.GetComponent<Pentagram>();
                pent.FreezeBlock();
            }
            if (obj.GetComponent<GrowingBrick>() != null)
            {
                var pent = obj.GetComponent<GrowingBrick>();
                pent.MetalizeBlock();
            }
        }
    }
}