﻿
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Resources.Code.GameLogic.Level;
using Assets.Resources.Code.GameLogic.Messaging;
using Assets.Resources.Code.Players;

using UnityEngine;


namespace Assets.Resources.Code.GameLogic
{
    public class GameDirector : MonoBehaviour
    {
        private static GameDirector _instance;
        private PlayerSettingsCreationManager _playerSettings;
        private Player _currentPlayer;
        private List<Player> _playerList; 
        private Queue<Player> _playerQueue;
        private List<BaseLevelController> _levelsList;
        private BaseLevelController _currentLevel;
        public bool SkipTutorial = true;
        private bool _singlePlayerMode;



        public Queue<Player> PlayerQueue
        {
            get
            {
                if (_playerQueue == null)
                {
                    _playerQueue = new Queue<Player>();
                }
                return _playerQueue;
            }
            set { _playerQueue = value; }
        }

        public static GameDirector Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject dir = GameObject.FindGameObjectWithTag("Director");
                    _instance = dir.GetComponent<GameDirector>();
                }
                return _instance;
            }
            set { _instance = value; }
        }

        public PlayerSettingsCreationManager PlayerSettings
        {
            get { return PlayerSettingsCreationManager.Instance; }

        }

        public List<BaseLevelController> LevelsList
        {
            get
            {
                if (_levelsList == null)
                {
                    _levelsList = new List<BaseLevelController>();
                }
                return _levelsList;
            }
            set { _levelsList = value; }
        }

        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set { _currentPlayer = value; }
        }

        public List<Player> PlayerList
        {
            get
            {
                if (_playerList == null)

                {
                    _playerList = new List<Player>();
                }
                return _playerList;
            }
            set { _playerList = value; }
        }


        public void NotifyWindow(string msg, string originator)
        {

        }


        /// <summary>
        /// need to know which player finished processing
        /// if it's player 1 and we have multiple players, then we set the current player as the next player in the list
        /// else change the state 
        /// </summary>
        internal void PlayerFinishedSelection()
        {
            if (_singlePlayerMode)
            {
                ChangeStateToGameMode();
            }
            else if (!_singlePlayerMode && PlayerQueue.Count != 0)
            {
                CurrentPlayer = PlayerQueue.Dequeue();
                ChangeStateToIconSelection();
            }
        }

        private void ChangeStateToIconSelection()
        {
            LevelView.Instance.SwitchToPlayerIconSelect(true);
        }

        private void ChangeStateToGameMode()
        {
            LevelView.Instance.SwitchToNoRollStateView(true);
        }

        public void PlayerIconSelected(string icon)
        {
            string chosenIcon = icon.ToLowerInvariant();
            switch (chosenIcon)
            {
                case "monkey":
                    CurrentPlayer.SelectedIcon = PlayerHeadType.Monkey;
                    break;
                case "giraffe":
                    CurrentPlayer.SelectedIcon = PlayerHeadType.Giraffe;
                    break;
            }
            CurrentPlayer.ChosenIcon = icon;
            
            PlayerSettingsCreationManager.Instance.IconSelected();
        }

        public void VerifyPlayerPicture()
        {
            if (CurrentPlayer.HasPicture)
            {
                MessageNotificationManager.Instance.RequestMessageBox(
                    new Message(new Sender 
                    { Name = CurrentPlayer.PlayerName, CallbackAction = RequestToReplacePicture },
                        "It seems like you already have a picture stored. Do you wish to take a new one instead?"));
            }
            else
            {
                MessageNotificationManager.Instance.RequestMessageBox(
                    new Message(new Sender { Name = CurrentPlayer.PlayerName, CallbackAction = RequestToTakeNewPicture },
                        "First we need to take a picture of you first. Make sure you line up your face for a snazzy pic!"));
               
            }
        }

        private void RequestToTakeNewPicture(bool obj)
        {
            ChangeLevelStateToCameraView();
        }

        private void RequestToReplacePicture(bool status)
        {
            if (status)
            {
                ChangeLevelStateToCameraView();
            }
            else
            {
                PlayerPictureSelected();
            }
        }

        private void ChangeLevelStateToCameraView()
        {
            _currentLevel.ChangeState(BaseLevelState.CameraViewState);
        }

        public void PlayerPictureSelected()
        {
            CurrentPlayer.HasPicture = true;
            PlayerSettingsCreationManager.Instance.PictureSelected();
        }


        public void TakePlayerPicture()
        {

        }
    }
}
