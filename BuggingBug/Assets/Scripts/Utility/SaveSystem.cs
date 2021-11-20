using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class SaveSystem : MonoBehaviour
{
    public void SaveHighScore(float data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highscore.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        float saveData = data;

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public float LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            float saveData = (float)formatter.Deserialize(stream);
            stream.Close();
            return saveData;
        }
        else
        {
            return 0;
        }
    }

    public void SavePlayerPrefs(PlayerPrefData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerpref.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerPrefData saveData = new PlayerPrefData(data);

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public PlayerPrefData LoadPlayerPrefs()
    {
        string path = Application.persistentDataPath + "/playerpref.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerPrefData saveData = (PlayerPrefData)formatter.Deserialize(stream);
            stream.Close();
            return saveData;
        }
        else
        {
            return new PlayerPrefData(0.5f);
        }
    }
}
