using System.Collections.Generic;
using UnityEngine;

public class Exitway : MonoBehaviour
{
    [Tooltip("The scene ID")]
    [SerializeField] private int connectingRoom;
    [Tooltip("Needs to be the same door number as the next scene where you want the player to be")]
    public int doorNumber;
    [Tooltip("The location where the player spawns when entering from this hallway")]
    public Transform spawnLocation;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        PlayerAir air = collision.gameObject.GetComponent<PlayerAir>();

        //BubbleManager.SaveRoom();
        SaveData data = new SaveData(air.transform.position, air.currentAir, doorNumber);
        SaveSystem.SaveRoom(data);

        List<GameObject> pooled = BubbleManager.GetAllActivePooledBubble();

        BubbleManager.SaveBubbles(pooled.ToArray());

        GameManager.LoadNewArea(connectingRoom);

    }
}
