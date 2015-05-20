using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using Assets.Resources.Code.GameLogic.IngameObjects.PieRoll;
using UnityEngine;

namespace Assets.Resources.Code.ProceduralGeneration
{
    public class SubDivisionBuilder: MonoBehaviour
    {
        public int SubDivisions; //for now only does base 2 subdivision
        public float Width;
        public float Height;
        public Vector3 Origin;
        public Texture2D Texture2D;
        public Material TriangleMaterial;
        public LayerMask SliceLayerMask;
        private List<PieSlice> m_PieSlices;

        public List<PieSlice> PieSlices
        {
            get
            {
                if (m_PieSlices == null)
                {
                    m_PieSlices = new List<PieSlice>();
                }
                return m_PieSlices;
            }
            set { m_PieSlices = value; }
        }


   

        private void AddCircleCollider()
        {
            throw new NotImplementedException();
        }
        public void BuildSubdvisions()
        {
            int pieIndex = 0;
            float xConstant = (Width)/(SubDivisions/4f);
            float yConstant = (Height)/(SubDivisions/4f);
            float xOrigin = Origin.x - (float)Width/2f;
            float yOrigin = Origin.y - (float) Height/2f;
            for (int i = 0; i < SubDivisions/4; i ++)
            {
                float uvMap = i / ((float)SubDivisions / 4);
                float uvMapNext = (i + 1f) / ((float)SubDivisions / 4);
                #region top portion
                //top part

                GameObject topTriangle = CreateNewTriangle(pieIndex++);
                MeshBuilder mBuilderTop = topTriangle.GetComponent<MeshBuilder>();
                

                //center vertex
                mBuilderTop.Vertices.Add(Origin);
                mBuilderTop.UVs.Add(new Vector2(0.5f, 0.5f));
                mBuilderTop.Normals.Add(Vector3.back);

                float widthFactor = i * xConstant ;
                //top left vertex
                mBuilderTop.Vertices.Add(new Vector3(widthFactor + xOrigin, Origin.y + Height / 2f, 0));
               // mBuilderTop.Vertices.Add(new Vector3(((i*) * ((float)Width / (float)SubDivisions)) + (Origin.x * offset), (Height / 2f) + Origin.y, 0f));
                mBuilderTop.UVs.Add(new Vector2(uvMap, 1));
                mBuilderTop.Normals.Add(Vector3.back);

                //top right vertex
              
                mBuilderTop.Vertices.Add(new Vector3(widthFactor + xConstant + xOrigin  , Origin.y + Height / 2f, 0));
                mBuilderTop.UVs.Add(new Vector2(uvMapNext, 1));
                mBuilderTop.Normals.Add(Vector3.back);

                mBuilderTop.AddTriangle(0, 1, 2);

                MeshFilter topFilterfilter = topTriangle.GetComponent<MeshFilter>();

                if (topFilterfilter != null)
                {
                    topFilterfilter.sharedMesh = mBuilderTop.CreateMesh();
                    AddMeshCollider(topTriangle, topFilterfilter.sharedMesh);
                    SetGameObjectLabel(topTriangle);
                }

               
                topTriangle.transform.parent = transform;
                #endregion 
                #region bottom portion
                //top part

                GameObject bottomTriangle = CreateNewTriangle(pieIndex++);
                MeshBuilder mBuilderBottom = bottomTriangle.GetComponent<MeshBuilder>();


                //center vertex
                mBuilderBottom.Vertices.Add(Origin);
                mBuilderBottom.UVs.Add(new Vector2(0.5f, 0.5f));
                mBuilderBottom.Normals.Add(Vector3.back);

                //top right vertex
          
                mBuilderBottom.Vertices.Add(new Vector3(widthFactor + xConstant + xOrigin, Origin.y - Height / 2f, 0));
                mBuilderBottom.UVs.Add(new Vector2(uvMapNext, 0));
                mBuilderBottom.Normals.Add(Vector3.back);
                //top left vertex
                mBuilderBottom.Vertices.Add(new Vector3(widthFactor + xOrigin, Origin.y - Height / 2f, 0));
                // mBuilderTop.Vertices.Add(new Vector3(((i*) * ((float)Width / (float)SubDivisions)) + (Origin.x * offset), (Height / 2f) + Origin.y, 0f));
                mBuilderBottom.UVs.Add(new Vector2(uvMap,0));
                mBuilderBottom.Normals.Add(Vector3.back);

                mBuilderBottom.AddTriangle(0, 1, 2);

                MeshFilter bottomFilter = bottomTriangle.GetComponent<MeshFilter>();

                if (bottomFilter != null)
                {
                    bottomFilter.sharedMesh = mBuilderBottom.CreateMesh();
                    AddMeshCollider(bottomTriangle, bottomFilter.sharedMesh);
                    SetGameObjectLabel(bottomTriangle);
                }

                
                bottomTriangle.transform.parent = transform;
                #endregion 
                #region left portion
                //top part

                GameObject leftTriangle = CreateNewTriangle(pieIndex++);
                MeshBuilder mBuilderLeft = leftTriangle.GetComponent<MeshBuilder>();
                float heightFactor = i * yConstant;

                //center vertex
                mBuilderLeft.Vertices.Add(Origin);
                mBuilderLeft.UVs.Add(new Vector2(0.5f, 0.5f));
                mBuilderLeft.Normals.Add(Vector3.back);

                //bottom left vertex

                mBuilderLeft.Vertices.Add(new Vector3(Origin.x - Width / 2f, heightFactor + yOrigin, 0));
                mBuilderLeft.UVs.Add(new Vector2(0,uvMap));
                mBuilderLeft.Normals.Add(Vector3.back);
                //top left vertex
                mBuilderLeft.Vertices.Add(new Vector3(Origin.x - Width / 2f, heightFactor + yConstant + yOrigin, 0));
                // mBuilderTop.Vertices.Add(new Vector3(((i*) * ((float)Width / (float)SubDivisions)) + (Origin.x * offset), (Height / 2f) + Origin.y, 0f));
                mBuilderLeft.UVs.Add(new Vector2(0,uvMapNext));
                mBuilderLeft.Normals.Add(Vector3.back);

                mBuilderLeft.AddTriangle(0, 1, 2);

                MeshFilter leftFilter = leftTriangle.GetComponent<MeshFilter>();

                if (bottomFilter != null)
                {
                    leftFilter.sharedMesh = mBuilderLeft.CreateMesh();
                    AddMeshCollider(leftTriangle, leftFilter.sharedMesh);
                    SetGameObjectLabel(leftTriangle);
                }


                leftTriangle.transform.parent = transform;
                #endregion 
                #region right portion
                //top part

                GameObject rightTriangle = CreateNewTriangle(pieIndex++);
                MeshBuilder mBuilderRight = rightTriangle.GetComponent<MeshBuilder>();


                //center vertex
                mBuilderRight.Vertices.Add(Origin);
                mBuilderRight.UVs.Add(new Vector2(0.5f, 0.5f));
                mBuilderRight.Normals.Add(Vector3.back);

               
                //top right vertex
                mBuilderRight.Vertices.Add(new Vector3(Origin.x + Width / 2f, heightFactor + yConstant + yOrigin, 0));

                mBuilderRight.UVs.Add(new Vector2(1,uvMapNext));
                mBuilderRight.Normals.Add(Vector3.back);
                //bottom right vertex

                mBuilderRight.Vertices.Add(new Vector3(Origin.x + Width / 2f, heightFactor + yOrigin, 0));
                mBuilderRight.UVs.Add(new Vector2(1,uvMap));
                mBuilderRight.Normals.Add(Vector3.back);

                mBuilderRight.AddTriangle(0, 1, 2);


                MeshFilter rightFilter = rightTriangle.GetComponent<MeshFilter>();

                if (bottomFilter != null)
                {
                    rightFilter.sharedMesh = mBuilderRight.CreateMesh();
                    AddMeshCollider(rightTriangle, rightFilter.sharedMesh);
                    SetGameObjectLabel(rightTriangle);
                }


                rightTriangle.transform.parent = transform;
                #endregion 
            }


        }

        private void AddMeshCollider(GameObject triangleGameObject,Mesh mesh)
        {
            MeshCollider meshCollider = triangleGameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh;
        }

        private void SetGameObjectLabel(GameObject triangleGameObject)
        {
            triangleGameObject.layer = LayerMask.NameToLayer("Slice");
        }

 
        GameObject CreateNewTriangle(int index)
        {
            GameObject returner = new GameObject();
            returner.AddComponent<MeshBuilder>();
            returner.AddComponent<MeshFilter>();
            returner.AddComponent<MeshRenderer>();
            Material triangleMaterial = new Material(TriangleMaterial);
            triangleMaterial.mainTexture = Texture2D;
            returner.GetComponent<MeshRenderer>().material = triangleMaterial;
            PieSlice pieSlice =returner.AddComponent<PieSlice>();
            pieSlice.index = index;
            PieSlices.Add(pieSlice);
            return returner;
        }


    }
}
