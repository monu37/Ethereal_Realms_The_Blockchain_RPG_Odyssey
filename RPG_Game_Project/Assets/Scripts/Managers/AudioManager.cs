using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource[] SfXSource;
    [SerializeField] AudioSource[] BgMSource;

    public bool IsPlayingBgm;
     int BgmIndex;

    //
    [SerializeField] float SfXMinimumDistance;
    bool IsCanPlaySfx;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }


        Invoke(nameof(allsfx), 1f);
    }

    private void Update()
    {
        if (!IsPlayingBgm)
        {
            stopallbgm();
        }
        else
        {
            if (!BgMSource[BgmIndex].isPlaying)
            {
                playrandombgm();
            }
        }
    }

    public void playsfx(int _sfxindex, Transform _source)
    {
        //if (SfXSource[BgmIndex].isPlaying)
        //{
        //    return;
        //}

        if (!IsCanPlaySfx)
        {
            return;
        }

      
        if(_source != null && Vector2.Distance(PlayerManager.instance.player.transform.position,_source.position) > SfXMinimumDistance)
        {
           
            return;
        }

        if(_sfxindex <SfXSource.Length)
        {
           
            SfXSource[_sfxindex].pitch = Random.Range(.8f, 1f);
            SfXSource[_sfxindex].Play();
        }
    }

    public void stopsfx(int _sfxindex)
    {
        SfXSource[_sfxindex].Stop();
    }

    public void playrandombgm()
    {
        BgmIndex = Random.Range(0,BgMSource.Length);

        playbgm(BgmIndex);
    }

    public void playbgm(int _bgindex)
    {

        BgmIndex = _bgindex;
        stopallbgm();

        BgMSource[BgmIndex].Play();
    }

    public void stopallbgm()
    {
        for (int i = 0; i < BgMSource.Length; i++)
        {
            BgMSource[i].Stop();
        }
    }

    void allsfx() => IsCanPlaySfx = true;
}
