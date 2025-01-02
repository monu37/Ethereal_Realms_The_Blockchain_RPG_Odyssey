using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [SerializeField] bool IsEncryptData;
    [SerializeField] string FileName;
    GameData gameData;


    List<ISaveManager> SaveManagerList= new List<ISaveManager>();
    FileDataHandler DataHandler;



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

    private void OnEnable()
    {
        DataHandler = new FileDataHandler(Application.persistentDataPath, FileName, IsEncryptData);
        SaveManagerList = FindAllSaveManager();

        print(IsHaveSaveData());

        loadgame();
    }

   
    public void newgame()
    {
        gameData = new GameData();
    }

    public void loadgame()
    {
        gameData = DataHandler.load();

        if (gameData == null)
        {
            print("No saved data found");
            newgame();
        }

        foreach (ISaveManager save in SaveManagerList)
        {
            save.loaddata(gameData);
        }
       
    }

    public void savegame()
    {
        print("Game is save");

        foreach(ISaveManager save in SaveManagerList)
        {
            save.savedata(ref gameData);
        }
        DataHandler.save(gameData);

      
    }

    private void OnApplicationQuit()
    {
        savegame();
    }

    List<ISaveManager> FindAllSaveManager()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }

    [ContextMenu("Delete Save File")]
    public void deletesavedata()
    {
        DataHandler = new FileDataHandler(Application.persistentDataPath, FileName, IsEncryptData);
        DataHandler.deletedata();
    }

    public bool IsHaveSaveData()
    {
        if (DataHandler.load() != null)
        {
            return true;
        }

        return false;
    }
}
