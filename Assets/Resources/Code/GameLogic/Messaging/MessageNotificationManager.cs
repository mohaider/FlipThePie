using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Code.GameLogic.Level;
using Assets.Resources.Code.UIElements;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.Messaging
{
    [RequireComponent(typeof(MessageView))]
    public class MessageNotificationManager:MonoBehaviour
    {
        private static MessageNotificationManager _instance;
      
        private Queue<Message> _messageQueue;
        private bool _isProcessingMessage;
        private Message _currentMessage;
        
        public static MessageNotificationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = GameObject.FindGameObjectWithTag("MessageWindow");
                    _instance = obj.GetComponent<MessageNotificationManager>();
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

    public struct Message
    {
        public Sender MsgSender;
        public string MsgContent;

        public Message(Sender sender, string contents)
        {
            MsgSender = sender;
            MsgContent = contents;
        }
    }

    public struct Sender
    {
        public string Name;
        public  Action<bool> CallbackAction;
      
        public Sender(string name, Action<bool> callbackAction)
        {
            Name = name;
            CallbackAction = callbackAction;
        }
    }
}
