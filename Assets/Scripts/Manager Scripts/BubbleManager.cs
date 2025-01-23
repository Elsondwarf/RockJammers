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
        //LoadRoom();
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
