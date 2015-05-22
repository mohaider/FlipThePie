using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.IngameObjects.MusicButton
{
   public class MusicButtonControllerView: MonoBehaviour
   {

       public void ShrinkView()
       {
           
       }

       public void ShowMusicButtons(bool status)
       {
           
       }

       public void HideView()
       {
            ThisAnimator.SetBool("Show",false);
       }
       public void InGameView()
       {
           ThisAnimator.SetBool("Show", true);
           ThisAnimator.SetBool("Grow", true);
       }
       public void IngameButtonView()
       {
           ThisAnimator.SetBool("Show",true);
           ThisAnimator.SetBool("Grow",false);
       }

     
       public Animator ThisAnimator
       {
           get
           {
               return GetComponent<Animator>();
           }
           
       }
   }
}
