using UnityEngine;

public class FastForward : MonoBehaviour {

    public float MaxDoubleTapTime = 0.5f;
    public float multiplicator = 2f;

    int TapCount;
    float NewTime;

    private float timeScale;

    private bool isFastForward = false;

    private MainController mainController;

    void Start ()
    {
        mainController = FindObjectOfType<MainController>();
        TapCount = 0;
    }

    void Update () {
        if (mainController.launcher.orbsReady != mainController.launcher.orbs.Count)
        {
            //touches
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    TapCount += 1;
                }

                if (TapCount == 1)
                {
                    NewTime = Time.time + MaxDoubleTapTime;
                }
                else if (TapCount == 2 && Time.time <= NewTime)
                {
                    Debug.Log("Double tap");

                    if (!isFastForward)
                    {
                        timeScale = Time.timeScale;
                    }

                    Time.timeScale *= multiplicator;

                    isFastForward = true;
                    TapCount = 0;
                }

            }

            //clicks
            if (Input.GetMouseButtonUp(0))
            {
                TapCount += 1;

                if (TapCount == 1) {
                    NewTime = Time.time + MaxDoubleTapTime;
                } else if (TapCount == 2 && Time.time <= NewTime) {

                    //Debug.Log ("Double tap");

                    if (!isFastForward) {
                        timeScale = Time.timeScale;
                    }

                    Time.timeScale *= multiplicator;

                    isFastForward = true;
                    TapCount = 0;
                }
        }

            if (Time.time > NewTime)
            {
                TapCount = 0;
            }
        }
        else
        {
            if (isFastForward)
            {
                Time.timeScale = timeScale;
                isFastForward = false;
            }
        }
    }
}