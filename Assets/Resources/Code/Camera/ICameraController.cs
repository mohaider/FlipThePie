using System;
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

        Texture2D SnapShot { get; }

        Renderer RendererMaterial { get; }

        string DeviceName { get; set; }



        void SavePicture();

        void SavePicture(string path);

        void LoadPicture(string path,Action<bool> callbackAction);

        void DisplayCameraStream();

        void HideCameraStream();


    }
}
