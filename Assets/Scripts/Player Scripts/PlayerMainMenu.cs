using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainMenu : MonoBehaviour
{

    public PlayerControls inputs;
    public float speed = 10;
    public static PlayerMainMenu instance;

    private Vector2 controllerLook;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();


    [SerializeField]
    private float forceMultipler = 1f;
    [SerializeField]
    private GameObject air;

    [SerializeField] private ParticleSpawner spawner;
    [SerializeField] private AudioSource audioSource;

    //private Vector2 mousePosition;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            //isInstance = true;
        }
        else
        {
            //Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);

        inputs = new PlayerControls();
        inputs.Gameplay.Look.performed += ctx => controllerLook = ctx.ReadValue<Vector2>();
        inputs.Gameplay.Look.canceled += ctx => controllerLook = Vector2.zero;
        inputs.UI.Back.performed += ctx => PauseMenu.instance.BackButtonPressed();

    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    void Update()
    {
        Rotate();
        Propel();
    }

    private void Rotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mp = new Vector3
        {
            x = ray.origin.x + controllerLook.x,
            y = ray.origin.y,
            z = ray.origin.z + controllerLook.y
        };


        Vector3 direction = mp - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);

        targetRotation.x = 0f;
        targetRotation.y = 0f;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * 100 * Time.deltaTime);

        Debug.Log(mp);

        spawner.SpawnParticle(Vector3.right, forceMultipler);

    }

    private void Propel()
    {

        rb.AddRelativeForce(new Vector2(0f, -1f) * forceMultipler);

        //AUDIO SORCE
        audioSource.Play();

        air.SetActive(true);
        //StartCoroutine(ShowAir());
    }

}
