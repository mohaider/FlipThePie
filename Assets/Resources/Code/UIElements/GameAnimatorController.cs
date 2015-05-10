using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.UIElements
{
   public class GameAnimatorController:MonoBehaviour
   {

       private Animator _thisAnimator;
       public string Name;


       public Animator ThisAnimator
       {
           get
           {
               if (_thisAnimator == null)
               {
                   _thisAnimator = GetComponent<Animator>();
               }
               return _thisAnimator;
           }
           set { _thisAnimator = value; }
       }
       internal void Show(bool status)
       {
           if (status)
           {
               ThisAnimator.enabled = true;
           }
           ThisAnimator.SetBool("isHidden", !status);
       }

       void Awake()
       {
           Name = name;
       }
     
   }
}
