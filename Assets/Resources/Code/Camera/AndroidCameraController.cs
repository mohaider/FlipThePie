using System;
using System.Collections.Generic;
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
        public WebCamTexture CameraTexture
        {
            get
            {
                if (_cameraTexture == null)
                {
                    _cameraTexture = new WebCamTexture(DeviceName,400,300,15);
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

        public string DeviceName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void TakePicture()
        {
            throw new NotImplementedException();
        }

        public void SavePicture()
        {
            throw new NotImplementedException();
        }


    
    }
}
