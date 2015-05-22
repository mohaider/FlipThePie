using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Code.GameLogic.IngameObjects.MusicButton;
using Assets.Resources.Code.GameLogic.Level.Level1;
using Assets.Resources.Code.GameLogic.Messaging.SwishingMessage;
using Assets.Resources.Code.Players;
using Assets.Resources.Code.Tools;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.Directors
{
    public class LevelOneDirector: MonoBehaviour
    {
        private List<Player> m_players;
        private Player m_currentPlayer;
        private int m_playerIndex = 0;
        private int m_difficultyIncreaseFactor = 0;
        private LevelOneController m_levelOneController;
        private MusicButtonController m_musicButtonControls;
        private SwishingMessageManager m_swishingMessage;

        private string[] flavorTextsForChangingPlayers =
        {"WATCH AND LISTEN", "LISTEN TO THIS!","WATCH AND LEARN","KEEP YOUR EARS OPEN AND YOUR EYES PEELED"};

        public void ChangeLevelState()
        {
            
        }

        public void ChangePlayer()
        {
            
        }

        private string PickRandomFlavorText()
        {
            int random = UnityEngine.Random.Range(0, flavorTextsForChangingPlayers.Length);
            return flavorTextsForChangingPlayers[random];
        }

        void IncreaseDifficulty()
        {
            m_difficultyIncreaseFactor++;
            if (m_difficultyIncreaseFactor%PlayersList.Count == 0)
            {
                Level1Controller.IncreaseDifficultyByOne();
               
            }
         
        }
        void ValidSequence()
        {
            m_playerIndex ++;
            if (m_playerIndex > PlayersList.Count)
            {
                m_playerIndex = 0;
            }
            m_currentPlayer = PlayersList[m_playerIndex];
            IncreaseDifficulty();
            Level1Controller.ChangeState(LevelOneStates.RegularGameState);
           SwishingMessageManage.RequestMessageBox(m_currentPlayer.PlayerName,PickRandomFlavorText());
            
        }

        void InvalidSequence()
        {
            //todo : change state for play to pie in the face
        }
       //music sequence

        void OnEnable()
        {
            MusicButtonController.IncorrectSequenceEvent += InvalidSequence;
            MusicButtonController.CorrectSequenceEvent += ValidSequence;
        }

        void OnDisable()
        {
            MusicButtonController.IncorrectSequenceEvent -= InvalidSequence;
            MusicButtonController.CorrectSequenceEvent -= ValidSequence;
        }

        public MusicButtonController MusicButtonControls
        {
            get
            {
                if (m_musicButtonControls == null)
                {
                    m_musicButtonControls = SceneObjectFinder<MusicButtonController>.FindGameObjectReturnT("MusicButtonController");
                }
                return m_musicButtonControls;
            }
            set { m_musicButtonControls = value; }
        }

        public LevelOneController Level1Controller
        {
            get { return GetComponent<LevelOneController>(); }
            set { m_levelOneController = value; }
        }

        public List<Player> PlayersList
        {
            get
            {
                if (m_players == null)
                {
                    PlayersList = GameDirector.Instance.PlayerList;
                }
                return m_players;
            }
            set { m_players = value; }
        }

        public SwishingMessageManager SwishingMessageManage
        {
            get { return SwishingMessageManager.Instance; }
            set { m_swishingMessage = value; }
        }
    }
}
