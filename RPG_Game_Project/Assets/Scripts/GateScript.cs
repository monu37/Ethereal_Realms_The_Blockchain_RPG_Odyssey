using TMPro;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public static GateScript instance;

    public bool IsLevelStarted;
    public bool IsLevelFinish;
    public int levelNo;

    private void Awake()
    {
        instance = this;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {


        if (col.GetComponent<Player>() != null)
        {
            GameManager.instance.setconversationtext("Welcome To Level : " + levelNo);

            //LevelManager level = LevelManager.instance;

            //if (!level.IsLevelStart && !level.IsLevelEnd && !IsLevelStarted)
            //{
            //    level.IsLevelStart = true;
            //    IsLevelStarted = true;
            //}
            //else if(level.IsLevelStart && !level.IsLevelEnd && !IsLevelStarted && !IsLevelFinish)
            //{
            //    level.IsLevelEnd = true;
            //    IsLevelFinish = true;
            //}
        }
    }
}
