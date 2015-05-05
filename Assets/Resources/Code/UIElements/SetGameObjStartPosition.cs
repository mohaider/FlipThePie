using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.UIElements
{
    /// <summary>
    /// sets the starting position of a gameobject relative to defined RectTransform
    /// </summary>
   public  class SetGameObjStartPosition: MonoBehaviour
    {
        public Vector3 PositionOffset;
        public RectTransform OtherTransform;

        void Start()
        {
            Vector3 offsetAmount = UnityEngine.Camera.main.ScreenToWorldPoint(OtherTransform.position) + PositionOffset;
            offsetAmount.z = 0;
            transform.position =  offsetAmount;
        }

        void Update()
        {
            Vector3 offsetAmount = UnityEngine.Camera.main.ScreenToWorldPoint(OtherTransform.position) + PositionOffset;
            offsetAmount.z = 0;
            transform.position =  offsetAmount;
        }
    }
}
