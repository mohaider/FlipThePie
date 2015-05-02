using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Code.Camera
{
    /// <summary>
    /// Interface for the camera controller
    /// </summary>
    public interface ICameraController
    {
        WebCamTexture CameraTexture
        {
            get; set; 
        }

        WebCamDevice[] Devices
        {
            get;
        }

        string DeviceName { get; set; }

        void TakePicture();

        void SavePicture();
        

    }
}
