using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHelper : MonoBehaviour {

    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    public Transform spawnedObjectsParent;

    #region Enemies prefabs
    [Header ("Enemies prefabs")]
    [SerializeField]
    private Brick brickPrefab;
    [SerializeField]
    private CollectOrb collectPrefab;
    [SerializeField]
    private Pyramid pyramidPrefab;
    [SerializeField]
    private RotatingPyramid rotPyramidPrefab;
    [SerializeField]
    private Ball ballPrefab;
    [SerializeField]
    private Rhombus rhombusPrefab;
    [SerializeField]
    private Hex hexPrefab;
    [SerializeField]
    private GrowingBrick growBrickPrefab;
    [SerializeField]
    private Pentagram pentagramPrefab;
    [SerializeField]
    private Pyramid downPyramidPrefab;
    [SerializeField]
    private Joker jokerPrefab;
    #endregion
    #region Power-ups prefabs
    [Header ("Power-ups prefabs")]
    //Randomizers
    [SerializeField]
    private GameObject flippPrefab;
    [SerializeField]
    private GameObject growerPrefab;
    [SerializeField]
    private GameObject shrinkerPrefab;
    [SerializeField]
    private GameObject slowyPrefab;
    //Real power-ups
    [SerializeField]
    private GameObject plasmaPrefab;
    [SerializeField]
    private GameObject lineKillerPrefab;
    [SerializeField]
    private GameObject firePrefab;
    [SerializeField]
    private GameObject mapKillerPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private GameObject handGrenadePrefab;
    [SerializeField]
    private GameObject bigGrenadePrefab;
    [SerializeField]
    private GameObject electricityPrefab;
    #endregion
    #region Debris prefabs
    [Header ("Debris prefabs")]
    [SerializeField]
    private GameObject downerPrefab;
    [SerializeField]
    private GameObject doublerPrefab;
    [SerializeField]
    private GameObject tenfolderPrefab;
    [SerializeField]
    private GameObject blackOutPrefab;
    [SerializeField]
    private GameObject oozePrefab;
    [SerializeField]
    private GrowingDowner growingDownerPrefab;
    [SerializeField]
    private GameObject miniWallPrefab;
    [SerializeField]
    private GameObject rotMiniWallPrefab;
    #endregion
    #region Other prefabs
    [Header ("Other prefabs")]
    [SerializeField]
    private GameObject rotatorPrefab;
    [SerializeField]
    private GameObject rotatorPlusPrefab;
    #endregion
    #region Spawners
    public void SpawnBrick(Vector3 pos, DamageReductionState state = DamageReductionState.Normal, int? hitsRemaining = null)
    {
        var brick = Instantiate(brickPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        if(hitsRemaining.HasValue) {
            brick.SetHits (hitsRemaining.Value);
        } else {
            int hits = UnityEngine.Random.Range (1, 2) + spawner.rowsSpawned;
            brick.SetHits (hits);
        }

        brick.SetState(state);

        spawner.allEnemies.Add(brick.gameObject);
    }

    public void SpawnJoker(Vector3 pos, DamageReductionState state = DamageReductionState.Normal, int? hitsRemaining = null)
    {
        var joker = Instantiate(jokerPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        if (hitsRemaining.HasValue)
        {
            joker.SetHits(hitsRemaining.Value);
        }
        else
        {
            int hits = UnityEngine.Random.Range(1, 2) + spawner.rowsSpawned;
            joker.SetHits(hits);
        }

        joker.SetState (state);

        spawner.allEnemies.Add(joker.gameObject);
    }

    public void SpawnCollectOrb(Vector3 pos, DamageReductionState state = DamageReductionState.Normal)
    {
        var collect = Instantiate(collectPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.orbsSpawned.Add(collect.gameObject);
    }

    public void SpawnPyramid(Vector3 pos, DamageReductionState state = DamageReductionState.Normal, int? hitsRemaining = null)
    {
        var pyramid = Instantiate(pyramidPrefab, pos, new Quaternion(0, 0, 90, 90), spawnedObjectsParent);
        if (hitsRemaining.HasValue) {
            pyramid.SetHits (hitsRemaining.Value);
        } else {
            int hits = UnityEngine.Random.Range (1, 2) + spawner.rowsSpawned;
            pyramid.SetHits (hits); ;
        }

        pyramid.SetState (state);

        spawner.allEnemies.Add(pyramid.gameObject);
    }

    public void SpawnDownPyramid(Vector3 pos, DamageReductionState state = DamageReductionState.Normal, int? hitsRemaining = null)
    {
        var pyramid = Instantiate(downPyramidPrefab, pos, new Quaternion(0, 0, -90, 90), spawnedObjectsParent);
        if (hitsRemaining.HasValue)
        {
            pyramid.SetHits(hitsRemaining.Value);
        }
        else
        {
            int hits = UnityEngine.Random.Range(1, 2) + spawner.rowsSpawned;
            pyramid.SetHits(hits); ;
        }

        pyramid.SetState (state);

        spawner.allEnemies.Add(pyramid.gameObject);
    }

    public void SpawnRotPyramid(Vector3 pos, DamageReductionState state = DamageReductionState.Normal, int? hitsRemaining = null)
    {
        var rotPyramid = Instantiate(rotPyramidPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        if (hitsRemaining.HasValue) {
            rotPyramid.SetHits (hitsRemaining.Value);
        } else {
            int hits = UnityEngine.Random.Range (1, 2) + spawner.rowsSpawned;
            rotPyramid.SetHits (hits);
        }

        rotPyramid.SetState (state);

        spawner.allEnemies.Add(rotPyramid.gameObject);
    }

    public void SpawnRhombus(Vector3 pos, DamageReductionState state = DamageReductionState.Normal, int? hitsRemaining = null)
    {
        var rhomb = Instantiate(rhombusPrefab, pos, rhombusPrefab.transform.rotation, spawnedObjectsParent);
        if (hitsRemaining.HasValue) {
            rhomb.SetHits (hitsRemaining.Value);
        } else {
            int hits = UnityEngine.Random.Range (1, 2) + spawner.rowsSpawned;
            rhomb.SetHits (hits);
        }

        rhomb.SetState (state);

        spawner.allEnemies.Add(rhomb.gameObject);
    }

    public void SpawnBall(Vector3 pos, DamageReductionState state = DamageReductionState.Normal, int? hitsRemaining = null)
    {
        var ball = Instantiate(ballPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        if (hitsRemaining.HasValue) {
            ball.SetHits (hitsRemaining.Value);
        } else {
            int hits = UnityEngine.Random.Range (1, 2) + spawner.rowsSpawned;
            ball.SetHits (hits);
        }

        ball.SetState (state);

        spawner.allEnemies.Add(ball.gameObject);
    }

    public void SpawnFlippyFloop(Vector3 pos)
    {
        var flipp = Instantiate(flippPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add(flipp);
    }

    public void SpawnMiniWall(Vector3 pos)
    {
        var wall = Instantiate(miniWallPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.allDebris.Add(wall);
    }

    public void SpawnDowner(Vector3 pos)
    {
        var downer = Instantiate(downerPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.allDebris.Add(downer);
    }

    public void SpawnHex(Vector3 pos, DamageReductionState state = DamageReductionState.Normal, int? hitsRemaining = null)
    {
        var hex = Instantiate(hexPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        if (hitsRemaining.HasValue) {
            hex.SetHits (hitsRemaining.Value);
        } else {
            int hits = Random.Range (1, 2) + spawner.rowsSpawned;
            hex.SetHits (hits);
        }

        hex.SetState (state);

        spawner.allEnemies.Add(hex.gameObject);
    }

    public void SpawnPlasma(Vector3 pos)
    {
        var plasma = Instantiate(plasmaPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add(plasma);
    }

    public void SpawnDoubler(Vector3 pos)
    {
        var doubler = Instantiate(doublerPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.allDebris.Add(doubler);
    }

    public void SpawnRotMiniWall(Vector3 pos)
    {
        var wall = Instantiate(rotMiniWallPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.allDebris.Add(wall);
    }

    public void SpawnLineKiller(Vector3 pos)
    {
        var killer = Instantiate(lineKillerPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add(killer);
    }

    public void SpawnFire(Vector3 pos)
    {
        var fire = Instantiate(firePrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add(fire);
    }

    public void SpawnGrower(Vector3 pos)
    {
        var grower = Instantiate(growerPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add(grower);
    }

    public void SpawnShrinker(Vector3 pos)
    {
        var shrinker = Instantiate(shrinkerPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add(shrinker);
    }

    public void SpawnMapKiller(Vector3 pos)
    {
        var killer = Instantiate(mapKillerPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add(killer);
    }

    public void SpawnTripleShot(Vector3 pos)
    {
        var triple = Instantiate(tripleShotPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add(triple);
    }

    public void SpawnHandGrenade(Vector3 pos)
    {
        var gren = Instantiate(handGrenadePrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add(gren);
    }

    public void SpawnBigGrenade(Vector3 pos)
    {
        var gren = Instantiate(bigGrenadePrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add(gren);
    }

    public void SpawnElectricity(Vector3 pos)
    {
        var elec = Instantiate(electricityPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add(elec);
    }

    public void SpawnSlowy(Vector3 pos)
    {
        var slowy = Instantiate(slowyPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add(slowy);
    }

    public void SpawnGrowingBrick(Vector3 pos, DamageReductionState state = DamageReductionState.Normal, int? hitsRemaining = null)
    {
        var growBrick = Instantiate(growBrickPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        if (hitsRemaining.HasValue) {
            growBrick.SetHits (hitsRemaining.Value);
        } else {
            int hits = Random.Range (1, 2) + spawner.rowsSpawned;
            growBrick.SetHits (hits);
        }
        growBrick.roundSpawned = spawner.rowsSpawned;
        growBrick.SetState (state);
        spawner.allEnemies.Add(growBrick.gameObject);
        spawner.growers.Add(growBrick);
    }

    public void SpawnBlackOut(Vector3 pos)
    {
        var blackOut = Instantiate(blackOutPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.allDebris.Add(blackOut.gameObject);
    }

    public void SpawnOoze(Vector3 pos)
    {
        var ooze = Instantiate(oozePrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.allDebris.Add(ooze.gameObject);
    }

    public void SpawnTenfolder(Vector3 pos)
    {
        var ten = Instantiate(tenfolderPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.allDebris.Add(ten.gameObject);
    }

    public void SpawnGrowingDowner(Vector3 pos)
    {
        var growDown = Instantiate(growingDownerPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.allDebris.Add(growDown.gameObject);
        spawner.expanders.Add(growDown);
    }

    public void SpawnPentagram(Vector3 pos, DamageReductionState state = DamageReductionState.Normal)
    {
        var pen = Instantiate(pentagramPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        pen.state = state;
        spawner.allEnemies.Add(pen.gameObject);
    }

    public void SpawnRotator (Vector3 pos) {
        var pen = Instantiate (rotatorPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add (pen);
    }

    public void SpawnRotatorPlus (Vector3 pos) {
        var pen = Instantiate (rotatorPlusPrefab, pos, Quaternion.identity, spawnedObjectsParent);
        spawner.powerUps.Add (pen);
    }
    #endregion

    public void KillMap() {
        List<GameObject> KillUs = new List<GameObject> ();

        for (int i = 0; i < spawnedObjectsParent.childCount; i++) {
            GameObject Go = spawnedObjectsParent.GetChild (i).gameObject;
            KillUs.Add (Go);
        }

        foreach (GameObject g in KillUs) {
            if (g.CompareTag ("Debris")) {
                if (g.GetComponent<Ooze> ()) {
                    //break;
                }
                if (g.GetComponent<BlackOut> ()) {
                    g.GetComponent<BlackOut> ().StopMe ();
                    Destroy (g);
                } else
                    Destroy (g);
            } else if (g.CompareTag ("Enemy")) {
                if (g.GetComponent<Pentagram> ()) {
                    g.GetComponent<Pentagram> ().hitsRemaining -= 400;
                    //break;
                } else
                    Destroy (g);
            } else {
                Destroy (g);
            }
        }
    }
}