using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    private readonly WaitForSecondsRealtime waitTime = new WaitForSecondsRealtime(1.5f);
    private readonly string exitScene = "Exit Scene";
    private readonly string enterNewScene = "New Scene";

    [Tooltip("This is the panel in the canvas")]
    [SerializeField] private Animator screenTransitionAnimator;
    [SerializeField] private GameObject player;
    [Tooltip("Only tick if its in the first scene and it avoids loading checks")]
    [SerializeField] private bool firstRoom;
    [SerializeField] private Exitway[] doorways;
    [SerializeField] private AudioSource changeSceneAudio;


    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        if(!firstRoom)
        {
            SaveData data = SaveSystem.LoadLastRoom();

            for(int i = 0; i < doorways.Length; i++)
            {
                int door = data.GetDoorNumber();
                if(doorways[i].doorNumber == door)
                {
                    player.transform.position = doorways[i].spawnLocation.position;
                    player.GetComponent<PlayerAir>().currentAir = data.GetPlayerAir();
                    player.GetComponent<PlayerAir>().UpdateUI();
                    break;
                }
            }

        }

        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(screenTransitionAnimator.gameObject.transform.parent.gameObject);


        //if (PlayerMovement.instance == null)
        //{
        //    GameObject pc = Instantiate(player, Vector3.zero, Quaternion.identity);
        //    PlayerAir air = pc.GetComponent<PlayerAir>();
        //    air.sliderImage = image;
        //    air.airSlider = slider;
        //    cameraController.GetPlayerTransform(air.transform);
        //    air.airSlider.maxValue = air.maxAir;
        //    air.currentAir = air.maxAir;
        //    air.UpdateUI();
        //}
    }

    void Start()
    {
        screenTransitionAnimator.SetTrigger(enterNewScene);

    }


    public static void LoadNewArea(int sceneID)
    {
        instance.StartCoroutine(instance.ScreenTransitionAnimation(sceneID));
    }

    private IEnumerator ScreenTransitionAnimation(int sceneID)
    {
        while (true)
        {
            //AUDIO SOURCE
            //if(!changeSceneAudio.isPlaying)
            //    changeSceneAudio.Play();
            screenTransitionAnimator.SetTrigger(exitScene);
            yield return waitTime;

            SceneManager.LoadScene(sceneID);

        }
    }

    public static int GetCurrentSceneID()
    {
        Scene scene = SceneManager.GetActiveScene();

        return scene.buildIndex;
    }
}
