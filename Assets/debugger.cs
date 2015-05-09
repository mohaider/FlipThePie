using UnityEngine;
using System.Collections;
using Assets.Resources.Code.GameLogic;
using Assets.Resources.Code.GameLogic.Level;
using Assets.Resources.Code.GameLogic.Level.GameStart;

public class debugger : MonoBehaviour
{
    public bool DebugMode;
    public GameDirector Gm;
    public BaseLevelController _Controller;
    
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
        if (Input.GetKeyDown(KeyCode.F2))
        {
            _Controller.ChangeState(StartLevelState.GameStart);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            _Controller.ChangeState(StartLevelState.GameModeSelect);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            _Controller.ChangeState(StartLevelState.PlayerIconSelect);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            _Controller.ChangeState(StartLevelState.CameraViewState);
        }
        
    }
}
