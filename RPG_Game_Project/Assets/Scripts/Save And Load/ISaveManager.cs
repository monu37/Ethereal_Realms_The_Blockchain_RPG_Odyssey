using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager 
{
    void loaddata(GameData _data);
    void savedata( ref GameData _data);
}
