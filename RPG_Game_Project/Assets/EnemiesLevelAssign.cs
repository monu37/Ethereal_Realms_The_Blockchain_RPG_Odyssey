using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesLevelAssign : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI EnemyText;
    [SerializeField] Image Icon;

   
    public void assignenemy(string _text,Sprite _icon)
    {
        EnemyText.text = _text;
        Icon.sprite = _icon;
        print("hello");
    }
}
