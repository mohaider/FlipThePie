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
            if (DebugMode)
            {
                CameraController.DisplayCameraStream();
            }
        }
        void Update()
        {
            if (DebugMode)
            {
                CheckInputs();
                UpdateDebuggerText();
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
                CameraController.SavePicture();
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                CameraController.DisplayCameraStream();
            }

        }

        public void DoStuff()
        {
            print("button pressed");

        }
        
    }
}
