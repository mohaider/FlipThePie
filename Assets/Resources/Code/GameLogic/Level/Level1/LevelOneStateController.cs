using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Resources.Code.GameLogic.Level.Level1
{
    public class LevelOneStateController:BaseLevelController
    {
        void Awake()
        {
            GameDirector.Instance.AddLevel(this);
        }
    }
}
