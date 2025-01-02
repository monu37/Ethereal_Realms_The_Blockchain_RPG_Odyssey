using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkillController : MonoBehaviour
{

    [SerializeField] GameObject HotKeyPrefab;
    [SerializeField] List<KeyCode> KeyCodeLlist;


    float MaxSize;
    float GrowSpeed;
    float ShrinkSpeed;
    float BlackHoleTImer;

    bool IsCanGrow = true;
    bool IsCanShrink;
    bool IsCanCreateHotKeys = true;
    bool IscloneAttackRealsed;
    bool IsPlayerCanDisapear = true;

    int AmountOfAttack;
    float CloneAttackCoolDown;
    float CloneAttackTimer;

    [SerializeField] List<Transform> Targets = new List<Transform>();
    List<GameObject> CreatedHotKeys = new List<GameObject>();


    public bool IsPlayerCanExitState { get; private set; }

    public void setupblackhole(float _maxsize, float _growspeed, float _shrinkspeed, int _amountofattack, float _cloneattackcooldown, float _duration)
    {
        MaxSize = _maxsize;
        GrowSpeed = _growspeed;
        ShrinkSpeed = _shrinkspeed;
        AmountOfAttack = _amountofattack;
        CloneAttackCoolDown = _cloneattackcooldown;


        BlackHoleTImer = _duration;

        if (SkillManager.instance.Clone.IsCrystalInsteadOfClone)
        {
            IsPlayerCanDisapear = false;
        }

    }

    private void Update()
    {

        CloneAttackTimer -= Time.deltaTime;
        BlackHoleTImer -= Time.deltaTime;

        if (BlackHoleTImer < 0)
        {
            BlackHoleTImer = Mathf.Infinity;

            if (Targets.Count > 0)
            {
                relasedcloneattack();
            }
            else
            {
                finishblackholeability();
            }
        }



        if (Input.GetKeyDown(KeyCode.R))
        {
            relasedcloneattack();
        }

        cloneattacklogic();

        growlogic();

        shrinklogic();
    }

    private void growlogic()
    {
        if (IsCanGrow && !IsCanShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(MaxSize, MaxSize), GrowSpeed * Time.deltaTime);
        }
    }

    private void shrinklogic()
    {

        if (IsCanShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), ShrinkSpeed * Time.deltaTime);

            if (transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void relasedcloneattack()
    {

        if (Targets.Count <= 0)
        {
            return;
        }

        destroyhotkeys();
        IscloneAttackRealsed = true;
        IsCanCreateHotKeys = false;

        if (IsPlayerCanDisapear)
        {
            IsPlayerCanDisapear = false;
            PlayerManager.instance.player.Fx.maketransparent(true);

        }
    }

    private void cloneattacklogic()
    {
        if (CloneAttackTimer < 0 && IscloneAttackRealsed && AmountOfAttack > 0)
        {
            CloneAttackTimer = CloneAttackCoolDown;


            int ran = Random.Range(0, Targets.Count);

            float xoffset;
            if (Random.Range(0, 100) > 50)
            {
                xoffset = 2;
            }
            else
            {
                xoffset = -2;
            }

            if(SkillManager.instance.Clone.IsCrystalInsteadOfClone)
            {
                SkillManager.instance.Crystal.createcrystal();
                SkillManager.instance.Crystal.currentcrystalchooserandomenemy();
            }
            else
            {
            SkillManager.instance.Clone.createclone(Targets[ran], new Vector3(xoffset, 0));

            }



            AmountOfAttack--;

            if (AmountOfAttack <= 0)
            {
                Invoke(nameof(finishblackholeability), 1f);

            }
        }
    }

    private void finishblackholeability()
    {
        destroyhotkeys();

        IsPlayerCanExitState = true;
        IsCanShrink = true;
        IscloneAttackRealsed = false;
    }

    void destroyhotkeys()
    {
        if (CreatedHotKeys.Count < 0)
        {
            return;
        }

        for (int i = 0; i < CreatedHotKeys.Count; i++)
        {
            Destroy(CreatedHotKeys[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Enemy>() != null)
        {
            col.GetComponent<Enemy>().freezetimer(true);

            createhotkey(col);

        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<Enemy>() != null)
        {
            col.GetComponent<Enemy>().freezetimer(false);
        }
    }

    private void createhotkey(Collider2D col)
    {

        if (KeyCodeLlist.Count < 0)
        {
            Debug.LogWarning("Not enough hot keys in a key code list");
            return;
        }

        if (!IsCanCreateHotKeys)
        {
            return;
        }

        GameObject newhotkey = Instantiate(HotKeyPrefab, col.transform.position + new Vector3(0, 2), Quaternion.identity);
        CreatedHotKeys.Add(newhotkey);

        KeyCode choosenkey = KeyCodeLlist[Random.Range(0, KeyCodeLlist.Count)];
        KeyCodeLlist.Remove(choosenkey);

        BlckholeHotkeyController newhotkeyscript = newhotkey.GetComponent<BlckholeHotkeyController>();
        newhotkeyscript.setuphotkey(choosenkey, col.transform, this);
    }

    public void addenemytolist(Transform _enemytrans) => Targets.Add(_enemytrans);
}
