using System.Collections;
using UnityEngine;

public class AirBubble : Bubble
{
    [Space]
    [SerializeField] private float airIntakeSpee;
    protected override void BubbleInteract(GameObject player)
    {

        player.GetComponent<PlayerAir>().ChangeAirAmount(+airIntakeSpee);
    }

}
