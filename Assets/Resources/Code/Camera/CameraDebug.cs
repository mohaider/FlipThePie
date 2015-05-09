using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.Camera
{
    public class CameraDebug:MonoBehaviour
    {
        private AndroidCameraController _androidCameraController;
        public CameraController Controller;
        public bool DebugMode;
        public UnityEngine.UI.Text Texter; 
        public AndroidCameraController CameraController
        {
            get
            {
                if (_androidCameraController == null)
                {
                    _androidCameraController = GetComponent<AndroidCameraController>();
                }
                return _androidCameraController;
            }
            set { _androidCameraController = value; }
        }

        void Start()
        {
         
        }
        void Update()
        {
            if (DebugMode)
            {
                CheckInputs();
             
            }
          
        }

        void UpdateDebuggerText()
        {
            string s = "current device: " + CameraController.DeviceName;
            s += "\n current device count = "+ CameraController.Devices.Length;
            Texter.text = s;
        }

        private void CheckInputs()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
              Controller.ChangeState(CameraControlState.TakePicture);
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                Controller.ChangeState(CameraControlState.AskIfPictureIsOk);
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                Controller.ChangeState(CameraControlState.LoadPicture);
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                Controller.ChangeState(CameraControlState.HideEverything);
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Controller.ChangeState(CameraControlState.SavePicture);
            }


        }

        public void DoStuff()
        {
            print("button pressed");

        }
        
    }
}
