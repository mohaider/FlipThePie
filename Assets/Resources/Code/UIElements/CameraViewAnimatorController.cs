using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.UIElements
{
    public class CameraViewAnimatorController: MonoBehaviour
    {
     public GameObject CameraSwitchButton;
        private Animator _animator;
        private string hideCameraFrame = "hideCameraFrame";
        private string showCameraControls = "showCameraControls";
        private string showValidationOptions = "showValidationOptions";
        private string showCameraFrame = "showCameraFrame";
        public Animator AnimatorController
        {
            get
            {
                if (_animator == null)
                {
                    _animator = GetComponent<Animator>();
                }
                return _animator;
            }
        }

        public void PictureTakingMode(bool status)
        {
            if (status)
            {
                AnimatorController.enabled = true;
            }
            AnimatorController.SetBool("isHidden", status);
        }


        public void PictureTakingModeOn()
        {

            AnimatorController.enabled = true;

            AnimatorController.SetBool(showCameraFrame, true);
            AnimatorController.SetBool(showCameraControls, true);
            AnimatorController.SetBool(showValidationOptions, false);
        }
        public void PictureTakingModeOffWithCameraFrame()
        {

            AnimatorController.enabled = true;

            AnimatorController.SetBool(showCameraFrame, true);
            AnimatorController.SetBool(showCameraControls, false);
            AnimatorController.SetBool(showValidationOptions, false);
        }

        public void HideEverything()
        {

            AnimatorController.enabled = true;

            AnimatorController.SetBool(showCameraFrame, false);
            AnimatorController.SetBool(showCameraControls, false);
            AnimatorController.SetBool(showValidationOptions, false);
        }

        public void ShowValidationView()
        {

            AnimatorController.enabled = true;

            AnimatorController.SetBool(showCameraFrame, true);
            AnimatorController.SetBool(showCameraControls, false);
            AnimatorController.SetBool(showValidationOptions, true);
        }
        public void HideValidationView()
        {

            AnimatorController.enabled = true;

            AnimatorController.SetBool(showCameraFrame, true);
            AnimatorController.SetBool(showCameraControls, false);
            AnimatorController.SetBool(showValidationOptions, false);
        }


    }
}
