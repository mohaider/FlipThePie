using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Code.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Code.GameLogic.Messaging
{
   public  class MessageView: MonoBehaviour
   {
       private static MessageView _instance;

       private GameAnimatorController _messageBox;
       private UnityEngine.UI.Text _messageBoxText;

       public GameAnimatorController MessageBox
       {
           get
           {
               if (_messageBox == null)
               {
                   GameObject messageGO = GameObject.FindGameObjectWithTag("MessageWindow");
                   _messageBox = messageGO.GetComponent<GameAnimatorController>();
               }
               return _messageBox;
           }
           set { _messageBox = value; }
       }
       public static MessageView Instance
       {
           get
           {
               if (_instance == null)
               {
                   GameObject messageBoxGO = GameObject.FindGameObjectWithTag("MessageWindow");
                   _instance = messageBoxGO.GetComponent<MessageView>();
                   
               }
               return _instance;
           }
           set { _instance = value; }
       }

       public Text MessageBoxText
       {
           get
           {
               if (_messageBoxText == null)
               {
                   GameObject messageBoxText = GameObject.FindGameObjectWithTag("MessageWindowText");
                   _messageBoxText = messageBoxText.GetComponent<Text>();
               }
               return _messageBoxText;
           }
           set { _messageBoxText = value; }
       }

       public  void PopUpBox(Message msg)
       {
           MessageBoxText.text = msg.MsgContent;
           ShowBox(true);
       }

       public void ShowBox(bool status)
       {
           MessageBox.Show(status);
       }

   }
}
