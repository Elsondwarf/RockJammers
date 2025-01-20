using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public Color bubbleColour;
    protected string playerTag = "Player";
    protected bool playerInBubble = false;

    [SerializeField]
    private SpriteRenderer bubbleSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbleSprite.color = bubbleColour;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != playerTag)
            return;

        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        BubbleInteract(player);

        StartCoroutine(BubbleTime(player));
        playerInBubble = true;
    }


    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != playerTag)
            return;

        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        StopCoroutine(BubbleTime(player));
        playerInBubble = false;
    }

    protected virtual void BubbleInteract()
    {

    }

    protected virtual void BubbleInteract(PlayerMovement player)
    {

    }

    protected virtual IEnumerator BubbleTime(PlayerMovement player)
    {
        while (transform.localScale.x > 0.5f)
        {
            //BubbleInteract();
            //transform.localScale /= 1.1f;
            yield return null;
        }
    }
}
