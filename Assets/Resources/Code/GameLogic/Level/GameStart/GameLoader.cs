using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.Level.GameStart
{
    public class GameLoader: MonoBehaviour
    {
        private static GameLoader _instance;


        public static GameLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = GameObject.FindGameObjectWithTag("GameLoader");
                    _instance = obj.GetComponent<GameLoader>();
                }
                return _instance;
            }
            set { _instance = value; }
        }
    }
}
