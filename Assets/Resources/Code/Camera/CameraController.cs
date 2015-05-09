using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.Camera
{
    public enum CameraControlState
    {
        TakePicture,
        AskIfPictureIsOk,
        LoadPicture,
        SavePicture,
        HideEverything
    }
    [RequireComponent(typeof (CameraModel))]
  
    public class CameraController: MonoBehaviour
    {
        public delegate void CameraStateHandler(CameraControlState newState);

        public delegate void CameraSwitchStateHandler();

        public event CameraSwitchStateHandler ChangePoV;

        public event CameraStateHandler OnStateChange;
        public void ChangeState(CameraControlState newState)
        {
            if (OnStateChange != null)
            {
                OnStateChange(newState);
            }
        }

        public void ChangePointOfView()
        {
            if (ChangePoV != null)
            {
                ChangePoV();
            }
        }

        public void SwitchCamera()
        {
            
        }
    }
}
