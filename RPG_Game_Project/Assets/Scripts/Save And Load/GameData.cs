using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int Currency;
    public SerializableDictionary<string, int> inventory;
    public List<string> EquipmentId;
    public SerializableDictionary<string, bool> SKillTree;

    public SerializableDictionary<string, bool> CheckPoints;
    public string ClosestCheckPointId;

    public float LostCurrencyX;
    public float LostCurrencyY;
    public int LostCurrencyAmount;

    public SerializableDictionary<string, float> VolumeSetings;

    public GameData()
    {
        LostCurrencyAmount = 0;
        LostCurrencyX = 0;
        LostCurrencyY = 0;

        this.Currency = 0;

        SKillTree = new SerializableDictionary<string, bool>();
        inventory = new SerializableDictionary<string, int>();
        EquipmentId = new List<string>();


        CheckPoints = new SerializableDictionary<string, bool>();
        ClosestCheckPointId = string.Empty;

        VolumeSetings = new SerializableDictionary<string, float>();
    }

}
