using System.Collections;
using UnityEngine;

public class AirBubble : Bubble
{

    protected override void BubbleInteract(PlayerMovement player)
    {

        Debug.Log("bubble");

        player.ChangeAirAmount(+ 0.1f * Time.deltaTime);
        //transform.localScale *= 0.1f * Time.deltaTime;

    }


    protected override IEnumerator BubbleTime(PlayerMovement player)
    {
        while (transform.localScale.x > 0.5f)
        {
            if (!playerInBubble)
                break;
            Debug.Log("bububu");
            BubbleInteract();
            transform.localScale /= 1.1f;
            yield return null;
        }
    }
}
