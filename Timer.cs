using System;
using UnityEngine;
using TMPro;


//This script will countdown the timer when player enter the gameplay scene, when the time is up, the script will sent message to all scripts that has subscribed to it

public class Timer : MonoBehaviour
{
    #region Variables

    private TMP_Text _timerText;    

    [SerializeField] private float timeToDisplay = 60.0f;

    private bool _isRunning;

    #endregion
    
    private void Awake() => _timerText = GetComponent<TMP_Text>();

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;        
    }

    private void OnDisable()
    {
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
        
    }

    private void EventManagerOnTimerStart() => _isRunning = true;
    private void EventManagerOnTimerStop() => _isRunning = false;    
    
    private void Update()
    {
        //////////////////////////
        //countdown game timer///
        /////////////////////////
        if (!_isRunning) return;
        if (timeToDisplay < 0.0f)
        {
            EventManager.OnTimerStop();
            return;
        }        
        
        timeToDisplay -= Time.deltaTime;        

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay); // represent timeToDisplay in seconds with timeSpan variable
        _timerText.text = timeSpan.ToString(@"mm\:ss\:ff"); //display time in minute:second:frame form        

    }
}
