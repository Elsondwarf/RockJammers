using UnityEngine;

[System.Serializable]
public class BubbleSaveData
{
    public float x;
    public float y;

    //public  Bubble type;

    public BubbleSaveData()
    {
        x = 0;
        y = 0;
        //type = new Bubble();
    }

    public BubbleSaveData(Vector2 position, Bubble bubble)
    {
        x = position.x;
        y = position.y;
        //type = bubble;
    }

    public BubbleSaveData(BubbleSaveData data)
    {
        x = data.x;
        y = data.y;
        //type = data.type;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(x, y);
    }

    //public Bubble GetBubble()
    //{
        //return type;
    //}

}
