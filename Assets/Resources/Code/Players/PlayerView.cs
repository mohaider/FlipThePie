
using UnityEngine;

namespace Assets.Resources.Code.Players
{
    public class PlayerView : MonoBehaviour
    {
        private GameObject _cameraButtonGameObj;
        private Animator _cameraButtonAnimator;
        private GameObject _yesNoRetakePhotoBGameObject;
        private Animator _yesNoRetakePhotoAnimator;
        private GameObject _flipCamButtonGameObject;
        private Animator _flipCamAnimator;


        public GameObject CameraButtonGameObj
        {
            get
            {
                if (_cameraButtonGameObj == null)
                {
                    _cameraButtonGameObj = GameObject.FindGameObjectWithTag("CameraButton");
                }
                return _cameraButtonGameObj;
            }
            set { _cameraButtonGameObj = value; }
        }

        public Animator CameraButtonAnimator
        {
            get
            {
                if (_cameraButtonAnimator == null)
                {
                    _cameraButtonAnimator = CameraButtonGameObj.GetComponent<Animator>();
                }
                return _cameraButtonAnimator;
            }
        }

        public GameObject YesNoRetakePhotoBGameObject
        {
            get
            {
                if (_yesNoRetakePhotoBGameObject == null)
                {
                    _yesNoRetakePhotoBGameObject = GameObject.FindGameObjectWithTag("RetakePhotoOption");
                }
                return _yesNoRetakePhotoBGameObject;
            }
        }

        public Animator YesNoRetakePhotoAnimator
        {
            get
            {
                if (_yesNoRetakePhotoAnimator == null)
                {
                    _yesNoRetakePhotoAnimator = YesNoRetakePhotoBGameObject.GetComponent<Animator>();
                }
                return _yesNoRetakePhotoAnimator;
            }
        }

        public GameObject FlipCamButtonGameObject
        {
            get
            {
                if (_flipCamButtonGameObject == null)
                {
                    _flipCamButtonGameObject = GameObject.FindGameObjectWithTag("FlipCamera");
                }
                return _flipCamButtonGameObject;
            }
        }

        public Animator FlipCamAnimator
        {
            get
            {
                if (_flipCamAnimator == null)
                {
                    _flipCamAnimator = FlipCamButtonGameObject.GetComponent<Animator>();
                }
                return _flipCamAnimator;
            }
        }

        internal void ShowCameraButton(bool status)
        {
            CameraButtonAnimator.enabled = true;
            CameraButtonAnimator.SetBool("isHidden", status); 
        }


        internal void ShowRetakePhotoOptions(bool status)
        {
            YesNoRetakePhotoAnimator.enabled = true;
            YesNoRetakePhotoAnimator.SetBool("isHidden", status);
        }

        internal void ShowFlipCamButton(bool status)
        {
            FlipCamAnimator.enabled = true;
            FlipCamAnimator.SetBool("isHidden",status);
        }
        
        private void Awake()
        {
            CameraButtonAnimator.enabled = false;
            YesNoRetakePhotoAnimator.enabled = false;
            FlipCamAnimator.enabled = false;
        }
    }
}
