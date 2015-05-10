using UnityEngine;

namespace Assets.Resources.Code.GameLogic.Level.GameStart
{

   public class StartScreenLevelController: BaseLevelController
    {
       void Awake()
       {
           GameDirector.Instance.AddLevel(this);
       }


    }
}
