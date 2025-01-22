using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerControls inputs;
    public float speed = 10;
    public static PlayerMovement instance;
    public bool isInstance = false;

    private Vector2 controllerLook;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();

    private PlayerAir playerAir => GetComponent<PlayerAir>();

    

    [SerializeField]
    private float forceMultipler = 1f;
    [SerializeField]
    private GameObject air;

    //private Vector2 mousePosition;

    private void Awake()
    {

        if(instance == null)
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
        //mousePosition = inputs.Gameplay.Look.ReadValue<Vector2>();
        inputs.Gameplay.Look.performed += ctx => controllerLook = ctx.ReadValue<Vector2>();
        inputs.Gameplay.Look.canceled += ctx => controllerLook = Vector2.zero;
        inputs.Gameplay.Propel.performed += ctx => Propel(ctx);


    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
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
    }

    private void Propel(InputAction.CallbackContext contex)
    {
        if (playerAir.GetCurrentAir() <= 0)
        {
            //play a pathetic air particle and a sad sound
            return;
        }

        //playerAir.ChangeAirAmount(-5);

        rb.AddRelativeForce(new Vector2(0f,-1f) * forceMultipler);
        StartCoroutine(ShowAir());
    }

    private IEnumerator ShowAir()
    {
        air.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        air.SetActive(false);
    }

}
