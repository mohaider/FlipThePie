using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Code.GameLogic.Level.GameStart;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Code.GameLogic.IngameObjects.MusicButton
{
  public  class MusicButton: MonoBehaviour
  {
      private ColorBlock m_originalColorBlock;
      
      private Button m_thisButton;
      private Color m_playSoundColor;
      private Color m_noPlaySoundColor;
      private AudioSource m_ThisAudioSource;
      public AudioClip m_thisAudioClip;
      public float m_Speed;
      public int Index;


      void Start()
      {
          m_originalColorBlock = ThisButton.colors;
      }

      /// <summary>
      /// depending on the bool that was passed in, disable or enable the current button interactivity
      /// </summary>
      /// <param name="status"></param>
      public void ButtonInteractivity(bool status)
      {
          if (!status)
          {

              ColorBlock cb = ButtonsColorBlock;
              cb.disabledColor = m_originalColorBlock.normalColor;
              ButtonsColorBlock = cb;
          }
          if (status)
          {
              ButtonsColorBlock = m_originalColorBlock;
          }
          ThisButton.interactable = status;
         
        
      }

      public void PlaySoundWhenDisabled(Action callbackAction)
      {
          ColorBlock cb = ButtonsColorBlock;
          cb.disabledColor = m_originalColorBlock.pressedColor;
          ButtonsColorBlock = cb; 
          ThisAudioSource.Play();
          StartCoroutine(ResetBackToNormalAfterTime(m_thisAudioClip.length*m_Speed, callbackAction));
          /*  Invoke("ResetBackToNormal",m_thisAudioClip.length);
          */
      }

      private void ResetBackToNormal()
      {
          ButtonsColorBlock = m_originalColorBlock;
      }

      private IEnumerator WaitThenResetColorToNormal(float t)
      {
          yield return new WaitForSeconds(t);
          ButtonsColorBlock = m_originalColorBlock;
      }
      private IEnumerator ResetBackToNormalAfterTime(float t, Action callbackAction)
      {
          StartCoroutine(WaitThenResetColorToNormal(0.25f));
          yield return new WaitForSeconds(t);
          callbackAction();
          
      }

      public Button ThisButton
      {
          get
          {
              if (m_thisButton == null)
              {
                  m_thisButton = GetComponent<Button>();
              }
              return m_thisButton;
          }
          set { m_thisButton = value; }
      }

      private ColorBlock ButtonsColorBlock
      {
          get { return ThisButton.colors; }
          set { ThisButton.colors = value; }
      }

   
      public AudioSource ThisAudioSource
      {
          get
          {
              if (m_ThisAudioSource == null)
              {
                 m_ThisAudioSource= GetComponent<AudioSource>();
              }
              m_ThisAudioSource.clip = m_thisAudioClip;
              return m_ThisAudioSource;
          }
          set { m_ThisAudioSource = value; }
      }

      internal void SetIndex(int index)
      {
          Index = index;
      }

      internal void PlaySound()
      {
          ThisAudioSource.Play();
      }
  }
}
