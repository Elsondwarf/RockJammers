using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Main Menu Music")]
    [field: SerializeField] public EventReference mainMenuMusic { get; private set; }
    
    [field: Header("Button Hover")]
    [field: SerializeField] public EventReference buttonHover { get; private set; }
    
    [field: Header("Thrust Sound")]
    [field: SerializeField] public EventReference thrustSound { get; private set; }
    
    [field: Header("Pickup Sound")]
    [field: SerializeField] public EventReference pickupSound { get; private set; }
    
    [field: Header("Exit Scene")]
    [field: SerializeField] public EventReference exitScene { get; private set; }
    

    public static FMODEvents Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.LogError("More then one instance of AudioManager!");
        }
        Instance = this;
    }
}
