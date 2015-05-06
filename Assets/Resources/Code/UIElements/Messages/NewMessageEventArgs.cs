using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Resources.Code.UIElements.Messages
{
    public class NewMessageEventArgs : EventArgs
    {

        public NewMessageEventArgs(string message,string messageOriginator)
        {
            Message = message;
            MessageOriginator = messageOriginator;
        }

        public string Message
        {
            get;
            set;
        }
        public string MessageOriginator { get; set; }
    }
}
