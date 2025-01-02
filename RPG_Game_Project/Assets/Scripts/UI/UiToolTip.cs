using UnityEngine;

public class UiToolTip : MonoBehaviour
{
    [SerializeField] float XLimit = 960f;
    [SerializeField] float YLimit = 540;

    [SerializeField] float XOffset = 150;
    [SerializeField] float YOffset = 150;

    public virtual void adjusttooltipposition()
    {

        float newXOffeset = 0;
        float newYOffset = 0;

        Vector2 MousePosition = Input.mousePosition;


        if (MousePosition.x > XLimit)
        {
            newXOffeset = -XOffset;
        }
        else
        {
            newXOffeset = XOffset;
        }

        if (MousePosition.y > YLimit)
        {
            newYOffset = -YOffset;
        }
        else
        {
            newYOffset = YOffset;
        }

        transform.position = new Vector2(MousePosition.x + newXOffeset, MousePosition.y + newYOffset);
    }

}


