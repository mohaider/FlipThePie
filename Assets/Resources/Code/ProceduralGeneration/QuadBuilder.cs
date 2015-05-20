using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.ProceduralGeneration
{
    [RequireComponent(typeof(MeshBuilder))]
   public class QuadBuilder:MonoBehaviour
   {
       private MeshBuilder m_meshBuilder;
       public float m_Length, m_Width;

       void Start()
       {
           MeshBuilder.Vertices.Add(new Vector3(0,0,0));
           MeshBuilder.UVs.Add(new Vector2(0,0));
           MeshBuilder.Normals.Add(Vector3.back);

           MeshBuilder.Vertices.Add(new Vector3(0.0f, m_Length, 0.0f));
           MeshBuilder.UVs.Add(new Vector2(0.0f, 1.0f));
           MeshBuilder.Normals.Add(Vector3.back);

           MeshBuilder.Vertices.Add(new Vector3(m_Width, m_Length, 0.0f));
           MeshBuilder.UVs.Add(new Vector2(1.0f, 1.0f));
           MeshBuilder.Normals.Add(Vector3.back);

           MeshBuilder.Vertices.Add(new Vector3(m_Width, 0.0f, 0.0f));
           MeshBuilder.UVs.Add(new Vector2(1.0f, 0.0f));
           MeshBuilder.Normals.Add(Vector3.back);

           MeshBuilder.AddTriangle(0, 1, 2);
           MeshBuilder.AddTriangle(0, 2, 3);

           //Create the mesh:
           MeshFilter filter = GetComponent<MeshFilter>();

           if (filter != null)
           {
               filter.sharedMesh = MeshBuilder.CreateMesh();
           }
       }

       public MeshBuilder MeshBuilder
       {
           get
           {
               if (m_meshBuilder == null)
               {
                   m_meshBuilder = GetComponent<MeshBuilder>();
               }
               return m_meshBuilder;
           } 
       }
   }
}
