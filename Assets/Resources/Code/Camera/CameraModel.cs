using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Resources.Code.GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Code.Camera
{
    public enum CameraViewState
    {
        ShowStream = 0,
        ShowPicture = 1,
        ShowPictureAndValidate = 2,
        ShowNothing = 3
    }
    [RequireComponent(typeof(CameraView))]
    public class CameraModel : MonoBehaviour
    {
        private GameDirector _director;
        private CameraView _view;
        private CameraController _camController;
        private string _path;
        private CameraControlState _currentState;
        private string _currentDeviceName;
        private WebCamTexture _cameraTexture;
        private WebCamDevice[] _devices;
        private int _currentDeviceIndex;

        private Texture2D _snapShot; //will be used to store snapshot


        public WebCamTexture CameraTexture
        {
            get
            {
                if (_cameraTexture == null)
                {

                    _cameraTexture = View.CameraTexture;


                }
                return _cameraTexture;
            }
            set { _cameraTexture = value; }
        }

        public WebCamDevice[] Devices
        {
            get
            {
                if (_devices == null)
                {
                    if (WebCamTexture.devices.Length == 0)
                    {
                        throw new NoCameraFoundException();
                    }

                    _devices = WebCamTexture.devices;
                }
                return _devices;
            }

        }
        public Texture2D SnapShot
        {
            get
            {
                if (_snapShot == null)
                {
                    _snapShot = new Texture2D(CameraTexture.width, CameraTexture.height);
                }
                return _snapShot;
            }
            set { _snapShot = value; }
        }

        public string DeviceName
        {
            get
            {
                if (_currentDeviceName == null)
                {
                    _currentDeviceName = Devices[_currentDeviceIndex].name;

                }
                _currentDeviceName = Devices[_currentDeviceIndex].name;
                return _currentDeviceName;
            }
            set { _currentDeviceName = value; }
        }


        public CameraView View
        {
            get
            {
                if (_view == null)
                {
                    _view = GetComponent<CameraView>();
                }
                return _view;
            }
            set { _view = value; }
        }

        public CameraController CamController
        {
            get
            {
                if (_camController == null)
                {
                    _camController = GetComponent<CameraController>();
                }
                return _camController;
            }
            set { _camController = value; }
        }

        public GameDirector Director
        {
            get
            {
                if (_director == null)
                { _director = GameDirector.Instance; }
                return _director;
            }

        }

        public string PicturePath
        {
            get
            {
                _path = Application.persistentDataPath + "/" + Director.CurrentPlayer.PlayerName+".png";
                return _path;
            }
            set { _path = value; }
        }

        public void SaveSnapShot(string path)
        {
            SnapShot = new Texture2D(CameraTexture.width, CameraTexture.height);
            SnapShot.SetPixels(View.CameraTexture.GetPixels());
            SnapShot.Apply();
            System.IO.File.WriteAllBytes("Assets/Resources/SaveData/Snapshots/test1.png", SnapShot.EncodeToPNG());
        }
/*        public void LoadPicture(string path, Action<bool> callbackAction)
        {
            try
            {
                CameraTexture.Stop();
                if (SnapShot.LoadImage(File.ReadAllBytes(path)))
                {
                    RawImage1.material.mainTexture = SnapShot;
                    callbackAction(true);
                }

            }
            catch (FileNotFoundException e)
            {
                callbackAction(false);
            }

        }*/

        public void SavePicture(string path)
        {
            SnapShot = new Texture2D(CameraTexture.width, CameraTexture.height);
            SnapShot.SetPixels(CameraTexture.GetPixels());
            SnapShot.Apply();
            System.IO.File.WriteAllBytes(path, SnapShot.EncodeToPNG());
        }
   

        public void OnEnable()
        {
            CamController.OnStateChange += OnStateChange;
            CamController.ChangePoV += ChangeCameraPov;
        }

        public void OnDisable()
        {
            CamController.OnStateChange -= OnStateChange;
            CamController.ChangePoV -= ChangeCameraPov;
        }

        public void ChangeCameraPov()
        {
           View.SwitchCamera(); 
        }
        public void OnStateChange(CameraControlState newState)
        {
            switch (newState)
            {
                case CameraControlState.AskIfPictureIsOk:
                {
                   
                        View.SwitchToValidationView();
                        break;
                    }

                    case CameraControlState.SavePicture:
                {
                    SavePicture(PicturePath);
                    View.SwitchToValidationView();
                    break;
                }
                case CameraControlState.TakePicture:
                    {
                        
                        View.SwitchToCameraTakingView();
                        break;
                    }

                case CameraControlState.LoadPicture:
                {
                    View.SwitchToLoadPictureView();
                    break;
                }
                    case CameraControlState.HideEverything:
                {
                    View.HideEverything();
                    break;
                }



            }
        }

        private void LoadPicture(string PicturePath)
        {
            throw new NotImplementedException();
        }

    }
}
