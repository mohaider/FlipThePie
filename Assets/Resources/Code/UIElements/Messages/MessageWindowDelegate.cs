﻿using System;

namespace Assets.Resources.Code.UIElements.Messages
{
   public class MessageWindowDelegate 
   {
       public event EventHandler<NewMessageEventArgs> MessageEvent;

       protected virtual void OnNewMessage(string message,string messageOriginator)
       {
           if (MessageEvent != null)
           {
               MessageEvent(this, new NewMessageEventArgs(message,messageOriginator));
           }
       }

       public void SetMessage(Messagerrrrr messagerrrrr)
       {
           OnNewMessage(messagerrrrr.Msg,messagerrrrr.MsgOrigin);
       }

 
   }
}
