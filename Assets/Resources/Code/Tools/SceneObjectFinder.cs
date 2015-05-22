using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.Tools
{
    public class SceneObjectFinder<T> where T : MonoBehaviour 
    {
        public static GameObject FindObjecInSceneWithObjectAttached(string tag)
        {
            GameObject foundObject = GameObject.FindGameObjectWithTag(tag);
            if (foundObject == null)
            {
               
                foundObject = new GameObject();
                foundObject.tag = tag;
                foundObject.AddComponent<T>();
            }
            if (foundObject.GetComponent<T>() == null)
            {
                foundObject.AddComponent<T>();
            }
            return foundObject;
        }

        public static T FindGameObjectReturnT(string tag)
        {
            GameObject obj = FindObjecInSceneWithObjectAttached(tag);
            return obj.GetComponent<T>();
        }
    }
}
