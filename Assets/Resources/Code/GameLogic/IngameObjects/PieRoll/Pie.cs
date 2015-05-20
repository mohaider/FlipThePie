using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Code.ProceduralGeneration;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.IngameObjects.PieRoll
{
    public class Pie : MonoBehaviour
    {
        private SubDivisionBuilder m_PieBuilder;
        private List<PieSlice> m_PieSlices;
        private List<int> m_DisabledPieSliceIndices =new List<int>(); 
        public Sprite SelectorSprite;
        private GameObject m_SelectorGameObject;

        public float HalfHeight
        {
            get { return PieBuilder.Height / 2f; }
        }

        void Start()
        {
            PieBuilder.BuildSubdvisions();
             m_PieSlices = PieBuilder.PieSlices;
            BuildSelectorGameObject();
        }

        private void BuildSelectorGameObject()
        {
            m_SelectorGameObject = new GameObject();
            Vector3 yPosition = m_SelectorGameObject.transform.position = Vector3.zero;
            yPosition.y = HalfHeight;
            m_SelectorGameObject.transform.position = yPosition;
            SpriteRenderer s = m_SelectorGameObject.AddComponent<SpriteRenderer>();
            s.sprite = SelectorSprite;
            m_SelectorGameObject.name = "Selector";
            
            BoxCollider box = m_SelectorGameObject.AddComponent<BoxCollider>();
            box.center -= yPosition/2;
            box.size = new Vector3(0.01f,0.01f,0.5f);
        }
        internal void EnableAllPieSliceColliders()
        {
            foreach (PieSlice p in PieSlices)
            {
                p.GetComponent<MeshCollider>().enabled = true;
            }
        }


        internal void DisableCollidersWithoutRenders()
        {
            foreach (int i in m_DisabledPieSliceIndices)
            {
                PieSlices[i].DisablePieSlice();
            }
        }

        public void DisablePieSlice(int index)
        {
            PieSlices[index].DisablePieSlice();
            if (!m_DisabledPieSliceIndices.Exists(i => i == index)) //to avoid adding a slice index multiple times
            {
                m_DisabledPieSliceIndices.Add(index);
            }

        }
        public SubDivisionBuilder PieBuilder
        {
            get
            {
                if (m_PieBuilder == null)
                {
                    m_PieBuilder = GetComponent<SubDivisionBuilder>();
                }
                return m_PieBuilder;
            }
            set { m_PieBuilder = value; }
        }

        public List<PieSlice> PieSlices
        {
            get
            {
                if (m_PieSlices == null)
                {
                    m_PieSlices = PieBuilder.PieSlices;

                }
                return m_PieSlices;
            }
            set { m_PieSlices = value; }
        }

        public GameObject MSelectorGameObject
        {
            get { return m_SelectorGameObject; }
            set { m_SelectorGameObject = value; }
        }

        public List<int> MDisabledPieSliceIndices
        {
            get { return m_DisabledPieSliceIndices; }
            set { m_DisabledPieSliceIndices = value; }
        }
    }
}
