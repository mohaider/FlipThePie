using UnityEngine;
using Assets.Resources.Code.Camera;
using Assets.Resources.Code.GameLogic;
using UnityEngine.UI;


namespace Assets.Resources.Code.Players
{
    
    public class Player : MonoBehaviour
    {
        // private const string Prependpath = "Assets/Resources/SaveData/SnapShots/";
     
        private string _playerName;


        private PlayerHeadType _selectedIcon;
        private PlayerView _playerView;
        public string ChosenIcon { get; set; }

        public bool HasPicture { get; set; }

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
            PlayerName = name;
            GameDirector.Instance.CurrentPlayer = this;
        }
    }
}
