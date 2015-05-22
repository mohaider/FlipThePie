using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.Messaging.SwishingMessage
{
    public class SwishingMessageManager: MonoBehaviour
    {
        private static SwishingMessageManager _instance;

        private Queue<Message> _messageQueue;
        private bool _isProcessingMessage;
        private Message _currentMessage;

        public static SwishingMessageManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = GameObject.FindGameObjectWithTag("SwishingText");
                    _instance = obj.GetComponent<SwishingMessageManager>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
            set { _instance = value; }
        }



        public Queue<Message> MessageQueue
        {
            get
            {
                if (_messageQueue == null)
                {
                    _messageQueue = new Queue<Message>();
                }
                return _messageQueue;
            }
            set { _messageQueue = value; }
        }

        public bool IsProcessingMessage
        {
            get { return _isProcessingMessage; }
            set { _isProcessingMessage = value; }
        }

        public Message CurrentMessage
        {
            get { return _currentMessage; }
            set { _currentMessage = value; }
        }





        /// <summary>
        /// Notify the sender that the message has been process
        /// </summary>
        /// <param name="sndr"></param>
        public void Notify(Sender sndr)
        {

        }
        /// <summary>
        /// Queue up a message and then try to see if there are anyother messages that need to be queued up
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="callback"></param>
        public void RequestMessageBox(Message msg)
        {
            MessageQueue.Enqueue(msg);
            Instance.TryNextMessage();
        }

        public void RequestMessageBox(string name, string message)
        {
            
        }


        private void TryNextMessage()
        {
            if (!Instance.IsProcessingMessage && Instance.MessageQueue.Count > 0)
            {
                IsProcessingMessage = true;
                CurrentMessage = Instance.MessageQueue.Dequeue();
                DisplayMessage(Instance.CurrentMessage);
            }
        }

        private void DisplayMessage(Message msg)
        {
            MessageView.Instance.PopUpBox(msg);
        }

        public void ConsumeMessage()
        {
            Instance.IsProcessingMessage = false;
            Instance.CurrentMessage.MsgSender.CallbackAction(true);
            MessageView.Instance.ShowBox(false);
            TryNextMessage();
        }

    }
}
