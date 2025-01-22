using UnityEngine;

[System.Serializable]
public class SaveData
{
    private float px;
    private float py;
    private float playerAir;
    private int doorNumber;

    public SaveData()
    {
        px= 0;
        py = 0;
        playerAir = 10;
        doorNumber = 1;
    }

    public SaveData(SaveData data)
    {
        px = data.px;
        py = data.py;
        playerAir = data.playerAir;
        doorNumber = data.doorNumber;
    }

    public SaveData(Vector2 pl, float a, int d)
    {
        px = pl.x;
        py = pl.y;
        playerAir = a;
        doorNumber = d;
    }

    public SaveData(Vector3 pl, float a, int d)
    {
        px = pl.x;
        py = pl.y;
        playerAir = a;
        doorNumber = d;
    }

    public int GetDoorNumber()
    {
        return doorNumber;
    }

    public Vector3 GetPlayerLocation()
    {
        return new Vector3(px, py, 0f);
    }

    public float GetPlayerAir()
    {
        return playerAir;
    }
}
