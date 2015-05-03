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
        private string _deviceName;
        private Renderer _renderer;
        private Texture2D _snapShot;
        public WebCamTexture CameraTexture
        {
            get
            {
                if (_cameraTexture == null)
                {
                    _cameraTexture = new WebCamTexture(FrontFacingDeviceName,400,400,15);
                    RendererMaterial.material.mainTexture = _cameraTexture;
                }
                return _cameraTexture;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public WebCamDevice[] Devices
        {
            get
            {
                if (_devices == null)
                {
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
        /// <summary>
        /// we'll only grab the front facing device's name
        /// </summary>
        public string FrontFacingDeviceName
        {
            get
            {
                if (_deviceName == null)
                {
                    for (int i = 0; i < Devices.Length; i++)
                    {
                        if (Devices[i].isFrontFacing)
                        {
                            _deviceName = Devices[i].name;
                            break;
                        }
                    }
                    
                }
                return _deviceName;
            }
            set
            {
                throw new NotImplementedException();
            }
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
        }
   

        public void SavePicture()
        {
            
            SnapShot.SetPixels(CameraTexture.GetPixels());
            SnapShot.Apply();
            System.IO.File.WriteAllBytes("Assets/Resources/SaveData/Snapshots/test1.png", SnapShot.EncodeToPNG());
        }

        public void SavePicture(string path)
        {
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





        public void DisplayCameraStream()
        {
            CameraTexture.Play();
        }

        public void HideCameraStream()
        {
            CameraTexture.Stop();
        }
    }
}
