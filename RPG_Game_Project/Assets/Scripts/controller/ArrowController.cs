using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private int Damage;
    [SerializeField] private string TargetLayerName = "Player";


    [SerializeField] private float XVelocity;
    [SerializeField] private Rigidbody2D Rb;

    [SerializeField] private bool IsCanMove;
    [SerializeField] private bool IsFlipped;

    private CharacterStats stats;
    private int facingDir = 1;

    private void Update()
    {
        if (IsCanMove)
            Rb.velocity = new Vector2(XVelocity, Rb.velocity.y);

        if (facingDir == 1 && Rb.velocity.x < 0)
        {
            facingDir = -1;
            sr.flipX = true;
        }
    }

    public void SetupArrow(float _speed, CharacterStats _stats)
    {
        sr = GetComponent<SpriteRenderer>();
        XVelocity = _speed;
        stats = _stats;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterStats>()?.IsInvincible == true)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer(TargetLayerName))
        {

            //collision.GetComponent<CharacterStats>()?.TakeDamage(damage);


            stats.dodamage(collision.GetComponent<CharacterStats>());

            if (TargetLayerName == "Enemy")
                Destroy(gameObject);

            StuckInto(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            StuckInto(collision);
    }

    private void StuckInto(Collider2D collision)
    {
        GetComponentInChildren<ParticleSystem>().Stop();
        GetComponent<CapsuleCollider2D>().enabled = false;
        IsCanMove = false;
        Rb.isKinematic = true;
        Rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;

        Destroy(gameObject, Random.Range(5, 7));
    }

    public void FlipArrow()
    {
        if (IsFlipped)
            return;


        XVelocity = XVelocity * -1;
        IsFlipped = true;
        transform.Rotate(0, 180, 0);
        TargetLayerName = "Enemy";
    }
}
