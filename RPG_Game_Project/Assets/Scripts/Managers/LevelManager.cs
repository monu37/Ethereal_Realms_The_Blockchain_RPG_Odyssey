using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

    [SerializeField] Transform[] SpawnPos;
    [SerializeField] GameObject[] EnemyPrefabs;
    [SerializeField] GameObject DropPrefab;
    [SerializeField] ItemData[] Drops;
    [SerializeField] GameObject CurrencyPrefab;

    //checkpoint
    [SerializeField] TextMeshProUGUI TotalCheckPointText;
    public int CheckPointClear;
    [SerializeField] GameObject[] AllCheckPoints;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InvokeRepeating(nameof(spawn), Random.Range(1, 5f), Random.Range(5f, 20f));
        TotalCheckPointText.text = CheckPointClear + "/" + AllCheckPoints.Length;

        for (int i = 0; i < AllCheckPoints.Length; i++)
        {
            AllCheckPoints[i].GetComponent<CheckPoint>().CheckPointId = i+1;
        }
    }

    void spawn()
    {
        int ran = Random.Range(0, 9);
        if(ran <= 3)
        {
            spawndrops();
        }
        if(ran >6)
        {
            spawnenemy();
        }

        spawncurrency();
    }

    void spawncurrency()
    {
        int ranvalue = Random.Range(0, 40);
        int spawnpos = Random.Range(0, SpawnPos.Length);
        int checkpointspawnpos = Random.Range(0, AllCheckPoints.Length);
        GameObject newcurrency = null;

        if (ranvalue > 10 && ranvalue <30)
        {
            newcurrency = Instantiate(CurrencyPrefab, SpawnPos[spawnpos].position, Quaternion.identity);
            newcurrency.transform.parent = SpawnPos[spawnpos];
        }
        else if(ranvalue <10)
        {
            newcurrency = Instantiate(CurrencyPrefab, AllCheckPoints[checkpointspawnpos].transform.position, Quaternion.identity);
            newcurrency.transform.parent = AllCheckPoints[checkpointspawnpos].transform;
        }


    }

    void spawndrops()
    {
        int ranvalue = Random.Range(0, 40);
        int spawnpos = Random.Range(0, SpawnPos.Length);
        int dropindex = Random.Range(0, Drops.Length);
       

        if (ranvalue > 30)
        {
            dropitem(Drops[dropindex], spawnpos);
        }

    }

    void dropitem(ItemData _item, int _parentindex)
    {
        GameObject newdrop = Instantiate(DropPrefab, transform.position, Quaternion.identity);

        Vector2 newvelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));

        newdrop.GetComponent<ItemObject>().setupitem(_item, newvelocity);

        newdrop.transform.parent = SpawnPos[_parentindex];
    }

    void spawnoncheckpoint()
    {
        int ranvalue = Random.Range(0, 40);
        int checkpointindex = Random.Range(0, AllCheckPoints.Length);
        GameObject newenemy = null;

        if (ranvalue > 0 && ranvalue <=10) // spawn skeleton
        {
            newenemy = Instantiate(EnemyPrefabs[0], AllCheckPoints[checkpointindex].transform.position, Quaternion.identity);
        }
        else if (ranvalue < 16 && ranvalue > 10)//death bringer
        {

            newenemy = Instantiate(EnemyPrefabs[6], AllCheckPoints[checkpointindex].transform.position, Quaternion.identity);
        }
        else if (ranvalue < 25 && ranvalue > 16) //archer
        {
            newenemy = Instantiate(EnemyPrefabs[4], AllCheckPoints[checkpointindex].transform.position, Quaternion.identity);
        }
        else if (ranvalue < 33 && ranvalue > 25) //archer
        {
            newenemy = Instantiate(EnemyPrefabs[5], AllCheckPoints[checkpointindex].transform.position, Quaternion.identity);
        }
        else
        {
            int ran = Random.Range(0, 2);
            if (ran > 1.2f)
            {
                newenemy = Instantiate(EnemyPrefabs[1], AllCheckPoints[checkpointindex].transform.position, Quaternion.identity);
            }
            else if (ran > .6f && ran < 1.2f)
            {
                newenemy = Instantiate(EnemyPrefabs[1], AllCheckPoints[checkpointindex].transform.position, Quaternion.identity);
            }
            else if (ran < .6f)
            {
                newenemy = Instantiate(EnemyPrefabs[1], AllCheckPoints[checkpointindex].transform.position, Quaternion.identity);
            }

        }


        newenemy.transform.parent = AllCheckPoints[checkpointindex].transform;
    }

    public void spawnenemy()
    {
        int ranvalue = Random.Range(0, 40);
        int spawnpos=Random.Range(0,SpawnPos.Length);
        GameObject newenemy = null;

        if(ranvalue > 25) // spawn skeleton
        {
            newenemy = Instantiate(EnemyPrefabs[0], SpawnPos[spawnpos].position, Quaternion.identity);
        }
        else if(ranvalue < 25 && ranvalue >= 20)//death bringer
        {

            newenemy = Instantiate(EnemyPrefabs[6], SpawnPos[spawnpos].position, Quaternion.identity);
        }
        else if(ranvalue < 20 &&  ranvalue > 10) //archer
        {
            newenemy = Instantiate(EnemyPrefabs[4], SpawnPos[spawnpos].position, Quaternion.identity);
        } 
        else if(ranvalue < 10 &&  ranvalue > 5) //archer
        {
            newenemy = Instantiate(EnemyPrefabs[5], SpawnPos[spawnpos].position, Quaternion.identity);
        }
        else
        {
            int ran = Random.Range(0, 2);
            if ( ran> 1.2f)
            {
                newenemy = Instantiate(EnemyPrefabs[1], SpawnPos[spawnpos].position, Quaternion.identity);
            } 
            else if ( ran> .6f && ran<1.2f)
            {
                newenemy = Instantiate(EnemyPrefabs[1], SpawnPos[spawnpos].position, Quaternion.identity);
            }
            else if ( ran<.6f)
            {
                newenemy = Instantiate(EnemyPrefabs[1], SpawnPos[spawnpos].position, Quaternion.identity);
            }
          
        }
        

        newenemy.transform.parent = SpawnPos[spawnpos];
    }

    public void checkcheckpoint()
    {
        CheckPointClear++;
        TotalCheckPointText.text = CheckPointClear + "/" + AllCheckPoints.Length;

        if(CheckPointClear == AllCheckPoints.Length)
        {
            GameManager.instance.setconversationtext("Congrulations!! You Completed The Game!");
        }
    }

    public bool IsRightCheckpoint(int _checkpointno)
    {
        if (CheckPointClear + 1 == _checkpointno)
        {
            return true;
        }

        return false;
    }

}
