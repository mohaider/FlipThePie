using UnityEngine;
using System.Collections;
using Assets.Resources.Code.GameLogic;

public class debugger : MonoBehaviour
{
    public bool DebugMode;
    public GameDirector Gm; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (DebugMode)
	        checkInput();
	}

    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Gm.NotifyWindow("in debug mode", "director");
        }
    }
}
