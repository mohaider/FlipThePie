using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.IngameObjects.PieRoll
{
    public class PieRollView: MonoBehaviour
    {
        private List<PieSlice> m_PieSlices;
        private bool m_IsSpinning;
        private float m_rotVelocity;
        public void HidePieSlice(int i)
        {
            PieSlices[i].DisablePieSlice();
        }

        void Update()
        {
            if (m_IsSpinning)
            {
                SlowDownRotation();
            }
        }
        public void SpinPie(float speed)
        {
            m_rotVelocity = speed;
            m_IsSpinning = true;
        }

        private void SlowDownRotation()
        {
            
        }
        public List<PieSlice> PieSlices
        {
            get { return m_PieSlices; }
            set { m_PieSlices = value; }
        }


    }
}
