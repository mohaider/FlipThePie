

using Assets.Resources.Code.Camera;
using UnityEngine;

namespace Assets.Resources.Code.Players
{
   public class PlayerController : MonoBehaviour
   {
       private PlayerModel _model;
     
       public PlayerModel Model
       {
           get
           {
               if (_model == null)
               {
                   _model = GetComponent<PlayerModel>();
               }
               return _model;
           }
       }

       public void CaptureSnapshot()
       {
           Model.UpdatePictureModel();
       }

       public void LoadPicture()
       {
           Model.LoadPlayerPicture();
       }
       /// <summary>
       /// if the player wishes to retake their picture, then the model's state must be enable to handle that
       /// </summary>
       public void RetakePicture()
       {
           Model.EnablePictureTaking();
       }

       public void DisablePictureTakeing()
       {
           Model.DisablePictureTaking();
       }

    

       private void DebugStuff()
       {
           Model.EnablePictureTaking();
       }

       private void Start()
       {
           DebugStuff();
       }
   }
}
