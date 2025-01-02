using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class FileDataHandler 
{
    string DataPath = "";
    string DataFileName = "";

    bool IsEncryptData = false;
    string EncryptCode = "FinalProject";

    public FileDataHandler(string _path, string _dataFileName, bool _isencrypdata)
    {
        DataPath = _path;
        DataFileName = _dataFileName;
        IsEncryptData = _isencrypdata;
    }

    public void save(GameData _data)
    {
        string path= Path.Combine(DataPath, DataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            string datatostore = JsonUtility.ToJson(_data, true);

            if (IsEncryptData)
            {
                datatostore=EncryptDecrypt(datatostore);
            }

            using(FileStream stream= new FileStream(path, FileMode.Create))
            {
                using(StreamWriter writer= new StreamWriter(stream))
                {
                    writer.Write(datatostore);
                }
            }
        }
        catch(Exception e) 
        {
            Debug.LogError("Error on trying to save data to file : " + path + "\n" + e);
        }
    }

    public GameData load()
    {
        string path= Path.Combine(DataPath , DataFileName);

        GameData loaddata = null;

        if(File.Exists(path))
        {
            try
            {
                string datatoload = "";

                using(FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using(StreamReader reader= new StreamReader(stream))
                    {
                        datatoload = reader.ReadToEnd();
                    }
                }

                if(IsEncryptData)
                {
                    datatoload=EncryptDecrypt(datatoload);
                }


                loaddata = JsonUtility.FromJson<GameData>(datatoload);
            }
            catch (Exception e)
            {
                Debug.LogError("Error on trying to load data from file : " + path + "\n" + e);
            }
        }

        return loaddata;
    }

    public void deletedata()
    {
        string path = Path.Combine(DataPath, DataFileName);

        if(File.Exists(path))
        {
            File.Delete(path);
        }
    }
    string EncryptDecrypt(string _data)
    {
        string modifieddata = "";

        for (int i = 0; i < _data.Length; i++)
        {
            modifieddata += (char)(_data[i] ^ EncryptCode[i % EncryptCode.Length]);
        }

        return modifieddata;
    }
}
