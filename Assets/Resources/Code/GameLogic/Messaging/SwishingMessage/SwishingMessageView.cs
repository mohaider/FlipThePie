using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Code.UIElements;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.Messaging.SwishingMessage
{
   public  class SwishingMessageView: MonoBehaviour
    {
       private static SwishingMessageView _instance;

        private GameAnimatorController _messageBox;
        private UnityEngine.UI.Text _messageBoxText;

        public GameAnimatorController MessageBox
        {
            get
            {
                if (_messageBox == null)
                {
                    GameObject messageGO = GameObject.FindGameObjectWithTag("SwishingText");
                    _messageBox = messageGO.GetComponent<GameAnimatorController>();
                }
                return _messageBox;
            }
            set { _messageBox = value; }
        }
        public static SwishingMessageView Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject messageBoxGO = GameObject.FindGameObjectWithTag("SwishingText");
                    _instance = messageBoxGO.GetComponent<SwishingMessageView>();

                }
                return _instance;
            }
            set { _instance = value; }
        }

        public UnityEngine.UI.Text MessageBoxText
        {
            get
            {
                if (_messageBoxText == null)
                {
                    GameObject messageBoxText = GameObject.FindGameObjectWithTag("SwishingMessageView");
                    _messageBoxText = messageBoxText.GetComponent<UnityEngine.UI.Text>();
                }
                return _messageBoxText;
            }
            set { _messageBoxText = value; }
        }

        public void PopUpBox(Message msg)
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
