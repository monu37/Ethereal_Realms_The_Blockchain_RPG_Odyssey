using TMPro;
using UnityEngine;

public class PopUpTextFx : MonoBehaviour
{
    private TextMeshPro PopUpText;

    [SerializeField] private float Speed;
    [SerializeField] private float DisappearanceSpeed;
    [SerializeField] private float ColorDisappearanceSpeed;

    [SerializeField] private float LifeTime;


    private float TextTimer;

    void Start()
    {
        PopUpText = GetComponent<TextMeshPro>();
        TextTimer = LifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), Speed * Time.deltaTime);
        TextTimer -= Time.deltaTime;

        if (TextTimer < 0)
        {
            float alpha = PopUpText.color.a - ColorDisappearanceSpeed * Time.deltaTime;
            PopUpText.color = new Color(PopUpText.color.r, PopUpText.color.g, PopUpText.color.b, alpha);


            if (PopUpText.color.a < 50)
                Speed = DisappearanceSpeed;

            if (PopUpText.color.a <= 0)
                Destroy(gameObject);
        }
    }
}
