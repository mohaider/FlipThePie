using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Code.Players;
using Assets.Resources.Code.UIElements.Messages;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Code.GameLogic
{
    public class GameDirector: MonoBehaviour
    {
        private List<PlayerModel> _playersList;
        private MessageWindowDelegate _msgWindowDelegate;
        private GameObject _messageWindowGameObject;
        private Text _messageWindowText;
        private Animator _messageWindowAnimator;

        public MessageWindowDelegate MsgWindowDelegate
        {
            get
            {
                if (_msgWindowDelegate == null)
                {
                    _msgWindowDelegate = new MessageWindowDelegate();
                }
                return _msgWindowDelegate;
            }
            set { _msgWindowDelegate = value; }
        }

        public GameObject MessageWindowGameObject
        {
            get
            {
                if (_messageWindowGameObject == null)
                {
                    _messageWindowGameObject = GameObject.FindGameObjectWithTag("MessageWindow");
                }
                return _messageWindowGameObject;
            }
        }

        public Text MessageWindowText
        {
            get
            {
                if (_messageWindowText == null)
                {
                    GameObject obj = GameObject.FindGameObjectWithTag("MessageWindowText");
                    _messageWindowText = obj.GetComponent<Text>();
                }
                return _messageWindowText;
            }
           
        }

        public Animator MessageWindowAnimator
        {
            get
            {
                if (_messageWindowAnimator == null)
                {
                    _messageWindowAnimator = MessageWindowGameObject.GetComponent<Animator>();
                }
                return _messageWindowAnimator;
            }
        }

        public List<PlayerModel> PlayersList
        {
            get
            {
                if (_playersList == null)
                {
                    _playersList = new List<PlayerModel>();
                }
                return _playersList;
            }
            set { _playersList = value; }
        }

        private void Start()
        {
            MessageWindowAnimator.enabled = false;

        }

        private void Awake()
        {
            _msgWindowDelegate = new MessageWindowDelegate();
            MsgWindowDelegate.MessageEvent += (s, e) => ShowTextBox(e.Message);
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        public void NotifyWindow(string msg,string originator)
        {
            MsgWindowDelegate.SetMessage(new Message{Msg = msg,MsgOrigin = originator});
        }
        private void ShowTextBox(string str)
        {
            EnableMessageBox(true);
            MessageWindowText.text = str;
        }

        private void EnableMessageBox(bool status)
        {
            if (status)
            {
                MessageWindowAnimator.enabled = status;
            }
            MessageWindowAnimator.SetBool("isHidden",!status);
        }

        public void HideMessageBox()
        {
            EnableMessageBox(false );
        }
    }
}
