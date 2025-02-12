using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script will countdown time and random chance that system will change hint in the game. 
public class RandomBugTimer : Subject
{
    bool isOver;

    [SerializeField]
    private float countdownTimeFill;
    
    private float countdownTime;

    [SerializeField]
    private int[] numForRan = new int[] {1, 2};    

    private bool isCountingDown = false;

    public Stack_Manager stackScript;

    // Start is called before the first frame update
    void Start()
    {
        StartCountdown(); // Start the countdown when the game starts
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountingDown)
        {            
            countdownTime -= Time.deltaTime; // Decrement countdown time
            if (countdownTime <= 0)
            {
                isCountingDown = false; // Stop the countdown

                int luckyNum = Random.Range(0, numForRan.Length) % 2; //random number to calculate for auto random rate
                int huay = luckyNum % 2;

                //randomCode will work when huay is divisible by 2
                if (luckyNum == 0)
                {
                    stackScript.randomCode();
                    //play sound
                    NotifyObserver(PlayerAction.bug);                                  
                }
                StartCountdown();               
                
            }
            
        }
    }

    // Start the countdown with a random duration
    void StartCountdown()    {
        
        isCountingDown = true;
        countdownTime = countdownTimeFill;
        
    }
}
