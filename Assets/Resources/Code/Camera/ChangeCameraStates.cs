using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.Camera
{
    public class ChangeCameraStates :MonoBehaviour
    {
        private CameraController _camController;

        public CameraController CamController
        {
            get
            {
                if (_camController == null)
                {
                    GameObject obj = GameObject.FindGameObjectWithTag("CameraStream");
                    _camController = obj.GetComponent<CameraController>();
                }
                return _camController;
            }
        }

        public void TakePicture()
        {
            CamController.ChangeState(CameraControlState.SavePicture);
            CamController.ChangeState(CameraControlState.AskIfPictureIsOk);
        }

        public void RetakePicture()
        {
            CamController.ChangeState(CameraControlState.TakePicture);
        }
    }
}
