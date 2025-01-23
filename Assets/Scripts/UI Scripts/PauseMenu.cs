using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public PlayerControls inputs;

    private bool isPaused = false;
    private int lastMenuOpened = 0;

    [SerializeField] private GameObject pauseObject;
    [SerializeField] private GameObject[] pauseTypeCanvas;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject[] firstButtonSelect;
    [SerializeField] private AudioSource hoverSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);

    }

    public void ResumeGame()
    {

        PauseGame();
    }

    private void SetRigidbodies2D(bool isSimulated)
    {
        Rigidbody2D[] rbArray = FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.InstanceID);

        foreach (Rigidbody2D rb in rbArray)
        {
            rb.simulated = isSimulated;
        }
    }

    public void PauseGame()
    {
        ///currently game is running (false)
        ///so when pause game set all rigidbodies to false simulated
        SetRigidbodies2D(isPaused);
        pauseObject.SetActive(!isPaused);

        OpenCanvas(0);

        isPaused = !isPaused;

    }

    public void OpenCanvas(int index)
    {
        for(int i = 0; i < pauseTypeCanvas.Length; i++)
        {
            if (index == i)
                continue;

            pauseTypeCanvas[i].SetActive(false);
        }



        StartCoroutine(HighlightButton(index));
        pauseTypeCanvas[index].SetActive(true);
        lastMenuOpened = index;
    }

    public IEnumerator HighlightButton(int index)
    {
        while (true)
        {
            eventSystem.SetSelectedGameObject(null);


            yield return new WaitForEndOfFrame();

            eventSystem.SetSelectedGameObject(firstButtonSelect[index]);
            break;
        }
    }

    public void OnHover(Image image)
    {
        image.sprite = hoverSprite;
        //AUDIO SOURCE
        hoverSource.Play();
    }

    public void LeftHover(Image image)
    {
        image.sprite = defaultSprite;

    }

    public void NewGame()
    {
        GameManager.LoadNewArea(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackButtonPressed()
    {
        if(lastMenuOpened == 0)
        {
            ResumeGame();
            return;
        }

        OpenCanvas(0);

    }

}
