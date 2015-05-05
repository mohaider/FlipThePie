using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.Camera
{
    public class AndroidCameraController: MonoBehaviour, ICameraController
    {
        private WebCamTexture _cameraTexture;
        private WebCamDevice[] _devices;
        
        private string _currentDeviceName;
        private int _currentDeviceIndex=0;
        private Renderer _renderer;
        private GUITexture _guiTexture;
        private Texture2D _snapShot;
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
        public Renderer RendererMaterial
        {
            get
            {
                if (_renderer == null)
                {
                    _renderer = GetComponent<Renderer>();
                }
                return _renderer;
            }
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
        public Texture2D SnapShot
        {
            get
            {
                if (_snapShot == null)
                {
                    _snapShot = new Texture2D(CameraTexture.width,CameraTexture.height);
                }
                return _snapShot;
            }
            set { _snapShot = value; }
        }

        public GUITexture GuiTexture
        {
            get
            {
                if (_guiTexture == null)
                {
                    _guiTexture = GetComponent<GUITexture>();
                }
                return _guiTexture;
            }
            set { _guiTexture = value; }
        }

        private void SetUpGUITexture()
        {
            float w = Screen.width;
            float h = Screen.height;
            float insetX = -(w/2);
           // float insetX =0;
//            float insetY = h;
            float insetY = -(h/2);
     //       GuiTexture.transform.localScale = new Vector3(-1,-1,1);
            GuiTexture.pixelInset = new Rect(insetX,insetY ,w,h);
            SetRendererTexture();

        }

        public void SavePicture()
        {
            SnapShot = new Texture2D(CameraTexture.width, CameraTexture.height);
            SnapShot.SetPixels(CameraTexture.GetPixels());
            SnapShot.Apply();
            System.IO.File.WriteAllBytes("Assets/Resources/SaveData/Snapshots/test1.png", SnapShot.EncodeToPNG());
        }

        public void SavePicture(string path)
        {
            SnapShot = new Texture2D(CameraTexture.width, CameraTexture.height);
            SnapShot.SetPixels(CameraTexture.GetPixels());
            SnapShot.Apply();
            System.IO.File.WriteAllBytes(path, SnapShot.EncodeToPNG());
        }
   

        /// <summary>
        /// try to load the image into the gameobjects renderer. if it fails, then assign the delegated action bool to false
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callbackAction"></param>

        public void LoadPicture(string path, Action<bool> callbackAction)
        {
            try
            {
                if (SnapShot.LoadImage(File.ReadAllBytes(path)))
                {
                    RendererMaterial.material.mainTexture = SnapShot;
                    callbackAction(true);
                }
                
            }
            catch (FileNotFoundException e)
            {
                callbackAction(false);
            }
          
         
        }
        /// <summary>
        /// switches between different cameras
        /// </summary>
        public void SwitchCamera()
        {
            CameraTexture.Stop();
            if (Devices.Length > 0)
            {
                _currentDeviceIndex++;
                if (_currentDeviceIndex >= Devices.Length)
                {
                    _currentDeviceIndex = 0;
                }
                CameraTexture.deviceName = DeviceName;
                //SetGUITexture();
                SetRendererTexture();
                DisplayCameraStream();
            }
            //DeviceName = Devices[_currentDeviceIndex].name;
          
        }





        public void DisplayCameraStream()
        {

            SetRendererTexture();
            CameraTexture.Play();
         
        }

        private void SetRendererTexture()
        {
            RendererMaterial.material.mainTexture = CameraTexture;
        }

        private void SetGUITexture()
        {
            GuiTexture.texture = CameraTexture;
        }
        public void HideCameraStream()
        {
            CameraTexture.Stop();
        }

       

        private void Start()
        {
            //SetUpGUITexture();
            //SetGUITexture();
            UnityEngine.Camera cam = UnityEngine.Camera.main;
          //  transform.localScale = new Vector3(cam.orthographicSize / 2 * ((float)Screen.width / (float)Screen.height),  1f,cam.orthographicSize / 2);
            SetRendererTexture();
            DisplayCameraStream();
            baseRotation = transform.rotation;
        }
        private Quaternion baseRotation;
        void Update()
        {
            transform.rotation = baseRotation * Quaternion.AngleAxis(CameraTexture.videoRotationAngle, Vector3.up);
        }
    }
}
