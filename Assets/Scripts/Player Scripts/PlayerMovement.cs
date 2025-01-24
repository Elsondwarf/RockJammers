using System;
using System.Collections;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerControls inputs;
    public float rotationSpeed = 10;
    public static PlayerMovement instance;

    private Vector2 controllerLook;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();

    private PlayerAir playerAir => GetComponent<PlayerAir>();

    private float moveinput;

    [SerializeField]
    private float forceMultipler = 1f;
    [SerializeField]
    private GameObject air;
    [SerializeField] private float airUseSpeed = 0.05f;
    [SerializeField] private ParticleSpawner spawner;
    [SerializeField] private AudioSource movingAudio;
    [SerializeField] private AudioSource collisionAudio;
    
    // Audio
    private EventInstance thrustInstance;
    private EventInstance noThrustInstance;
    private bool isThrusting = false;


    //private Vector2 mousePosition;

    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
            //isInstance = true;
        }

        inputs = new PlayerControls();
        //mousePosition = inputs.Gameplay.Look.ReadValue<Vector2>();
        inputs.Gameplay.Look.performed += ctx => controllerLook = ctx.ReadValue<Vector2>();
        inputs.Gameplay.Look.canceled += ctx => controllerLook = Vector2.zero;
        inputs.Gameplay.Propel.performed += ctx => moveinput = ctx.ReadValue<float>();// Propel(ctx);
        inputs.Gameplay.Propel.canceled += ctx => air.SetActive(false); 
        inputs.Gameplay.Propel.canceled += ctx => moveinput = 0f;

        inputs.UI.Pause.performed += ctx => PauseMenu.instance.PauseGame();
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

    private void Start()
    {
        thrustInstance = AudioManager.Instance.CreateEventInstance(FMODEvents.Instance.thrustSound);
        noThrustInstance = AudioManager.Instance.CreateEventInstance(FMODEvents.Instance.noThrustSound);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        isThrusting = false;
        Propel();
        UpdateSound();
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

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * 100 * Time.deltaTime);
    }

    private void Propel()
    {
        if (moveinput == 0)
            return;


        if (playerAir.GetCurrentAir() <= 0)
        {
            noThrustInstance.getPlaybackState(out var state);
            if (state.Equals(PLAYBACK_STATE.STOPPED))
            {
                noThrustInstance.start();
            }
            //play a pathetic air particle and a sad sound
            return;
        }
        
        isThrusting = true;

        playerAir.ChangeAirAmount(-airUseSpeed);

        rb.AddRelativeForce(new Vector2(0f,-1f) * forceMultipler * moveinput);
        spawner.SpawnParticle(transform.up, forceMultipler * moveinput * 100);

        air.SetActive(true);
    }

    private void UpdateSound()
    {
        if (isThrusting)
        {
            thrustInstance.getPlaybackState(out PLAYBACK_STATE playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                thrustInstance.start();
            }
        }
        else
        {
            thrustInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

    private IEnumerator ShowAir()
    {
        air.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        air.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.thudSound, transform.position);
            //AUDIO SOURCE
            //collisionAudio.Play();

        }

    }
}
