using UnityEngine;
using Assets.Resources.Code.Camera;
using UnityEngine.UI;


namespace Assets.Resources.Code.Players
{
    public class Player : MonoBehaviour
    {
        private const string Prependpath = "Assets/Resources/SaveData/SnapShots/";
        private string _snapShotPath;
        private GameObject _cameraButtonGO;
        private bool _hasPicture = false;
        private AndroidCameraController _cameraController;


        public AndroidCameraController CameraController
        {
            get
            {
                if (_cameraController == null)
                {
                    GameObject camObj = GameObject.FindGameObjectWithTag("Camera");
                    _cameraController = camObj.GetComponent<AndroidCameraController>();
                }

                return _cameraController;
            }
            set { _cameraController = value; }
        }

        public string SnapShotPath
        {
            get
            {
                if (_snapShotPath == null)
                {
                    _snapShotPath = Prependpath + name + ".png";
                }

                return _snapShotPath;
            }
        }

        public GameObject CameraButtonGoGo
        {
            get
            {
                if (_cameraButtonGO == null)
                {
                    _cameraButtonGO = GameObject.FindGameObjectWithTag("CameraButton");
                  
                }
                return _cameraButtonGO;
            }
        }

        public bool HasPicture
        {
            get
            {
                _hasPicture = System.IO.File.Exists(SnapShotPath);
                return _hasPicture;
            }
            set { _hasPicture = value; }
        }

        public void PictureFound(bool isSuccessfull)
        {
            if (isSuccessfull)
            {
                //todo: implement continue method to game
            }
            else
            {
                ShowSnapShotButton();
            }
        }

        public void TryToLoadPlayerPicture()
        {
            CameraController.LoadPicture(SnapShotPath, PictureFound);
        }

        private void ShowSnapShotButton()
        {
            ToggleCameraButton(true);
            CameraController.DisplayCameraStream();
        }

        public void TakeSnapShot()
        {
            ToggleCameraButton(false);
            CameraController.SavePicture(SnapShotPath);
            CameraController.HideCameraStream();
            TryToLoadPlayerPicture();
            AskIfPictureIsOK();
        }

        private void AskIfPictureIsOK()
        {

        }

        private void ToggleCameraButton(bool toggleSwitch)
        {
            CameraButtonGoGo.SetActive(toggleSwitch); 
        }

        private void Start()
        {
            TryToLoadPlayerPicture();
        }
}
}
