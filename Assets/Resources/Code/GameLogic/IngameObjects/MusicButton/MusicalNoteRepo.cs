using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Code.GameLogic.Level.GameStart;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.IngameObjects.MusicButton
{
   public  class MusicalNoteRepo: MonoBehaviour
   {
       public AudioClip Note1, Note2, Note3, Note4, Note5, Note6, Note7;
       public static AudioClip A, B, C, D, E, F, G;

       void Start()
       {
           A = Note1;
           B = Note2;
           C = Note3;
           D = Note4;
           E = Note5;
           F = Note6;
           G = Note7;
       }
   }
}
