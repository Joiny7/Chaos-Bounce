using UnityEngine;

public enum DamageReductionState
{
    Normal = 0,
    Ice = 1,
    Metal = 2
}

public class BaseObject : MonoBehaviour {
    public int hitsRemaining = 1;
    public DamageReductionState state = DamageReductionState.Normal;

    Transform parent;

    void Awake()
    {
        transform.localScale *= Camera.main.aspect / (9.0f / 16.0f);
    }
}