using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string mainLevel = "MainScene";
    public string menuLevel = "MainMenu";

    public Text pizzasTxt;
    public PlayerMovement player;
    public Text livesTxt;
    public Text lastRecordTxt;
    public Text lastKillsRecordTxt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Screen.SetResolution(640, 480, true);
        lastRecordTxt.text = PlayerPrefs.GetInt("LastPizzasRecord").ToString();
        lastKillsRecordTxt.text = PlayerPrefs.GetInt("LastKillsRecord").ToString();
        // PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        pizzasTxt.text = player.pizzasCollected.ToString();
        livesTxt.text = player.lives.ToString();
        lastRecordTxt.text = PlayerPrefs.GetInt("LastPizzasRecord").ToString();
        lastKillsRecordTxt.text = PlayerPrefs.GetInt("LastKillsRecord").ToString();
        if(Input.GetKeyDown(KeyCode.F))
        {
            FullScreen();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }
    
    void FullScreen(){
        Screen.fullScreen = !Screen.fullScreen;
    }    
    public void Restart(){
        SceneManager.LoadScene(mainLevel);
    }

    public void Quit(){
        Application.Quit();
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(menuLevel);
    }
}
