using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.ProceduralGeneration
{
    public class MeshBuilder: MonoBehaviour
    {
        private List<Vector3> m_Vertices = new List<Vector3>();
        private List<Vector3>  m_Normals = new List<Vector3>();
        private List<Vector2> m_UVs = new List<Vector2>(); 
        private List<int> m_Indices = new List<int>();

        public void AddTriangle(int index0, int index1, int index2)
        {
            m_Indices.Add(index0);
            m_Indices.Add(index1);
            m_Indices.Add(index2);
        }

        public Mesh CreateMesh()
        {
            Mesh mesh = new Mesh();

            mesh.vertices = m_Vertices.ToArray();
            mesh.triangles = m_Indices.ToArray();

            if (m_Normals.Count == m_Vertices.Count)
            {
                mesh.normals = m_Normals.ToArray();
            }

            if (m_UVs.Count == m_Vertices.Count)
            {
                mesh.uv = m_UVs.ToArray();
            }

            mesh.RecalculateBounds();
            return mesh;
        }
        public List<Vector3> Vertices
        {
            get
            {
                return m_Vertices;
            }
        }

        public List<Vector3> Normals
        {
            get { return m_Normals; } 
        }

        public List<Vector2> UVs
        {
            get { return m_UVs; } 
        }

        public List<int> Indices
        {
            get { return m_Indices; } 
        }
    }
}
