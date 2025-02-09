using UnityEngine;
using UnityEngine.UI;

public class PlayerAir : MonoBehaviour
{
    public float currentAir;

    [SerializeField] public Slider airSlider;
    [SerializeField] public Image sliderImage;
    [SerializeField] public readonly float maxAir = 100;
    [SerializeField] private float airReduceAmount = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        airSlider.maxValue = maxAir;
        //currentAir = maxAir;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAirAmount(-airReduceAmount * Time.deltaTime);
    }

    public void UpdateUI()
    {
        airSlider.value = currentAir;
        sliderImage.color = Color.Lerp(Color.red, Color.blue, currentAir / maxAir);
    }


    public void ChangeAirAmount(float amount)
    {
        currentAir += amount;

        if (currentAir > maxAir)
            currentAir = maxAir;

        if (currentAir <= 0)
            currentAir = 0;

        UpdateUI();
    }

    public float GetCurrentAir()
    {
        return currentAir;
    }
}
