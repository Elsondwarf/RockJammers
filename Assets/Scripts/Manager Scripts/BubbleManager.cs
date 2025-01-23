using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public BubbleManager instance;
    private static List<Bubble> roomBubbles;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    private void Start()
    {
        LoadBubbles();
    }

    public static List<GameObject> GetAllActivePooledBubble()
    {
        ObjectPooling[] pooling = FindObjectsOfType<ObjectPooling>();

        List<GameObject> ob = new List<GameObject>(); ;
        foreach(ObjectPooling pool in pooling)
        {
            ob.AddRange(pool.activeObjects);
        }

        return ob;
        //BubbleSaveData currentBubble = new BubbleSaveData();
        //foreach(GameObject o in ob)
        //{
        //    if (o == null)
        //        continue;

        //    totalActiveBubbles++;

        //    currentBubble.x = o.transform.position.x;
        //    currentBubble.y = o.transform.position.y;
            
        //}

        //BubbleSaveData[] datas = new BubbleSaveData[totalActiveBubbles];

        //for(int i = 0; i <datas.Length; i++)
        //{
        //    datas[i] = new BubbleSaveData(currentBubble);
        //}
    }

    public static List<GameObject> GetAllPooledBubble()
    {
        ObjectPooling[] pooling = FindObjectsOfType<ObjectPooling>();

        List<GameObject> ob = new List<GameObject>(); ;
        foreach(ObjectPooling pool in pooling)
        {
            ob.AddRange(pool.pooledObjects);
        }

        return ob;
    }

    public static void SaveBubbles(GameObject[] ob)
    {
        BubbleSaveData currentBubble = new BubbleSaveData();
        int totalActiveBubbles = 0;

        foreach (GameObject o in ob)
        {
            if (o == null)
                continue;

            totalActiveBubbles++;

        }
        Debug.Log(currentBubble.GetPosition());
        BubbleSaveData[] datas = new BubbleSaveData[totalActiveBubbles];

        for (int i = 0; i < datas.Length; i++)
        {
            if(ob[i] != null)
            {
                currentBubble.x = ob[i].transform.position.x;
                currentBubble.y = ob[i].transform.position.y;
            }

            datas[i] = new BubbleSaveData(currentBubble);
        }


        SaveSystem.SaveRoomBubbles(datas, GameManager.GetCurrentSceneID());
    }


    public static void LoadBubbles()
    {
        BubbleSaveData[] datas = SaveSystem.LoadRoomBubble(GameManager.GetCurrentSceneID());
        Debug.Log(datas.Length);

        List<GameObject> s = GetAllPooledBubble();
        Debug.Log(s.Count);
        for (int i = 0; i < s.Count; i++)
        {
            if (i >= datas.Length)
                break;

            if (s[i] == null || datas[i] == null)
                continue;

            Vector3 v = new Vector3
            {
                x = datas[i].x,
                y = datas[i].y,
                z = 0
            };
            s[i].transform.position = v;
            s[i].SetActive(true);

        }
    }

    public static void AddBubble(Bubble bubble)
    {
        roomBubbles.Add(bubble);
    }

    public static void removeBubble(Bubble bubble)
    {
        roomBubbles.Remove(bubble);
    }

    public static void SaveRoom()
    {
        SaveSystem.SaveRoomBubblesNumber(roomBubbles.Count, 1);


    }

    //public void LoadRoom()
    //{
    //    int count = SaveSystem.LoadRoomBubbleNumber(1);

    //    for(int i = 0; i <count; i++)
    //    {
    //        BubbleSaveData data = SaveSystem.LoadRoomBubble(i, 1);
    //        Bubble bubble = data.GetBubble();
    //        Vector2 bPos = data.GetPosition();
    //        bubble.transform.position.With(bPos.x, bPos.y);

    //    }
    //}
}
