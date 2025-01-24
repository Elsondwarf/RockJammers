using System.Collections;
using UnityEngine;

public class AirBubble : Bubble
{
    [Space]
    [SerializeField] private float airIntakeSpee;
    protected override void BubbleInteract(GameObject player)
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.pickupSound, transform.position);

        player.GetComponent<PlayerAir>().ChangeAirAmount(+airIntakeSpee);
    }

}
