using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public Color bubbleColour;
    public bool canShrink = false;

    protected string playerTag = "Player";
    protected bool playerInBubble = false;
    [SerializeField] protected float bubbleSrinkSpeed = 1.1f;

    [SerializeField]
    private SpriteRenderer bubbleSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbleSprite.color = bubbleColour;
        //BubbleManager.AddBubble(this);
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

        playerInBubble = true;
        BubbleInteract(player.gameObject);

        if(canShrink)
            StartCoroutine(BubbleTime(player.gameObject));
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != playerTag)
            return;

        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        if (canShrink)
            StartCoroutine(BubbleTime(other.gameObject));
    }


    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != playerTag)
            return;

        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

        if (player == null)
            return;
 
        playerInBubble = false;
    }

    protected virtual void BubbleInteract()
    {

    }

    protected virtual void BubbleInteract(GameObject player)
    {

    }

    protected IEnumerator BubbleTime(GameObject player)
    {
        PlayerAir air = player.GetComponent<PlayerAir>();

        while (playerInBubble && transform.localScale.x > 0.01f)
        {
            if (air.GetCurrentAir() >= air.maxAir)
                break;
            Vector3 currentScale = transform.localScale;
            Vector3 newScale = transform.localScale / bubbleSrinkSpeed;

            transform.localScale = Vector3.Lerp(currentScale, newScale, bubbleSrinkSpeed * Time.deltaTime);
            BubbleInteract(player);
            yield return null;
        }

        if (transform.localScale.x <= 0.01f)
            Destroy(gameObject);
    }
}
