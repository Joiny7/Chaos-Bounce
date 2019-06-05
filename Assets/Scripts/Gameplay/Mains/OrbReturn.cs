using UnityEngine;

public class OrbReturn : MonoBehaviour
{
    private OrbLauncher OrbLauncher;
    public Transform visualOrb;
    public MainController main;
    private Vector3 orbStandardSize = new Vector3(0.07f, 0.07f, 0.08f);
    public Spawner spawner;
    private PowerUpsController powerUpsController;
    public GameObject minYValue;
    public GameObject minXValue;
    public GameObject maxXValue;

    private void Awake()
    {
        powerUpsController = FindObjectOfType<PowerUpsController> ();
        OrbLauncher = FindObjectOfType<OrbLauncher>();
    }

    private void OnTriggerEnter2D (Collider2D col) {

        Orb orb = col.GetComponent<Orb> ();
        GameObject go = col.gameObject;

        if (col.CompareTag ("Debris"))
        {
            spawner.allDebris.Remove (go);
            Destroy (go);
        } 
        else if (orb != null)
        {
            if (!orb.Catchable)
            {
                return;
            }

            if(orb.Fake) {
                Destroy (go);
                return;
            }

            var pos1 = col.transform.position;
            var pos = CorrectPosition(pos1);

            EndPowers(orb);
            OrbLauncher.ReturnOrb();
            go.SetActive(false);
            visualOrb.position = pos;
            OrbLauncher.transform.position = pos;
            orb.circleCollider.enabled = false;
        }
    }

    public void OutOfBoundsReturn(Orb orb)
    {
        EndPowers(orb);
        OrbLauncher.ReturnOrb();
        orb.GetComponent<CircleCollider2D>().enabled = false;
        orb.transform.position = visualOrb.position;
        orb.gameObject.SetActive(false);
    }

    private Vector3 CorrectPosition(Vector3 initPos)
    {
        var pos = initPos;

        if (pos.y < minYValue.transform.position.y)
        {
            pos = new Vector3(pos.x, minYValue.transform.position.y);
        }
        if (pos.x < minXValue.transform.position.x)
        {
            pos = new Vector3(minXValue.transform.position.x, pos.y);
        }
        if (pos.x > maxXValue.transform.position.x)
        {
            pos = new Vector3(maxXValue.transform.position.x, pos.y);
        }

        return pos;
    }

    private void EndPowers(Orb orb)
    {
        orb.Fire(false);
        orb.Plasma(false);
        orb.Electricity (false);
        orb.transform.localScale = orbStandardSize;
        orb.SlowDown (false);
    }
}