using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundController : MonoBehaviour {

    public AudioSource source;
    public AudioClip[] blockSounds;
    public AudioClip[] destroyBlocks;
    public GameObject soundParent;

#region PowerUp Prefabs
    public GameObject collectOrbSoundPrefab;
    public GameObject lineKillerSoundPrefab;
    public GameObject tripleShotSoundPrefab;
    public GameObject fireSoundPrefab;
    public GameObject plasmaSoundPrefab;
#endregion
#region Randomizers Prefabs
    public GameObject flippyFloopSoundPrefab;
    public GameObject growerSoundPrefab;
    public GameObject shrinkerSoundPrefab;
    public GameObject slowySoundPrefab;
    public GameObject speedySoundPrefab;
    public GameObject rotatorSoundPrefab;
    public GameObject rotatorPlusSoundPrefab;
#endregion
#region Debris Prefabs
    public GameObject blackOutOnSoundPrefab;
    public GameObject blackOutOffSoundPrefab;
    public GameObject DoublerSoundPrefab;
    public GameObject DownerSoundPrefab;
    public GameObject growingdownerSoundPrefab;
    public GameObject oozeSoundPrefab;
    public GameObject tenfolderSoundPrefab;
#endregion
#region Other prefabs
    public GameObject pentragramHitSoundPrefab;
    public GameObject pentragramDestroySoundPrefab;
#endregion
#region SpawnMethods
    public void PlayCollectOrbEffect()
    {
        var coll = Instantiate(collectOrbSoundPrefab);
        coll.transform.SetParent(soundParent.transform);
    }

    public void LineKillerEffect()
    {
        var lK = Instantiate(lineKillerSoundPrefab);
        lK.transform.SetParent(soundParent.transform);
    }

    public void TripleShotEffect()
    {
        var tS = Instantiate(tripleShotSoundPrefab);
        tS.transform.SetParent(soundParent.transform);
    }

    public void FireEffect()
    {
        var fire = Instantiate(fireSoundPrefab);
        fire.transform.SetParent(soundParent.transform);
    }

    public void PlasmaEffect()
    {
        var plasma = Instantiate(plasmaSoundPrefab);
        plasma.transform.SetParent(soundParent.transform);
    }

    public void FlippyEffect()
    {
        var flip = Instantiate(flippyFloopSoundPrefab);
        flip.transform.SetParent(soundParent.transform);
    }

    public void GrowerEffect()
    {
        var grow = Instantiate(growerSoundPrefab);
        grow.transform.SetParent(soundParent.transform);
    }

    public void ShrinkerEffect()
    {
        var shrink = Instantiate(shrinkerSoundPrefab);
        shrink.transform.SetParent(soundParent.transform);
    }

    public void SlowyEffect()
    {
        var slow = Instantiate(slowySoundPrefab);
        slow.transform.SetParent(soundParent.transform);
    }

    public void SpeedyEffect()
    {
        var speed = Instantiate(speedySoundPrefab);
        speed.transform.SetParent(soundParent.transform);
    }

    public void RotatorEffect()
    {
        var rot = Instantiate(rotatorSoundPrefab);
        rot.transform.SetParent(soundParent.transform);
    }

    public void RotatorPlusEffect()
    {
        var rotPlus = Instantiate(rotatorPlusSoundPrefab);
        rotPlus.transform.SetParent(soundParent.transform);
    }

    public void BlackOutOnEffect()
    {
        var blackOn = Instantiate(blackOutOnSoundPrefab);
        blackOn.transform.SetParent(soundParent.transform);
    }

    public void BlackOutOffEffect()
    {
        var blackOff = Instantiate(blackOutOffSoundPrefab);
        blackOff.transform.SetParent(soundParent.transform);
    }

    public void DoublerEffect()
    {
        var doubler = Instantiate(DoublerSoundPrefab);
        doubler.transform.SetParent(soundParent.transform);
    }

    public void DownerEffect()
    {
        var down = Instantiate(DownerSoundPrefab);
        down.transform.SetParent(soundParent.transform);
    }

    public void GrowingDownerEffect()
    {
        var growDown = Instantiate(growingdownerSoundPrefab);
        growDown.transform.SetParent(soundParent.transform);
    }

    public void OozeEffect()
    {
        var ooze = Instantiate(oozeSoundPrefab);
        ooze.transform.SetParent(soundParent.transform);
    }

    public void TenfolderEffect()
    {
        var ten = Instantiate(tenfolderSoundPrefab);
        ten.transform.SetParent(soundParent.transform);
    }

    public void PlayDestroyPentagram()
    {
        var pen = Instantiate(pentragramDestroySoundPrefab);
        pen.transform.SetParent(soundParent.transform);
    }

    public void PlayHitPentagram()
    {
        var pen = Instantiate(pentragramHitSoundPrefab);
        pen.transform.SetParent(soundParent.transform);
    }

    public void DestroyBlock()
    {
        if (source)
        {
            int sound = Random.Range(0, 3);
            source.PlayOneShot(destroyBlocks[sound]);
        }
    }
#endregion
}