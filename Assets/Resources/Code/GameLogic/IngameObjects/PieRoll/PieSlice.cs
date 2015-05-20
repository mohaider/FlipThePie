using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.IngameObjects.PieRoll
{
    public class PieSlice: MonoBehaviour
    {
        public int index;
        private Renderer _objRenderer;
        private MeshCollider _mCollider;

        /// <summary>
        /// disables the renderer for this pie slice
        /// </summary>
        /// <param name="status"></param>
        public void DisablePieSlice()
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;
        }
        
    }
}
