
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
        private Nullable<PlayerSettingsRequest> _currentSettingsRequest;
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

        public PlayerSettingsRequest? CurrentSettingsRequest
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
                Instance.CurrentSettingsRequest = null;
                yield break;
            }

            CurrentSettingsRequest = Instance.PlayerCreationQueue.Dequeue();
            PlayerSettingsRequest currRequest = (PlayerSettingsRequest) CurrentSettingsRequest;//unbox the nullable type
            while (!currRequest.FinishedSelection())
                {
                    yield return null;
                }
            //send a message to the director that a player has finished processing
            Director.PlayerFinishedSelection();
            StartCoroutine(ProcessQueue()); //call this recursively. 
            yield break;

        }
 

        /// <summary>
        /// Registers the director, then add the number of players
        /// </summary>
        /// <param name="numberPlayers"></param>
        public void RegisterDirector( int numberPlayers)
        {
            
            for (int i = 0; i < numberPlayers; i++)
            {
                PlayerSettingsRequest playerRequest = new PlayerSettingsRequest();
                playerRequest.Initialize();
                Instance.PlayerCreationQueue.Enqueue(playerRequest);
            }
        }

        public void IconSelected()
        {
            PlayerSettingsRequest currRequest = (PlayerSettingsRequest)CurrentSettingsRequest;//unbox the nullable type
            currRequest.HasSelectedIcon = true;
        }

        public void PictureSelected()
        {
            PlayerSettingsRequest currRequest = (PlayerSettingsRequest)CurrentSettingsRequest;//unbox the nullable type
            currRequest.HasSelectedPicture = true;  
        }



        
    }

    public struct PlayerSettingsRequest
    {
        public bool HasSelectedIcon;
        public bool HasSelectedPicture;

        public void Initialize()
        {
            HasSelectedIcon = false;
            HasSelectedPicture = false;
        }

        public bool FinishedSelection()
        {
            return HasSelectedIcon && HasSelectedPicture;
        }

    }
}
