using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public static class SaveSystem
{
    public static void SaveRoomBubbles(List<BubbleSaveData> saveData,int sceneID)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Bubble" + sceneID.ToString() + ".save";
        FileStream stream = new FileStream(path, FileMode.Create);

        List<BubbleSaveData> data = saveData;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static List<BubbleSaveData> LoadRoomBubble(int sceneID)
    {
        string path = Application.persistentDataPath + "/Bubble" + sceneID.ToString() + ".save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            List<BubbleSaveData> data = formatter.Deserialize(stream) as List<BubbleSaveData>;
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogError("Path not found : " + path);
            return new List<BubbleSaveData>();
        }
    }

    public static void SaveRoomBubblesNumber(int number, int sceneID)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/BubbleRoomCount" + sceneID.ToString()+ ".save";
        FileStream stream = new FileStream(path, FileMode.Create);

        int data = number;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static int LoadRoomBubbleNumber(int sceneID)
    {
        string path = Application.persistentDataPath + "/BubbleRoomCount" + sceneID.ToString()+ ".save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int data = (int)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogError("Path not found : " + path);
            return 0;
        }
    }



    public static void SaveRoom(SaveData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/LevelComeplete.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(saveData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadLastRoom()
    {
        string path = Application.persistentDataPath + "/LevelComeplete.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogError("Path not found : " + path);
            return new SaveData();
        }

    }
}
