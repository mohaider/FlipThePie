using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.IngameObjects.PieRoll
{
    public class PieRoller: MonoBehaviour
    {
        private List<PieSlice> m_PieSlices;
        private PieRollState m_CurrentState;
        private int m_NumberOfEnabledSlices;
        private bool m_hasTouched;
        public LayerMask SliceLayerMask;
        private Pie m_pie;
        void OnStateChange(PieRollState newState)
        {
            switch (newState)
            {
                
                case PieRollState.Completed:
                    break;
                case PieRollState.EndRoll:
                    break;
                case PieRollState.Rolling:
                    break;
                case PieRollState.OutOfView:
                    break;
                case PieRollState.Selection:
                    break;

            }
        }

        internal void HidePieSlice(int p)
        {
            MPie.DisablePieSlice(p);
        }

        public List<PieSlice> PieSlices
        {
            get
            {
                if (m_PieSlices == null)
                {
                    m_PieSlices = GetComponent<Pie>().PieSlices;
                }
                return m_PieSlices;
            }
            set { m_PieSlices = value; }
        }

        public Pie MPie
        {
            get
            {
                if (m_pie == null)
                {
                    m_pie = GetComponent<Pie>();
                }
                return m_pie;
            }
            set { m_pie = value; }
        }

   
    }
}
