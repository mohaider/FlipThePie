
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Resources.Code.Players;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic
{
    public class PlayerSettingsCreationManager: MonoBehaviour
    {
        private static PlayerSettingsCreationManager _instance ;
        private Queue<PlayerSettingsRequest> _playerCreationQueue;
        private bool _isProcessing=false;
        private PlayerSettingsRequest _currentSettingsRequest;
        private GameDirector _director;
        public static PlayerSettingsCreationManager Instance
        {
            get
            {
                if (_instance == null)
                {

                    GameObject obj = GameObject.FindGameObjectWithTag("Director");
                    _instance = obj.GetComponent<PlayerSettingsCreationManager>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
      
        }
        public Queue<PlayerSettingsRequest> PlayerCreationQueue
        {
            get
            {
                if (_playerCreationQueue == null)
                {
                    _playerCreationQueue = new Queue<PlayerSettingsRequest>();
                }
                return _playerCreationQueue;
            }
            set { _playerCreationQueue = value; }
        }

        public bool IsProcessing
        {
            get
            {
                return _isProcessing;
            }
            set { _isProcessing = value; }
        }

        public PlayerSettingsRequest CurrentSettingsRequest
        {
            get { return _currentSettingsRequest; }
            set { _currentSettingsRequest = value; }
        }

        public GameDirector Director
        {
            get { return GameDirector.Instance; }

        }
        /// <summary>
        /// process the queue, notify the director that the player has finished selecting their choices.
        /// </summary>
        /// <returns></returns>
        public IEnumerator ProcessQueue()
        {

            if (Instance.PlayerCreationQueue.Count == 0)
            {
                //Instance.CurrentSettingsRequest = null;
                yield break;
            }

            CurrentSettingsRequest = Instance.PlayerCreationQueue.Dequeue();
           
          //  PlayerSettingsRequest currRequest = (PlayerSettingsRequest) CurrentSettingsRequest;//unbox the nullable type
            while (!CurrentSettingsRequest.FinishedSelection())
                {
                    yield return null;
                }
            //send a message to the director that a player has finished processing
            Director.PlayerFinishedSelection();
            if (CurrentSettingsRequest.FinishedSelection())
           {
               Debug.Log("changing players");
              
                //let the director know that the player has finished selecting his choices
               StartCoroutine(ProcessQueue()); //call this recursively. 
            }
            

        }
 

        /// <summary>
        /// Registers the director, then add the number of players
        /// </summary>
        /// <param name="numberPlayers"></param>
        public void RegisterNumPlayers( List<Player> playerList )
        {
            Instance.PlayerCreationQueue = new Queue<PlayerSettingsRequest>();
            for (int i = playerList.Count - 1; i >= 0; i--)
            {
                PlayerSettingsRequest playerRequest = new PlayerSettingsRequest();
                playerRequest.Player = playerList[i];
                Instance.PlayerCreationQueue.Enqueue(playerRequest);
            }
          
            StartCoroutine(ProcessQueue());
        }

        public void IconSelected()
        {
            
            _currentSettingsRequest.HasSelectedIcon = true;
        }

        public void PictureSelected()
        {
            _currentSettingsRequest.HasSelectedPicture=true;
        }

        public string info;

        void Update()
        {
            info = "has selected icon" + CurrentSettingsRequest.HasSelectedIcon + "   " + "has selected pic" + CurrentSettingsRequest.HasSelectedPicture;
        }


    }

    public struct PlayerSettingsRequest
    {
        public bool HasSelectedIcon;
        public bool HasSelectedPicture;
        public Player Player;

        public void Initialize()
        {
            HasSelectedIcon = false;
            HasSelectedPicture = false;
        }

        public void ChangeIconSelectionToTrue()
        {
            HasSelectedIcon = true;
        }
        public void ChangeIconSelectionToFalse()
        {
            HasSelectedIcon = false;
        }
        public bool FinishedSelection()
        {
            return HasSelectedIcon && HasSelectedPicture;
        }

        public void ChangePictureSelectionToTrue()
        {
            HasSelectedIcon = true;
        }
        public void ChangePictureSelectionToFalse()
        {
            HasSelectedIcon = false;
        }
    }
}
