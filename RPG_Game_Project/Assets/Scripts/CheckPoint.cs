using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    Animator Anim;
    public string Id;
    public bool IsActivated;
    public int CheckPointId;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    [ContextMenu("Generate checkpoint Id")]
    void GenerateId()
    {
        Id = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            activatedcheckpoint();
        }
    }

    public void activatedcheckpoint()
    {
        if (!LevelManager.Instance.IsRightCheckpoint(CheckPointId))
        {
            return;
        }

        if (!IsActivated)
        {
            AudioManager.instance.playsfx(6, transform);
            LevelManager.Instance.checkcheckpoint();
            //LevelManager.instance.clearcheckpoint();
        }

        IsActivated = true;
        Anim.SetBool("IsActive", true);
    }
}
