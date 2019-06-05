using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectController : MonoBehaviour {

    [SerializeField]
    private MainController main;
    [SerializeField]
    private GameObject smallExplosion;
    [SerializeField]
    private GameObject plasmaExplosion;
    [SerializeField]
    private GameObject contactSpark;
    [SerializeField]
    private GameObject mapBomb;
    [SerializeField]
    private GameObject darknessSpark;
    [SerializeField]
    private GameObject darknessSparkBig;
    [SerializeField]
    private GameObject dissolveEffect;
    [SerializeField]
    private GameObject doublerEffect;
    [SerializeField]
    private GameObject tenfolderEffect;
    [SerializeField]
    private GameObject zapEffect;
    [SerializeField]
    private GameObject atmosphereEffect;
    [SerializeField]
    private GameObject tinyFreeze;
    [SerializeField]
    private GameObject tinyMetal;
    [SerializeField]
    private GameObject blindedCollisionEffect;
    [SerializeField]
    private GameObject GameOver;

    public void PlasmaDeath(Vector3 pos)
    {
        var pXplosion = Instantiate(plasmaExplosion, pos, Quaternion.identity);
    }

    public void StartGameOver()
    {
        GameOver.SetActive(true);
        PlayerData data = SaveSystem.LoadPlayer ();
        SaveSystem.SavePlayer (main.highScore, 0, null, main.visualOrbEdge.transform.position.x, main.visualOrbEdge.transform.position.y, null, 1, data.playerName, data.userId);
        Time.timeScale = 0;
    }

    public void SmallBoom(Vector3 pos)
    {
        var Xplosion = Instantiate(smallExplosion, pos, Quaternion.identity);
    }

    public void TinyFreeze(Vector3 pos)
    {
        var freeze = Instantiate(tinyFreeze, pos, Quaternion.identity);
    }

    public void BlindCollision(Vector3 pos)
    {
        var blind = Instantiate(blindedCollisionEffect, pos, Quaternion.identity);
    }

    public void TinyMetal(Vector3 pos)
    {
        var met = Instantiate(tinyMetal, pos, Quaternion.identity);
    }

    public void SmallSpark(Vector3 pos)
    {
        var spark = Instantiate(contactSpark, pos, Quaternion.identity);
    }

    public void DoublerEffect(Vector3 pos)
    {
        var doubler = Instantiate(doublerEffect, pos, Quaternion.identity);
    }

    public void BurnInAtmosphereEffect(Vector3 pos)
    {
        var atmos = Instantiate(atmosphereEffect, pos, Quaternion.identity);
    }

    public void TenfolderEffect(Vector3 pos)
    {
        var tenfolder = Instantiate(tenfolderEffect, pos, Quaternion.identity);
    }

    public void MapBoom()
    {
        var boom = Instantiate(mapBomb, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void Dissolve(Vector3 pos)
    {
        var hiss = Instantiate(dissolveEffect, pos, Quaternion.identity);
    }

    public void LineBoom(List<Vector3> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            SmallBoom(positions[i]);
        }
    }

    public void DarkSpark(Vector3 pos)
    {
        var dark = Instantiate(darknessSpark, pos, Quaternion.identity);
    }

    public void DarkSparkBig(Vector3 pos)
    {
        var dark = Instantiate(darknessSparkBig, pos, Quaternion.identity);
    }

    public void ZapEffect(Vector3 pos)
    {
        var zap = Instantiate(zapEffect, pos, Quaternion.identity);
    }
}