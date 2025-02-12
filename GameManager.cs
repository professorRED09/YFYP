using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//This script will manage almost all scenes in the game except victory scene.

//The script is subscribing to timer script, so when the time is up, it will trigger a specific event like showing game over scene

public class GameManager : MonoBehaviour
{  

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnTimerStart();
    }   

    public void StartButton()
    {
        SceneManager.LoadScene("Select");
    }
    public void StartEasyButton()
    {
        SceneManager.LoadScene("GamePlaySceneEasy");
    }
    public void StartNormalButton()
    {
        SceneManager.LoadScene("GamePlayScene");
    }
    public void StartHardButton()
    {
        SceneManager.LoadScene("GamePlaySceneHard");
    }

    public void TryAgainButton()
    {
        SceneManager.LoadScene("Select");
    }

    public void TutorialButton()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("IntroScene");
    }
    public void RealExitButton()
    {
        Application.Quit();

    }

    private void OnEnable()
    {
        EventManager.TimerStop += EventManagerOnTimeStop;
    }

    private void OnDisable()
    {
        EventManager.TimerStop -= EventManagerOnTimeStop;
    }

    void EventManagerOnTimeStop()
    {       
        Debug.Log("GameOVER");            
        SceneManager.LoadScene("GameOverScene");                     
    }
    
}
