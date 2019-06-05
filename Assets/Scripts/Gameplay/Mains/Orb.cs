using System.Collections;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite plasmaSprite;
    [SerializeField]
    private Sprite fireSprite;
    [SerializeField]
    private Sprite electricitySprite;

    private new Rigidbody2D rigidbody2D;

    public TrailRenderer plasmaTrail;
    public TrailRenderer fireTrail;
    public TrailRenderer lightningTrail;

    public bool turnedToPlasma = false;
    public bool turnedToFire = false;
    public bool turnedToElectricity = false;

    public float moveSpeed = 10;
    public bool Fake = false;
    public bool Catchable = false;

    private MainController main;
    private EffectController effects;

    GravityHelper gravityHelper;
    public CircleCollider2D circleCollider;

    private void Awake()
    {
        main = FindObjectOfType<MainController>();
        effects = FindObjectOfType<EffectController>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        gravityHelper = GetComponent<GravityHelper> ();
        transform.localScale *= Camera.main.aspect / (9.0f / 16.0f);
        circleCollider = GetComponent<CircleCollider2D> ();
    }

    private void OnEnable()
    {
        rigidbody2D.gravityScale = 0f;
        Catchable = false;
        StartCoroutine(StartCollider());
    }

    private IEnumerator StartCollider()
    {
        yield return new WaitForSeconds(0.005f);
        circleCollider.enabled = true;
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = rigidbody2D.velocity.normalized * moveSpeed;
    }

    private void OnCollisionEnter2D (Collision2D collision) {

        if (!collision.gameObject.name.Equals ("Orb Catcher")) {
            Catchable = true;
        }
        if (main.BlackOUT)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                effects.BlindCollision(collision.transform.position);
            }
            else if (collision.gameObject.name.Equals("MiniWall(Clone)") || collision.gameObject.name.Equals("MiniWallRandomRotation(Clone)") || collision.gameObject.name.Equals("RotatingMiniWall(Clone)")
                || collision.gameObject.name.Equals("Downer(Clone)") || collision.gameObject.name.Equals("GrowingDowner(Clone)") || collision.gameObject.name.Equals("Ooze(Clone)"))
            {
                effects.BlindCollision(collision.transform.position);
            }
        }

        moveSpeed += 0.2f;
    }

    public void Plasma(bool state) {
        turnedToPlasma = state;
        plasmaTrail.enabled = state;
        spriteRenderer.sprite = state ? plasmaSprite : defaultSprite;
    }

    public void Fire (bool state) {
        turnedToFire = state;
        fireTrail.enabled = state;
        spriteRenderer.sprite = state ? fireSprite : defaultSprite;
    }

    public void Electricity (bool state) {
        turnedToElectricity = state;
        lightningTrail.enabled = state;
        spriteRenderer.sprite = state ? electricitySprite : defaultSprite;
    }

    public void SlowDown(bool shouldSlow = true) {
        if (shouldSlow) {
            moveSpeed -= (moveSpeed / 2);
            gravityHelper.timeMultiplier = (13f / moveSpeed);
        } else {
            moveSpeed = 13;
            gravityHelper.timeMultiplier = 1;
        }
    }
}