using UnityEngine;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void NewGame()
    {
        GameManager.LoadNewArea(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
