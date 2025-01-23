using UnityEngine;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{

    public void NewGame()
    {
        GameManager.LoadNewArea(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
