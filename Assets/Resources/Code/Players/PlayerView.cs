
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Code.Players
{
    public enum PlayerHeadType
    {
        Giraffe,
        Monkey
    }
    public class PlayerView : MonoBehaviour
    {

        public Sprite MonkeySprite;
        public Sprite GiraffeSprite;
        private UnityEngine.UI.Image _playerImage;
        private static PlayerView _instance;



        internal void ChangeIcon(PlayerHeadType value)
        {
            switch (value)
            {
                case PlayerHeadType.Giraffe:
                    PlayerImage.sprite = GiraffeSprite;
                    break;

                    case PlayerHeadType.Monkey:
                    PlayerImage.sprite = MonkeySprite;
                    break;
            }
        }
      
     
        public static PlayerView Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject viewGameObject = GameObject.FindGameObjectWithTag("PlayerHead");
                    _instance = viewGameObject.GetComponent<PlayerView>();
                }
                return _instance;
            }
            set { _instance = value; }
        }

        public Image PlayerImage
        {
            get
            {
                if (_playerImage == null)
                {
                    _playerImage = GetComponent<Image>();
                }
                return _playerImage;
            } 
        }


        internal void ShowSettingsButton(bool status)
        {
           Debug.Log("Fill up this code");
        }

       
        internal void EnableAnimator(Animator anim, bool status)
        {
            anim.enabled = status;
        }


        private void Awake()
        {
        }

      
    }
}
