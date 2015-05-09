using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Resources.Code.GameLogic;
using Assets.Resources.Code.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Code.Camera
{
    public class CameraView:MonoBehaviour
    {
        private CameraViewAnimatorController _cameraAnimatorController;
        public GameObject SwitchButton;
        private GameDirector _director;
        private Texture2D _snapShot;
        private string _loadPath;
        private RawImage _rawImage; //will be used to display pictures and streams on screen
        private WebCamTexture _cameraTexture;
        private WebCamDevice[] _devices;
        private string _deviceName;
 
        private int _currentDeviceIndex;
        private Quaternion baseRotation;
        public string DeviceName
        {
            get
            {
                _deviceName = Devices[_currentDeviceIndex].name;
                return _deviceName;
            }
            set { _deviceName = value; }
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

        public WebCamTexture CameraTexture
        {
            get
            {
                if (_cameraTexture == null)
                {

                    _cameraTexture = new WebCamTexture(DeviceName);


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
        public RawImage RawImage1
        {
            get
            {
                if (_rawImage == null)
                {
                    GameObject obj = GameObject.FindGameObjectWithTag("CameraStream");
                    _rawImage = obj.GetComponent<RawImage>();
                }
                return _rawImage;
            }
            set { _rawImage = value; }
        }

        public GameDirector Director
        {
            get
            {
                if (_director == null)
                {
                    _director = GameDirector.Instance;
                }
                return _director;
            }
            set { _director = value; }
        }

        public string LoadPath
        {
            get
            {
                _loadPath = Application.persistentDataPath+"/"+Director.CurrentPlayer.PlayerName+".png";
                return _loadPath;
            }
           
        }

        public CameraViewAnimatorController CameraAnimatorController
        {
            get
            {
                if (_cameraAnimatorController == null)
                {
                    GameObject camAnimGameObject = GameObject.FindGameObjectWithTag("Camera");
                    _cameraAnimatorController = camAnimGameObject.GetComponent<CameraViewAnimatorController>();
                }
                return _cameraAnimatorController;
            }
            set { _cameraAnimatorController = value; }
        }

        public void SwitchCamera()
        {
         
           CameraTexture.Stop();
                _currentDeviceIndex++;
                if (_currentDeviceIndex >= Devices.Length)
                {
                    _currentDeviceIndex = 0;
                }
                CameraTexture.deviceName = DeviceName;
                //SetGUITexture();
                ShowStream();
                
            
            //DeviceName = Devices[_currentDeviceIndex].name;

        }




        internal void LoadPicture()
        {
            try
            {
                CameraTexture.Stop();
                if (SnapShot.LoadImage(File.ReadAllBytes(LoadPath)))
                {
                    
                    RawImage1.material.mainTexture = SnapShot;
                    RawImage1.texture = SnapShot;


                }

            }
            catch (FileNotFoundException e)
            {
                Debug.Log("cannot find picture: " + LoadPath);
            }
          
        }

        internal void TurnOffView()
        {
           CameraAnimatorController.HideEverything();
        }

        internal void ShowStream()
        {
                //RawImage1.material.mainTexture = CameraTexture;
            RawImage1.texture = CameraTexture;
            CameraTexture.Play();
        }

        private void Start()
        {
            CameraTexture.Play();
            ShowStream();
            baseRotation = transform.rotation;
        }
        void Update()
        {
            transform.rotation = baseRotation * Quaternion.AngleAxis(CameraTexture.videoRotationAngle, Vector3.up);
        }



        internal void SwitchToValidationView()
        {
            LoadPicture();
           CameraAnimatorController.ShowValidationView();
        }

        internal void SwitchToCameraTakingView()
        {
            ShowStream();
            CameraAnimatorController.PictureTakingModeOn();
        }

        internal void SwitchToLoadPictureView()
        {
            LoadPicture();
            CameraAnimatorController.PictureTakingModeOffWithCameraFrame();
        }

        bool HasMultipleCameras()
        {
            return Devices.Length > 1;
        }

        internal void HideEverything()
        {
            CameraAnimatorController.HideEverything();
        }

        void OnEnable()
        {
            if (!HasMultipleCameras())
            {
                CameraAnimatorController.CameraSwitchButton.SetActive(false);
            }
        }

    }
}
