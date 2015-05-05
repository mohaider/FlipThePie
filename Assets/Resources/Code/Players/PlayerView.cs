
using System;
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
        private GameObject _playerHeadMoverGameObject;
        private Animator _playerHeadMoverAnimator;

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

        public GameObject PlayerHeadMoverGameObject
        {
            get
            {
                if (_playerHeadMoverGameObject == null)
                {
                    _playerHeadMoverGameObject = GameObject.FindGameObjectWithTag("PlayerHeadMover");
                }
                return _playerHeadMoverGameObject;
            }
        }

        public Animator PlayerHeadMoverAnimator
        {
            get
            {
                if (_playerHeadMoverAnimator == null)
                {
                    _playerHeadMoverAnimator = PlayerHeadMoverGameObject.GetComponent<Animator>();
                }
                return _playerHeadMoverAnimator;
            }
        }

        internal void ShowCameraButton(bool status)
        {
            if (status)
            {
                CameraButtonAnimator.enabled = status;
            }
            CameraButtonAnimator.SetBool("isHidden", !status); 
        }


        internal void ShowRetakePhotoOptions(bool status)
        {
            if (status)
            {
                YesNoRetakePhotoAnimator.enabled = status;
            }
            YesNoRetakePhotoAnimator.SetBool("isHidden", !status);
        }

        internal void ShowFlipCamButton(bool status)
        {
            if (status)
            {
                FlipCamAnimator.enabled = status;
            }
            FlipCamAnimator.SetBool("isHidden",!status);
        }

        internal void ShowSettingsButton(bool status)
        {
           Debug.Log("Fill up this code");
        }

        internal void ShowPlayerHead(bool status)
        {
            if (status)
            {
                EnableAnimator(PlayerHeadMoverAnimator,status);
            }
            PlayerHeadMoverAnimator.SetBool("isHidden",!status);
        }
        internal void EnableAnimator(Animator anim, bool status)
        {
            anim.enabled = status;
        }
        
        
        private void Awake()
        {
            CameraButtonAnimator.enabled = false;
            YesNoRetakePhotoAnimator.enabled = false;
            FlipCamAnimator.enabled = false;
        }
    }
}
