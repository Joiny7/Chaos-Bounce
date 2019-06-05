using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Metal
public class Tenfolder : BaseObject {

    private Spawner spawner;
    private GameSoundController gameSound;
    EffectController effectController;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            Tenfold();
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
            Instantiate (Resources.Load ("DestroyEffect/" + Enum.GetName (typeof (DamageReductionState), state), typeof (GameObject)), transform.position, transform.rotation);
        }
        spawner.allDebris.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    private void Tenfold()
    {
        spawner.MetalPicked ();

        var allAffected = spawner.allEnemies;

        for (int i = 0; i < allAffected.Count; i++)
        {
            var obj = allAffected[i].gameObject;
            TenfoldHits(obj);
            effectController.TinyMetal(obj.transform.position);
        }

        gameSound.TenfolderEffect();
        FindObjectOfType<EffectController>().TenfolderEffect(gameObject.transform.position);
        FindObjectOfType<StatusController>().UpdateHistory("Coated in metal!", Color.grey);
    }

    private void TenfoldHits(GameObject obj)
    {
        if (obj != null)
        {
            if (obj.GetComponent<Brick>() != null)
            {
                var brick = obj.GetComponent<Brick>();
                brick.MetalizeBlock();
            }
            if (obj.GetComponent<Ball>() != null)
            {
                var ball = obj.GetComponent<Ball>();
                ball.MetalizeBlock();
            }
            if (obj.GetComponent<Hex>() != null)
            {
                var hex = obj.GetComponent<Hex>();
                hex.MetalizeBlock();
            }
            if (obj.GetComponent<Pyramid>() != null)
            {
                var pyr = obj.GetComponent<Pyramid>();
                pyr.MetalizeBlock();
            }
            if (obj.GetComponent<Rhombus>() != null)
            {
                var rh = obj.GetComponent<Rhombus>();
                rh.MetalizeBlock();
            }
            if (obj.GetComponent<RotatingPyramid>() != null)
            {
                var rot = obj.GetComponent<RotatingPyramid>();
                rot.MetalizeBlock();
            }
            if (obj.GetComponent<Pentagram>() != null)
            {
                var pent = obj.GetComponent<Pentagram>();
                pent.MetalizeBlock();
            }
            if (obj.GetComponent<GrowingBrick>() != null)
            {
                var pent = obj.GetComponent<GrowingBrick>();
                pent.MetalizeBlock();
            }
        }
    }
}