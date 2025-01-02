using UnityEngine;

public class ShockStrikeController : MonoBehaviour
{
    [SerializeField] CharacterStats TargetStats;
    [SerializeField] float Speed;
    int Damage;

    Animator Anim;
    bool Triggered;
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
    }


    public void damage(int _damage, CharacterStats _targetstats)
    {
        TargetStats = _targetstats;
        Damage = _damage;
    }

    // Update is called once per frame
    void Update()
    {

        if (!TargetStats) return;

        if (Triggered) return;

        transform.position = Vector2.MoveTowards(transform.position, TargetStats.transform.position, Speed * Time.deltaTime);
        transform.right = transform.position - TargetStats.transform.position;

        if (Vector2.Distance(transform.position, TargetStats.transform.position) < .1f)
        {
            Anim.transform.localRotation = Quaternion.identity;
            Anim.transform.localPosition = new Vector3(0, .3f);

            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(3, 3);

            Invoke(nameof(damageanddestroy), .2f);

            Triggered = true;
            Anim.SetTrigger("Hit");
        }
    }

    void damageanddestroy()
    {
        TargetStats.applyshock(true);
        TargetStats.takedamage(Damage);
        Destroy(gameObject, .4f);

    }
}
