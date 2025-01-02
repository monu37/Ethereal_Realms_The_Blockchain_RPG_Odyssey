using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    public static PlayerManager instance;

    public Player player;

    public int Currency;

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

    public bool IsHaveEnoughCurrency(int _amount)
    {
        if (_amount > Currency)
        {
            print("Not enough money");
            return false;
        }

        Currency -= _amount;
        return true;
    }

    public int GetCurrency() {  return Currency; }

    public void loaddata(GameData _data)
    {
       this. Currency = _data.Currency;
    }

    public void savedata(ref GameData _data)
    {
      _data.Currency = this.Currency;
    }
}
