using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

//this script is mostly about gameplay of the game

public class Stack_Manager : Subject
{
    //text variables
    public TMP_Text code_txt;
    public TMP_Text log_txt;
    public TMP_Text point_txt;

    [SerializeField]
    private int goalPoint;

    private int[] codeArray = new int[4]; // store the generated password
    private int[] fillArray = new int[4]; // store the password that player fill in    

    private int num;

    public Points point;    

    private int pos = 0;    

    public GameObject[] digitSignal = new GameObject[4];

    //other variables    
    public GameObject startingPoint;
    private Vector2 tempStartingPoint;

    public GameObject sObject;    
    private float xOffset = 2;
    public Stack<GameObject> stackObject = new Stack<GameObject>();

    //this function will be attached to Add button
    public void Push()
    {  
        // prevent player enter more than 6 bar
        if (stackObject.Count < 6)
        {
            // create new bar 
            GameObject newObject = Instantiate(sObject);
            // set position for new bar
            newObject.transform.parent = this.transform;
            newObject.transform.position = new Vector3(startingPoint.transform.position.x + xOffset, startingPoint.transform.position.y);
            // add new bar to the stack
            stackObject.Push(newObject);
            // translate offset
            xOffset += 2;

            numUpdate();

            //play add sound
            NotifyObserver(PlayerAction.add);
            Debug.Log(stackObject.Count);
        }
        else
        {
            //update log text
            log_txt.text = "the password uses number between 0-6";

            //play error sound
            NotifyObserver(PlayerAction.error);
            Debug.Log(stackObject.Count);
        }
        
    }

    //this function will be attached to Remove button
    public void Pop()
    {        
        // prevent player to keep remove when there's no bar left
        if (stackObject.Count > 0)
        {
            //destroy bar
            GameObject popObject = stackObject.Pop();
            xOffset -= 2;
            Destroy(popObject);

            numUpdate();

            //play remove sound
            NotifyObserver(PlayerAction.remove);
            Debug.Log(stackObject.Count);
        }
        else
        {
            //update log text
            log_txt.text = "the password uses number between 0-6";

            //play error sound
            NotifyObserver(PlayerAction.error);
            Debug.Log(stackObject.Count);
        }
        
    }

    // destroy all the bar from previous play
    public void clearBar()
    {
        foreach (GameObject barClone in stackObject)
        {               
           Destroy(barClone);
           Debug.Log("DESTROY");
            
        }
        stackObject.Clear();
        numUpdate();
    }


    // changing color sign to red, show that those digit still empty
    public void cleanStack()
    {
        pos = 0;       

        for (int i = 0; i < digitSignal.Length; i++)
        {
            digitSignal[i].GetComponent<Image>().color = Color.red;
        }

        xOffset = 2;  

    }
    
    // This function is linked to the redo button in the scene to clear both bar and digit in the scene
    public void Redo()
    {
        cleanStack();
        clearBar();
        NotifyObserver(PlayerAction.redo);
    }
        
           
    // This function will check if player enters password correct or not.
    public void Login()
    {
        bool areEqual = fillArray.SequenceEqual(codeArray); // use method from Linq to check if two arrays are equal or not

        if (areEqual)
        {
            log_txt.text = "you correcly enter you password, but it's correcly wrong. try again";
            
            point.points++; // increase player's point
            point_txt.text = point.points.ToString();

            //if players's point reach to the goal, they win the game. 
            if (point.points >= goalPoint)
            {
                SceneManager.LoadScene("VictoryScene");
            }

            randomCode();
            NotifyObserver(PlayerAction.correct); // play correct sound

            // reset bar and digit
            cleanStack();
            clearBar();
        }
        else
        {
            log_txt.text = "wrong password, please try again";
            
            NotifyObserver(PlayerAction.wrong); // play wrong sound
            // reset bar and digit
            cleanStack();
            clearBar();
        }       
    }

    
    // attach to NextDigit Button
    public void NextDigit()
    {   
        // prevent players to go beyond 4 digit of password
        if (pos <= 3)
        {
            fillArray[pos] = num; // assign value in array with the number that players have entered.
            NotifyObserver(PlayerAction.degit); // play a sound
            Debug.Log("Spos = " + pos);
            Debug.Log("your digit number " + (pos+1) + " is " + fillArray[pos]);
            pos++;                      
            
        }
        else
        {
            //play error sound
            NotifyObserver(PlayerAction.error);
            log_txt.text = "you are at the final degit already!";
            //pos = 4;
        }

        digitSignal[pos - 1].GetComponent<Image>().color = Color.green; // change digit signal color to green, showing that the digit has been filled
        
    }


    //update num equal to total bars player has added
    void numUpdate() => num = stackObject.Count;

    //generate new members for codeArray
    public void randomCode()
    {
        for (int i = 0; i < codeArray.Length; i++)
        {
            codeArray[i] = Random.Range(0, 6);
        }

        //display code text on screen
        code_txt.text = codeArray[0] + " - " + codeArray[1] + " - " + codeArray[2] + " - " + codeArray[3];
    }

    private void Start()
    {
        tempStartingPoint = startingPoint.transform.position; //assign starting point to create the first bar when player adds.
        point.points = 0; //set the point to start at 0
        log_txt.text = "please enter your password"; //show text to inform player what to do
        randomCode(); // random the code when the game start        
    }


}
