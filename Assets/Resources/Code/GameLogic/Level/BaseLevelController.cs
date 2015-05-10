using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.Level
{
 
    public class BaseLevelController: MonoBehaviour
    {

        public int LevelNumber;
        public string LevelName;
        public delegate void LevelStateHandler(int newState);

        public event LevelStateHandler OnStateChange;

        public void ChangeState(int newState)
        {
            if (OnStateChange != null)
            {
                OnStateChange(newState);
            }
        }
    }
}
