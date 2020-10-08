using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameHandler.Start");
        int i=0;
        FunctionPeriodic.Create(() => { 
            CMDebug.TextPopupMouse("Waffles" + i + "!"); 
            i++;
        }, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
