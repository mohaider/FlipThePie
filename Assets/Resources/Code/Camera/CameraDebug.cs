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
            }
        }

        private void CheckInputs()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                CameraController.SavePicture();
            }
        }

        public void DoStuff()
        {
            print("button pressed");
        }
        
    }
}
