using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;

    Transform playerTrans;

    [SerializeField] CheckPoint[] AllCheckPoints;
    [SerializeField] string ClosestCheckPointID;

    [Header("Lost Currency")]
    [SerializeField] GameObject LostCurrencyPrefab;
    public int LostCurrencyAmount;
    [SerializeField] float LostCurrencyX;
    [SerializeField] float LostCurrencyY;

    [Space]
    [SerializeField] TextMeshProUGUI ConversationText;

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

    }

    private void Start()
    {
        AllCheckPoints = GameObject.FindObjectsOfType<CheckPoint>();
        playerTrans = PlayerManager.instance.player.transform;

        setconversationtext("Clear all the checkpoint in a sequence to complete the game");

    }

    private void Update()
    {
       
    }

    public void restartgame()
    {
        SaveManager.instance.savegame();

        Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(scene.name);
    }

    public void loaddata(GameData _data)
    {

        StartCoroutine(loadwithdelay(_data));
        print("Game manager Loaded");
    }

    private void loadcheckpoints(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.CheckPoints)
        {
            foreach (CheckPoint check in AllCheckPoints)
            {
                if (check.Id == pair.Key && pair.Value == true)
                {
                    check.activatedcheckpoint();
                }
            }
        }
    }

    IEnumerator loadwithdelay(GameData _data)
    {
        yield return new WaitForSeconds(.1f);

        loadcheckpoints(_data);
        loadclosestcheckpoint(_data);
        loadlostcurrency(_data);

    }

    private void loadlostcurrency(GameData _data)
    {
        LostCurrencyAmount = _data.LostCurrencyAmount;
        LostCurrencyX = _data.LostCurrencyX;
        LostCurrencyY = _data.LostCurrencyY;

        if (LostCurrencyAmount > 0)
        {
            GameObject newlostcurrency = Instantiate(LostCurrencyPrefab, new Vector3(LostCurrencyX, LostCurrencyY), Quaternion.identity);
            newlostcurrency.GetComponent<LostCurrencyController>().Currency = LostCurrencyAmount;
        }

        LostCurrencyAmount = 0;
    }

    private void loadclosestcheckpoint(GameData _data)
    {
        if (_data.ClosestCheckPointId == null)
        {
            return;
        }

        ClosestCheckPointID = _data.ClosestCheckPointId;

        foreach (CheckPoint check in AllCheckPoints)
        {
            if (ClosestCheckPointID == check.Id)
            {
                playerTrans.position = check.transform.position;
            }
        }
    }

    public void savedata(ref GameData _data)
    {
        _data.LostCurrencyAmount = LostCurrencyAmount;
        _data.LostCurrencyX = playerTrans.position.x;
        _data.LostCurrencyY = playerTrans.position.y;



        if (FindClosestCheckPoint() != null)
        {
            _data.ClosestCheckPointId = FindClosestCheckPoint().Id;

        }
        _data.CheckPoints.Clear();

        foreach (CheckPoint _checkPoint in AllCheckPoints)
        {
            _data.CheckPoints.Add(_checkPoint.Id, _checkPoint.IsActivated);
        }
    }

    CheckPoint FindClosestCheckPoint()
    {
        float closestdistance = Mathf.Infinity;

        CheckPoint closestcheckpoint = null;

        foreach (var checkpoint in AllCheckPoints)
        {
            float distancetocheckpoint = Vector2.Distance(playerTrans.position, checkpoint.transform.position);

            if (distancetocheckpoint < closestdistance && checkpoint.IsActivated == true)
            {
                closestdistance = distancetocheckpoint;
                closestcheckpoint = checkpoint;

            }

        }

        return closestcheckpoint;
    }

    public void pausegame(bool _ispause)
    {
        if (_ispause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void setconversationtext(string _text)
    {
       StartCoroutine(onoffconversation(_text));
    }

    IEnumerator onoffconversation(string _text)
    {
        ConversationText.text = " ";
        ConversationText.gameObject.SetActive(true);
        yield return new WaitForSeconds(.21f);

        ConversationText.text = _text;

        yield return new WaitForSeconds(2f);
        ConversationText.gameObject.SetActive(false);

    }
}
