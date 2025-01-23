using UnityEngine;

public class Exitway : MonoBehaviour
{
    [Tooltip("The scene ID")]
    [SerializeField] private int connectingRoom;
    [Tooltip("Needs to be the same door number as the next scene where you want the player to be")]
    public int doorNumber;
    [Tooltip("The location where the player spawns when entering from this hallway")]
    public Transform spawnLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        PlayerAir air = collision.gameObject.GetComponent<PlayerAir>();

        //BubbleManager.SaveRoom();
        SaveData data = new SaveData(air.transform.position, air.currentAir, doorNumber);
        SaveSystem.SaveRoom(data);
        GameManager.LoadNewArea(connectingRoom);
    }
}
