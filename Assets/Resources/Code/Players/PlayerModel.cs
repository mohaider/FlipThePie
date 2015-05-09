using System;
using Assets.Resources.Code.Camera;
using UnityEngine;

namespace Assets.Resources.Code.Players
{
    public class PlayerModel : MonoBehaviour
    {
        private PlayerView _view;
        private bool _hasPicture = false;
        private AndroidCameraController _cameraController;
        private string _snapShotPath;
        private string _playerName;

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
        public bool HasPicture
        {
            get
            {
                _hasPicture = System.IO.File.Exists(SnapShotPath);
                return _hasPicture;
            }
            set { _hasPicture = value; }
        }
        /// <summary>
        /// This function will take a snapshot of the players picture by calling the camera controllers save picture function.
        /// It will update the view to show a dialog box if the player is satisfied with the current picture. 
        /// It will hide the flip camera button  and the take picture button 
        /// </summary>
        public void UpdatePictureModel()
        {
       
            View.ShowSettingsButton(false);
            CameraController.SavePicture(SnapShotPath);
            CameraController.HideCameraStream();
            TryToLoadPlayerPicture();

        }
        public string SnapShotPath
        {
            get
            {
                if (_snapShotPath == null)
                {
                    _snapShotPath = Application.persistentDataPath + "/" + PlayerName + ".png";
                }
                return _snapShotPath;
            }
        }
        public void TryToLoadPlayerPicture()
        {
            CameraController.LoadPicture(SnapShotPath, PictureFound);
        }
        public PlayerView View
        {
            get
            {
                if (_view == null)
                {
                    _view = GetComponent<PlayerView>();
                }
                return _view;
            }
          
        }

        public string PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; }
        }

        public void PictureFound(bool isSuccessfull)
        {
            if (isSuccessfull)
            {
                //todo: implement continue method to game
            }
            else
            {
                EnablePictureTaking();
            }
        }

        internal void EnablePictureTaking()
        {
        
            View.ShowSettingsButton(false);
            CameraController.DisplayCameraStream();
        }

        internal void LoadPlayerPicture()
        {
            CameraController.LoadPicture(SnapShotPath, PictureFound);
        }

        internal void DisablePictureTaking()
        {
          
            View.ShowSettingsButton(true);
        }
    }
}
