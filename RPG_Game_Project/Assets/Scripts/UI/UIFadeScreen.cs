using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeScreen : MonoBehaviour
{
    Animator Anim;

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }

    public void fadeout() => Anim.SetTrigger("FadeOut");    
    public void fadein() => Anim.SetTrigger("FadeIn");    
}
