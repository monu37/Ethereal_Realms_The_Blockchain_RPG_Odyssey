using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlckholeHotkeyController : MonoBehaviour
{
    SpriteRenderer Sr;

    KeyCode MyHotKey;
    TextMeshProUGUI MyText;

    Transform MyEnemy;
    BlackholeSkillController BlackHoleController;

    public void setuphotkey(KeyCode _myhotkey, Transform _enemy, BlackholeSkillController _blackhole)
    {
        Sr = GetComponent<SpriteRenderer>();
        MyText = GetComponentInChildren<TextMeshProUGUI>();

        MyEnemy = _enemy;
        BlackHoleController = _blackhole;
        MyHotKey = _myhotkey;
        MyText.text = MyHotKey.ToString();

    }

    private void Update()
    {
        if(Input.GetKeyDown(MyHotKey)) 
        {
            BlackHoleController.addenemytolist(MyEnemy);

            MyText.color = Color.clear;

            Sr.color = Color.clear; 
        }
    }
}
