using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrbLauncher : MonoBehaviour
{
    [SerializeField]
    private MainController main;
    [SerializeField]
    private VisualOrb visOrb;

    [SerializeField]
    private Transform orbParent;

    private Vector3 startDragPosition;
    private Vector3 endDragPosition;
    private Spawner spawner;
    public LaunchPreview launchPreview;
    private LineRenderer launchLine;
    public List<Orb> orbs = new List<Orb>();
    public int orbsReady;
    private Vector3 startPosThisRound;
    public bool Launching;

    private bool threeShot;
    private PowerUpsController powerUpsController;

    [SerializeField]
    private Orb orbPrefab;

    [SerializeField]
    private Animator powerUpsPanelAnimator;

    Vector3 launchDirection;
    private Orb extraOrb1, extraOrb2;
    const string POWERUPS_BOOL_NAME = "ShowPowerUps";

    private StatusController status;

    public delegate void OrbLauncherAction ();
    public static event OrbLauncherAction OrbNumberChanged;

    private void Awake()
    {
        powerUpsController = FindObjectOfType<PowerUpsController> ();
        spawner = FindObjectOfType<Spawner>();
        status = FindObjectOfType<StatusController>();
        launchLine = launchPreview.GetComponent<LineRenderer>();
        startPosThisRound = transform.position;
        CreateOrb();
    }

    public void SetStartPos(Vector3 pos)
    {
        startPosThisRound = pos;
    }

    public void ReturnOrb()
    {
        orbsReady++;
        if (orbsReady == orbs.Count && !Launching)
        {
            spawner.SpawnRow();
            main.EndRound();
            powerUpsPanelAnimator.SetBool (POWERUPS_BOOL_NAME, true);
        }
    }

    public void CreateOrb()
    {
        var orb = Instantiate(orbPrefab, orbParent);
        orbs.Add(orb);
        orb.gameObject.SetActive(false);
        orbsReady++;
        if (OrbNumberChanged != null) {
            OrbNumberChanged ();
        }
    }

    private void Update()
    {
        if (IsPointerOverUIObject ()) {
            return;
        }

        //DebugMe();

        if (main.paused)
            return;

        if (main.CheckForPause())
            main.Pause();

        if (orbsReady != orbs.Count) // don't let the player launch until all orbs are back.
            return;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

        if (!main.ForwardAim)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartDragInverted(Camera.main.ScreenToWorldPoint (Input.mousePosition));
            }
            else if (Input.GetMouseButton(0))
            {
                ContinueDragInverted(Camera.main.ScreenToWorldPoint (Input.mousePosition));
            }
            else if (Input.GetMouseButtonUp(0))
            {
                EndDrag();
            }
        }
        if (main.ForwardAim)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartDrag(Camera.main.ScreenToWorldPoint (Input.mousePosition));
            }
            else if (Input.GetMouseButton(0))
            {
                ContinueDrag(Camera.main.ScreenToWorldPoint (Input.mousePosition));
            }
            else if (Input.GetMouseButtonUp(0))
            {
                EndDrag();
            }
        }

#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

        if(Input.touchCount > 0) 
        {
            Vector3 worldPos2 = Camera.main.ScreenToWorldPoint((Vector3)Input.GetTouch(0).position);

            if (!main.ForwardAim)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    StartDragInverted(worldPos2);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    ContinueDragInverted(worldPos2);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    EndDrag();
                }
            }
            if (main.ForwardAim)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    StartDrag(worldPos2);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    ContinueDrag(worldPos2);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    EndDrag();
                }
            }
        }
        #endif
    }

    private bool IsPointerOverUIObject () {
        PointerEventData eventDataCurrentPosition = new PointerEventData (EventSystem.current);
        eventDataCurrentPosition.position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult> ();
        EventSystem.current.RaycastAll (eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    private IEnumerator LaunchOrbs()
    {
        if (!main.ForwardAim)
        {
            launchDirection = endDragPosition - startDragPosition;
        }
        if (main.ForwardAim)
        {
            launchDirection = endDragPosition - launchPreview.dragStartPoint;
        }

        launchDirection.Normalize ();
        launchDirection = new Vector3 (launchDirection.x, Mathf.Clamp (launchDirection.y, 0f, 1f), launchDirection.z);

        CheckPowerups ();

        var shootys = orbs.ToArray();
        var posStart = transform.position;

        if (Launching)
        {
            foreach(var orb in shootys)
            {
                if (orb != null)
                {
                    orbsReady--;
                    orb.transform.position = posStart;
                    orb.gameObject.SetActive(true);
               
                    Rigidbody2D orbRigidbody2D = orb.GetComponent<Rigidbody2D> ();

                    orbRigidbody2D.gravityScale = 0;
                    status.DecreaseShots();

                    if (threeShot)
                    {
                        extraOrb1 = Instantiate(orbPrefab, orb.transform.position, Quaternion.identity);
                        extraOrb2 = Instantiate(orbPrefab, orb.transform.position, Quaternion.identity);
                        
                        extraOrb1.Fake = true;
                        extraOrb2.Fake = true;
                    }

                    if (!main.ForwardAim)
                    {
                        orbRigidbody2D.AddForce(-launchDirection);
                        if (threeShot)
                        {
                            extraOrb1.GetComponent<Rigidbody2D>().AddForce(-launchPreview.extra1);
                            extraOrb2.GetComponent<Rigidbody2D>().AddForce(-launchPreview.extra2);
                        }
                    }
                    if (main.ForwardAim)
                    {
                        orbRigidbody2D.AddForce(launchDirection);
                        if (threeShot)
                        {
                            extraOrb1.GetComponent<Rigidbody2D>().AddForce(launchPreview.extra1);
                            extraOrb2.GetComponent<Rigidbody2D>().AddForce(launchPreview.extra2);
                        }
                    }

                    yield return new WaitForSeconds(0.09f);

                    if (threeShot)
                    {
                        extraOrb1.GetComponent<CircleCollider2D>().enabled = true;
                        extraOrb2.GetComponent<CircleCollider2D>().enabled = true;
                    }
                }
            }
        }

        Launching = false;

        visOrb.Plasma (false);
        visOrb.Fire (false);
        threeShot = false;
        visOrb.Electricity (false);
    }

    public void AutoLaunch()
    {
        float dir = Random.Range(0, 2).Equals(1) ? 1f : -1f;
        float x = dir;
        float y = main.ForwardAim ? Random.Range (0.1f, 0.13f) : Random.Range (-0.13f, -0.1f);
        float z = main.ForwardAim ? -1f : 0f;
        
        endDragPosition = new Vector3(x, y, z); //direction if below is zero
        startDragPosition = Vector3.zero;
        launchPreview.dragStartPoint = Vector3.zero;
        StartCoroutine (LaunchOrbs ());
    }

    public void CheckPowerups()
    {
        if (!Launching)
        {
            if (powerUpsController.PlasmaOn) {
                for (int i = 0; i < orbs.Count; i++) {
                    orbs[i].Plasma (true);
                }
                powerUpsController.PlasmaOn = false;
            }

            if (powerUpsController.FireOn) {
                for (int i = 0; i < orbs.Count; i++) {
                    orbs[i].Fire (true);
                }
                powerUpsController.FireOn = false;
            }

            if (powerUpsController.TripleShotOn) {
                threeShot = true;
                powerUpsController.TripleShotOn = false;
            }

            if (powerUpsController.ElectricityOn) {
                for (int i = 0; i < orbs.Count; i++) {
                    orbs[i].Electricity (true);
                }
                powerUpsController.ElectricityOn = false;
            }
        }

        Launching = true;
    }

    private void ContinueDrag(Vector3 worldPosition)
    {       
        launchPreview.SetEndPoint(worldPosition);
        endDragPosition = worldPosition;
    }

    private void StartDrag(Vector3 worldPosition)
    {
        powerUpsPanelAnimator.SetBool (POWERUPS_BOOL_NAME, false);
        launchPreview.SetOnOff(true);
        startDragPosition = worldPosition;
        launchPreview.SetStartPoint(transform.position);
    }

    private void EndDrag()
    {
        launchPreview.SetOnOff(false);
        StartCoroutine(LaunchOrbs());
    }

    private void ContinueDragInverted(Vector3 worldPosition)
    {
        endDragPosition = worldPosition;
        Vector3 direction = endDragPosition - startDragPosition;
        launchPreview.SetEndPoint(transform.position - direction);
    }

    private void StartDragInverted(Vector3 worldPosition)
    {
        launchPreview.SetOnOff(true);
        startDragPosition = worldPosition;
        launchPreview.SetStartPoint(transform.position);
    }

    private void DebugMe()
    {
        Debug.Log("orblauncher 3shot: " + threeShot);
        Debug.Log("Powerups 3shot: " + powerUpsController.TripleShotOn);
    }
}