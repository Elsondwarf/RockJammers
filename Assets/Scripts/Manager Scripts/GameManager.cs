using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider slider;
    public Image image;

    public static GameManager instance;


    private readonly WaitForSecondsRealtime waitTime = new WaitForSecondsRealtime(1.5f);
    private readonly string exitScene = "Exit Scene";
    private readonly string enterNewScene = "New Scene";

    [SerializeField] private Animator screenTransitionAnimator;
    [SerializeField] private GameObject player;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private bool firstRoom;
    [SerializeField] private Exitway[] doorways;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            //Destroy(screenTransitionAnimator.gameObject.transform.parent.gameObject);
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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenTransitionAnimator.SetTrigger(enterNewScene);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void LoadNewArea(int sceneID)
    {
        instance.StartCoroutine(instance.ScreenTransitionAnimation(sceneID));
    }

    private void ChangeTimeScale(float timeCheck, float falseTime, float trueTime = 1f)
    {
        Time.timeScale = timeCheck == 0 ? trueTime : falseTime;
    }

    private IEnumerator ScreenTransitionAnimation(int sceneID)
    {
        while (true)
        {
            ChangeTimeScale(1f, 1f);

            screenTransitionAnimator.SetTrigger(exitScene);
            yield return waitTime;

            SceneManager.LoadScene(sceneID);

        }
    }
}
