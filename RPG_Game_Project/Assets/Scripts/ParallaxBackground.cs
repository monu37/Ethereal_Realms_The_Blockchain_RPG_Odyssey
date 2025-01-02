using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    GameObject Cam;

    [SerializeField] float ParallaxEffect;
    float XPos;
    float Length;

    private void Start()
    {
        Cam = GameObject.Find("Main Camera");

        XPos = transform.position.x;
        Length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float DistanceToMove = Cam.transform.position.x * ParallaxEffect;
        float DistanceMoved = Cam.transform.position.x * (1 - ParallaxEffect);


        transform.position = new Vector3(XPos + DistanceToMove, transform.position.y);

        if(DistanceMoved >XPos + Length)
        {
            XPos += Length;
        }
        else if(DistanceMoved < XPos-Length)
        {
            XPos -= Length;
        }
    }
}
