using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// this script is for showing the point players get in the game over scene
public class ShowPoint : MonoBehaviour
{
    public Points point;

    public TMP_Text point_txt;

    // Start is called before the first frame update
    void Start()
    {
        point_txt.text = point.points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
