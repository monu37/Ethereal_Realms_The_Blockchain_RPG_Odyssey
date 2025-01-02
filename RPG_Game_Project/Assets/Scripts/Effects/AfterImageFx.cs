using UnityEngine;

public class AfterImageFx : MonoBehaviour
{
    private SpriteRenderer sr;
    private float ColorLooseRate;


    public void setupafterimage(float _loosingspeed, Sprite _spriteimage)
    {
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = _spriteimage;
        ColorLooseRate = _loosingspeed;


    }

    private void Update()
    {
        float alpha = sr.color.a - ColorLooseRate * Time.deltaTime;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);


        if (sr.color.a <= 0)
            Destroy(gameObject);
    }
}
