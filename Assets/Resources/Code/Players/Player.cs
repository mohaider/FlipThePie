using UnityEngine;
using Assets.Resources.Code.Camera;
using Assets.Resources.Code.GameLogic;
using Assets.Resources.Code.Players;
using UnityEngine.UI;


namespace Assets.Resources.Code.Players
{
[RequireComponent(typeof(PlayerView))]
    public class Player : MonoBehaviour
    {
        // private const string Prependpath = "Assets/Resources/SaveData/SnapShots/";
     
        private string _playerName;


        private PlayerHeadType _selectedIcon;
        private PlayerView _playerView;
        public string ChosenIcon { get; set; }

    public bool HasPicture
    {
        
        get; set;//Todo : try to load the picture and return the result of the load
    }

        public string PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; }
        }

        public PlayerHeadType SelectedIcon
        {
            get { return _selectedIcon; }
            set
            {
              
                switch (value)
                {
                    case(PlayerHeadType.Giraffe):
                    {
                        View.ChangeIcon(value);
                        break;
                    }
                    case (PlayerHeadType.Monkey):
                    {
                        View.ChangeIcon(value);
                        break;
                    }
                       
                }
                _selectedIcon = value;

            }
        }

        public PlayerView View
        {
            get
            {
                if (_playerView == null)
                {
                    _playerView = GetComponent<PlayerView>();
                }
                return _playerView;
            }
            
        }

        void Start()
        {
           

        }
    }
}
