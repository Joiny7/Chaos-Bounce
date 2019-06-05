using UnityEngine;

public class LaunchPreview : MonoBehaviour
{
    public SpriteRenderer SpriteRendererMain;
    public SpriteRenderer SpriteRendererLeft;
    public SpriteRenderer SpriteRendererRight;

    [SerializeField] SpriteRenderer aim;

    public Vector3 dragStartPoint;
    public bool ThreeShot;
    public float xOffset;
    public Vector2 extra1;
    public Vector2 extra2;

    private void Awake()
    {
        SpriteRendererMain.enabled = false;
        aim.enabled = false;
        SetExtrasEnabled (false);
    }

    public void SetStartPoint(Vector3 worldPoint)
    {
        dragStartPoint = worldPoint;
    }

    public void SetEndPoint(Vector3 worldPoint)
    {
        RotateAroundTowardsPoint (transform, worldPoint);

        Vector3 pointOffset = worldPoint - dragStartPoint;
        Vector3 endPoint = transform.position + pointOffset;
        Vector3 extension = endPoint + pointOffset;
        extension.z = 0;

        if (ThreeShot)
            SetExtraEndPoints(extension);
    }

    void RotateAroundTowardsPoint(Transform entity, Vector3 target) {
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2 (target.y - entity.position.y, target.x - entity.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        AngleDeg -= 90f;

        if(AngleDeg <= -180f) {
            AngleDeg = 90f;
        } else if(AngleDeg <= -90f) {
            AngleDeg = -90f;
        }

        // Rotate Object
        entity.rotation = Quaternion.Euler (0, 0, AngleDeg);
    }

    public void SetExtraEndPoints(Vector3 endPoint)
    {
        Vector3 extraEnd1 = endPoint;
        Vector3 extraEnd2 = endPoint;
        extraEnd1.x += xOffset;
        extraEnd2.x -= xOffset;

        extra1 = extraEnd1 - transform.position;
        extra2 = extraEnd2 - transform.position;

        RotateAroundTowardsPoint (SpriteRendererLeft.transform, extraEnd1);
        RotateAroundTowardsPoint (SpriteRendererRight.transform, extraEnd2);
    }

    public void SetExtrasEnabled(bool condition)
    {
        SpriteRendererLeft.enabled = condition;
        SpriteRendererRight.enabled = condition;

        ThreeShot = condition;
    }

    public void SetOnOff(bool condition)
    {
        SpriteRendererMain.enabled = condition;
        aim.enabled = condition;

        if (ThreeShot)
            SetExtrasEnabled(condition);
    }
}