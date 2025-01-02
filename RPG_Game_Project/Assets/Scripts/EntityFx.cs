using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;

public class EntityFx : MonoBehaviour
{
    protected Player player;
    protected SpriteRenderer Sr;

    [Header("Pop Up Text")]
    [SerializeField] private GameObject PopUpTextPrefab;


    [Header("Flash FX")]
    [SerializeField] float FlashDuration;
    [SerializeField] Material HitMat;
    Material OriginalMat;


    [Header("Ailment Colors")]
    [SerializeField] Color[] IgniteColor;
    [SerializeField] Color[] ChillColor;
    [SerializeField] Color[] ShockColor;

    [Header("Ailment particles")]
    [SerializeField] private ParticleSystem IgniteFx;
    [SerializeField] private ParticleSystem ChillFx;
    [SerializeField] private ParticleSystem ShockFx;

    [Header("Hit FX")]
    [SerializeField] private GameObject HitFx;
    [SerializeField] private GameObject CriticalHitFx;

    private GameObject MyHealthBar;



    protected virtual void Start()
    {
        Sr = GetComponentInChildren<SpriteRenderer>();
        OriginalMat = Sr.material;

        player = PlayerManager.instance.player;


        MyHealthBar = GetComponentInChildren<UIHealthBar>(true).gameObject;
    }

    public void createpopuptext(string _text)
    {
        float randomx = Random.Range(-1, 1);
        float randomy = Random.Range(1.5f, 3);

        Vector3 positionoffset = new Vector3(randomx, randomy, 0);

        GameObject newtext = Instantiate(PopUpTextPrefab, transform.position + positionoffset, Quaternion.identity);

        newtext.GetComponent<TextMeshPro>().text = _text;
    }

    public void maketransparent(bool _istransparent)
    {
        if (_istransparent)
        {
            MyHealthBar.SetActive(false);
            Sr.color = Color.clear;
        }
        else
        {
            MyHealthBar.SetActive(true);
            Sr.color = Color.white;
        }
    }

    IEnumerator flashfx()
    {
        Sr.material = HitMat;
        Color currentcolor = Sr.color;
        Sr.color = Color.white;

        yield return new WaitForSeconds(FlashDuration);

        Sr.color = currentcolor;
        Sr.material = OriginalMat;

    }

    void redcolorblink()
    {
        if (Sr.color != Color.white)
        {
            Sr.color = Color.white;
        }
        else
        {
            Sr.color = Color.red;
        }
    }

    void cancelcolorchange()
    {
        CancelInvoke();

        Sr.color = Color.white;
        IgniteFx.Stop();
        ChillFx.Stop();
        ShockFx.Stop();
    }


    // 

    public void chillfxfor(float _sec)
    {
        ChillFx.Play();
        InvokeRepeating(nameof(setchillcolor), 0, .2f);
        Invoke(nameof(cancelcolorchange), _sec);
    }
    public void ignitefxfor(float _sec)
    {
        IgniteFx.Play();
        InvokeRepeating(nameof(setignitedcolor), 0, .2f);
        Invoke(nameof(cancelcolorchange), _sec);
    }
    public void shockfxfor(float _sec)
    {
        ShockFx.Play();
        InvokeRepeating(nameof(setshockcolor), 0, .2f);
        Invoke(nameof(cancelcolorchange), _sec);
    }

    void setchillcolor()
    {
        if (Sr.color != ChillColor[0])
        {
            Sr.color = ChillColor[0];
        }
        else
        {
            Sr.color = ChillColor[1];
        }
    }

    void setignitedcolor()
    {
        if (Sr.color != IgniteColor[0])
        {
            Sr.color = IgniteColor[0];
        }
        else
        {
            Sr.color = IgniteColor[1];
        }
    }

    void setshockcolor()
    {
        if (Sr.color != ShockColor[0])
        {
            Sr.color = ShockColor[0];
        }
        else
        {
            Sr.color = ShockColor[1];
        }
    }

    public void createhitfx(Transform _target, bool _iscritical)
    {

        float zrotation = Random.Range(-90, 90);
        float xposition = Random.Range(-.5f, .5f);
        float yposition = Random.Range(-.5f, .5f);

        Vector3 hitfxrotation = new Vector3(0, 0, zrotation);

        GameObject hitprefab = HitFx;

        if (_iscritical)
        {
            hitprefab = CriticalHitFx;

            float yRotation = 0;
            zrotation = Random.Range(-45, 45);

            if (GetComponent<Entity>().FacingDir == -1)
                yRotation = 180;

            hitfxrotation = new Vector3(0, yRotation, zrotation);

        }

        GameObject newhitfx = Instantiate(hitprefab, _target.position + new Vector3(xposition, yposition), Quaternion.identity); /*// uncomment this if you want particle to follow target ,_target);*/
        newhitfx.transform.Rotate(hitfxrotation);
        Destroy(newhitfx, .5f);
    }
}
